using AzureMediaServicesSamples.SDK.Utils;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureMediaServicesSamples.SDK
{
    public class AzureMediaServicesManager
    {
        private readonly ConfigWrapper _config;
        private IAzureMediaServicesClient _azureMediaServicesClient;

        public AzureMediaServicesManager(ConfigWrapper config)
        {
            _config = config;
        }

        private async Task<ServiceClientCredentials> GetCredentialsAsync()
        {
            ClientCredential clientCredential = new ClientCredential(_config.AadClientId, _config.AadSecret);
            return await ApplicationTokenProvider.LoginSilentAsync(_config.AadTenantId, clientCredential, ActiveDirectoryServiceSettings.Azure);
        }

        public async Task<IAzureMediaServicesClient> CreateMediaServicesClientAsync(ConfigWrapper config)
        {
            var credentials = await GetCredentialsAsync();

            if (_azureMediaServicesClient == null)
            {
                _azureMediaServicesClient = new AzureMediaServicesClient(_config.ArmEndpoint, credentials)
                {
                    SubscriptionId = config.SubscriptionId,
                };

                return _azureMediaServicesClient;
            }

            else
            {
                return _azureMediaServicesClient;
            }
        }

        public async Task<LiveEvent> CreateLiveEventAsync(string liveEventName)
        {
            // Getting the mediaServices account so that we can use the location to create the
            // LiveEvent and StreamingEndpoint
            MediaService mediaService = await _azureMediaServicesClient.Mediaservices.GetAsync(_config.ResourceGroup, _config.AccountName);

            Console.WriteLine($"Creating a live event named {liveEventName}");
            Console.WriteLine();

            // Note: When creating a LiveEvent, you can specify allowed IP addresses in one of the following formats:                 
            //      IpV4 address with 4 numbers
            //      CIDR address range

            IPRange allAllowIPRange = new IPRange(
                name: "AllowAll",
                address: "0.0.0.0",
                subnetPrefixLength: 0
            );

            // Create the LiveEvent input IP access control.
            LiveEventInputAccessControl liveEventInputAccess = new LiveEventInputAccessControl
            {
                Ip = new IPAccessControl(
                        allow: new IPRange[]
                        {
                                allAllowIPRange
                        }
                    )
            };

            // Create the LiveEvent Preview IP access control
            LiveEventPreview liveEventPreview = new LiveEventPreview
            {
                AccessControl = new LiveEventPreviewAccessControl(
                    ip: new IPAccessControl(
                        allow: new IPRange[]
                        {
                                allAllowIPRange
                        }
                    )
                )
            };

            // To get the same ingest URL for the same LiveEvent name:
            // 1. Set vanityUrl to true so you have ingest like: 
            //        rtmps://liveevent-hevc12-eventgridmediaservice-usw22.channel.media.azure.net:2935/live/522f9b27dd2d4b26aeb9ef8ab96c5c77           
            // 2. Set accessToken to a desired GUID string (with or without hyphen)

            LiveEvent liveEvent = new LiveEvent(
                location: mediaService.Location,
                description: "Sample LiveEvent for testing",
                vanityUrl: false,
                encoding: new LiveEventEncoding(
                            // Set this to Standard to enable a transcoding LiveEvent, and None to enable a pass-through LiveEvent
                            encodingType: LiveEventEncodingType.None,
                            presetName: null
                        ),
                input: new LiveEventInput(LiveEventInputProtocol.RTMP, liveEventInputAccess),
                preview: liveEventPreview,
                streamOptions: new List<StreamOptionsFlag?>()
                {
                        // Set this to Default or Low Latency
                        // When using Low Latency mode, you must configure the Azure Media Player to use the 
                        // quick start hueristic profile or you won't notice the change. 
                        // In the AMP player client side JS options, set -  heuristicProfile: "Low Latency Heuristic Profile". 
                        // To use low latency optimally, you should tune your encoder settings down to 1 second GOP size instead of 2 seconds.
                        StreamOptionsFlag.LowLatency
                }
            );

            Console.WriteLine($"Creating the LiveEvent, be patient this can take time...");

            // When autostart is set to true, the Live Event will be started after creation. 
            // That means, the billing starts as soon as the Live Event starts running. 
            // You must explicitly call Stop on the Live Event resource to halt further billing.
            // The following operation can sometimes take awhile. Be patient.
            return await _azureMediaServicesClient.LiveEvents.CreateAsync(_config.ResourceGroup, _config.AccountName, liveEventName, liveEvent, autoStart: true);
        }

        public async Task<StreamingLocator> CreateAssetAndLocatorAsync(string assetName, string streamingLocatorName)
        {
            // Create an Asset for the LiveOutput to use
            Console.WriteLine($"Creating an asset named {assetName}");
            Console.WriteLine();
            Asset asset = await _azureMediaServicesClient.Assets.CreateOrUpdateAsync(_config.ResourceGroup, _config.AccountName, assetName, new Asset());

            // Create the StreamingLocator
            Console.WriteLine($"Creating a streaming locator named {streamingLocatorName}");
            Console.WriteLine();

            StreamingLocator locator = new StreamingLocator(assetName: asset.Name, streamingPolicyName: PredefinedStreamingPolicy.ClearStreamingOnly);
            return await _azureMediaServicesClient.StreamingLocators.CreateAsync(_config.ResourceGroup, _config.AccountName, streamingLocatorName, locator);
        }

        public async Task<StreamingEndpoint> StartStreamingEndpointAsync(string assetName, string streamingEndpointName)
        {
            // Get the default Streaming Endpoint on the account
            StreamingEndpoint streamingEndpoint = await _azureMediaServicesClient.StreamingEndpoints.GetAsync(_config.ResourceGroup, _config.AccountName, streamingEndpointName);

            // If it's not running, Start it. 
            if (streamingEndpoint.ResourceState != StreamingEndpointResourceState.Running)
            {
                Console.WriteLine("Streaming Endpoint was Stopped, restarting now..");
                await _azureMediaServicesClient.StreamingEndpoints.StartAsync(_config.ResourceGroup, _config.AccountName, streamingEndpointName);

                streamingEndpoint = await _azureMediaServicesClient.StreamingEndpoints.GetAsync(_config.ResourceGroup, _config.AccountName, streamingEndpointName);
            }

            return streamingEndpoint;
        }

        public async Task CleanupLiveEventAndOutputAsync(string liveEventName)
        {
            try
            {
                LiveEvent liveEvent = await _azureMediaServicesClient.LiveEvents.GetAsync(_config.ResourceGroup, _config.AccountName, liveEventName);

                if (liveEvent != null)
                {
                    if (liveEvent.ResourceState == LiveEventResourceState.Running)
                    {
                        // If the LiveEvent is running, stop it and have it remove any LiveOutputs
                        await _azureMediaServicesClient.LiveEvents.StopAsync(_config.ResourceGroup, _config.AccountName, liveEventName, removeOutputsOnStop: true);
                    }

                    // Delete the LiveEvent
                    await _azureMediaServicesClient.LiveEvents.DeleteAsync(_config.ResourceGroup, _config.AccountName, liveEventName);
                }
            }
            catch (ApiErrorException e)
            {
                Console.WriteLine("CleanupLiveEventAndOutputAsync -- Hit ApiErrorException");
                Console.WriteLine($"\tCode: {e.Body.Error.Code}");
                Console.WriteLine($"\tCode: {e.Body.Error.Message}");
                Console.WriteLine();
            }
        }

        public async Task CleanupLocatorAndAssetAsync(string streamingLocatorName, string assetName)
        {
            try
            {
                // Delete the Streaming Locator
                await _azureMediaServicesClient.StreamingLocators.DeleteAsync(_config.ResourceGroup, _config.AccountName, streamingLocatorName);

                // Delete the Archive Asset
                await _azureMediaServicesClient.Assets.DeleteAsync(_config.ResourceGroup, _config.AccountName, assetName);
            }
            catch (ApiErrorException e)
            {
                Console.WriteLine("CleanupLocatorAndAssetAsync -- Hit ApiErrorException");
                Console.WriteLine($"\tCode: {e.Body.Error.Code}");
                Console.WriteLine($"\tCode: {e.Body.Error.Message}");
                Console.WriteLine();
            }
        }

        public async Task StopStreamingEndpointAsync(string assetName, string streamingEndpointName)
        {
            // Get the default Streaming Endpoint on the account
            StreamingEndpoint streamingEndpoint = await _azureMediaServicesClient.StreamingEndpoints.GetAsync(_config.ResourceGroup, _config.AccountName, streamingEndpointName);

            // If it's running, Stop it. 
            if (streamingEndpoint.ResourceState == StreamingEndpointResourceState.Running)
            {
                Console.WriteLine("Streaming Endpoint was running, stopping now...");
                await _azureMediaServicesClient.StreamingEndpoints.StopAsync(_config.ResourceGroup, _config.AccountName, streamingEndpointName);
            }
        }
    }
}
