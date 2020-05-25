using AzureMediaServicesSamples.SDK;
using AzureMediaServicesSamples.SDK.Utils;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMediaServicesSamples.ConsoleApp
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigWrapper config = new ConfigWrapper(new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .Build());

            try
            {
                await RunAsync(config);
            }
            catch (Exception exception)
            {
                if (exception.Source.Contains("ActiveDirectory"))
                {
                    Console.Error.WriteLine("Please make sure that you have filled out the appsettings.json file");
                }

                Console.Error.WriteLine($"{exception.Message}");

                ApiErrorException apiException = exception.GetBaseException() as ApiErrorException;
                if (apiException != null)
                {
                    Console.Error.WriteLine(
                        $"ERROR: API call failed with error code '{apiException.Body.Error.Code}' and message '{apiException.Body.Error.Message}'.");
                }
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        private static async Task RunAsync(ConfigWrapper config)
        {
            AzureMediaServicesManager azureMediaServicesManager = new AzureMediaServicesManager(config);
            IAzureMediaServicesClient client = await azureMediaServicesManager.CreateMediaServicesClientAsync(config);

            // Creating a unique suffix so that we don't have name collisions if you run the sample
            // multiple times without cleaning up.
            string uniqueness = Guid.NewGuid().ToString().Substring(0, 13);
            string liveEventName = "liveevent-" + uniqueness;
            string assetName = "archiveAsset" + uniqueness;
            string liveOutputName = "liveOutput" + uniqueness;
            string streamingLocatorName = "streamingLocator" + uniqueness;
            string streamingEndpointName = "default";

            try
            {
                Task<LiveEvent> liveEventTask = azureMediaServicesManager.CreateLiveEventAsync(liveEventName);
                Task<StreamingLocator> streamingLocatorTask = azureMediaServicesManager.CreateAssetAndLocatorAsync(assetName, streamingLocatorName);
                Task<StreamingEndpoint> startStreamingEndpointTask = azureMediaServicesManager.StartStreamingEndpointAsync(assetName, streamingEndpointName);

                Task[] tasks = new Task[]
                {
                    liveEventTask,
                    streamingLocatorTask,
                    startStreamingEndpointTask
                };

                Task.WaitAll(tasks);

                LiveEvent liveEvent = liveEventTask.Result;
                StreamingEndpoint streamingEndpoint = startStreamingEndpointTask.Result;

                // Get the input endpoint to configure the on premise encoder with
                string ingestUrl = liveEvent.Input.Endpoints.First().Url;
                Console.WriteLine($"The ingest url to configure the on premise encoder (OBS Studio) with:");
                Console.WriteLine($"\t{ingestUrl}");
                Console.WriteLine();

                // Use the previewEndpoint to preview and verify
                // that the input from the encoder is actually being received
                string previewEndpoint = liveEvent.Preview.Endpoints.First().Url;
                Console.WriteLine($"The preview url:");
                Console.WriteLine($"\t{previewEndpoint}");
                Console.WriteLine();

                Console.WriteLine($"Open the live preview in your browser and use the Azure Media Player to monitor the preview playback:");
                Console.WriteLine($"\thttps://ampdemo.azureedge.net/?url={previewEndpoint}&heuristicprofile=lowlatency");
                Console.WriteLine();

                Console.WriteLine("Start the live stream now, sending the input to the ingest url and verify that it is arriving with the preview url.");
                Console.WriteLine("IMPORTANT!: Make ABSOLUTLEY CERTAIN that the video is flowing to the Preview URL before continuing!");
                Console.WriteLine("Press enter to continue...");
                Console.Out.Flush();
                var ignoredInput = Console.ReadLine();

                // Create the LiveOutput
                string manifestName = "output";
                Console.WriteLine($"Creating a live output named {liveOutputName}");
                Console.WriteLine();

                LiveOutput liveOutput = new LiveOutput(assetName: assetName, manifestName: manifestName, archiveWindowLength: TimeSpan.FromMinutes(10));
                liveOutput = await client.LiveOutputs.CreateAsync(config.ResourceGroup, config.AccountName, liveEventName, liveOutputName, liveOutput);

                // Get the url to stream the output
                var paths = await client.StreamingLocators.ListPathsAsync(config.ResourceGroup, config.AccountName, streamingLocatorName);

                Console.WriteLine("The urls to stream the output from a client:");
                Console.WriteLine();
                StringBuilder stringBuilder = new StringBuilder();
                string playerPath = string.Empty;

                for (int i = 0; i < paths.StreamingPaths.Count; i++)
                {
                    UriBuilder uriBuilder = new UriBuilder();
                    uriBuilder.Scheme = "https";
                    uriBuilder.Host = streamingEndpoint.HostName;

                    if (paths.StreamingPaths[i].Paths.Count > 0)
                    {
                        uriBuilder.Path = paths.StreamingPaths[i].Paths[0];
                        stringBuilder.AppendLine($"\t{paths.StreamingPaths[i].StreamingProtocol}-{paths.StreamingPaths[i].EncryptionScheme}");
                        stringBuilder.AppendLine($"\t\t{uriBuilder.ToString()}");
                        stringBuilder.AppendLine();

                        if (paths.StreamingPaths[i].StreamingProtocol == StreamingPolicyStreamingProtocol.Dash)
                        {
                            playerPath = uriBuilder.ToString();
                        }
                    }
                }

                if (stringBuilder.Length > 0)
                {
                    Console.WriteLine(stringBuilder.ToString());
                    Console.WriteLine("Open the following URL to playback the published, recording LiveOutput in the Azure Media Player");
                    Console.WriteLine($"\t https://ampdemo.azureedge.net/?url={playerPath}&heuristicprofile=lowlatency");
                    Console.WriteLine();

                    Console.WriteLine("Continue experimenting with the stream until you are ready to finish.");
                    Console.WriteLine("Press enter to stop the LiveOutput...");
                    Console.Out.Flush();
                    ignoredInput = Console.ReadLine();

                    await azureMediaServicesManager.CleanupLiveEventAndOutputAsync(liveEventName);

                    Console.WriteLine("The LiveOutput and LiveEvent are now deleted.  The event is available as an archive and can still be streamed.");
                    Console.WriteLine("Press enter to finish cleanup...");
                    Console.Out.Flush();
                    ignoredInput = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("No Streaming Paths were detected. Probably the stream has not been started?");
                    Console.WriteLine("Cleaning up and exiting...");
                }
            }
            catch (ApiErrorException e)
            {
                Console.WriteLine("Hit ApiErrorException");
                Console.WriteLine($"\tCode: {e.Body.Error.Code}");
                Console.WriteLine($"\tCode: {e.Body.Error.Message}");
                Console.WriteLine();
                Console.WriteLine("Exiting, cleanup may be necessary...");
                Console.ReadLine();
            }
            finally
            {
                await azureMediaServicesManager.CleanupLiveEventAndOutputAsync(liveEventName);

                await azureMediaServicesManager.CleanupLocatorAndAssetAsync(streamingLocatorName, assetName);

                await azureMediaServicesManager.StopStreamingEndpointAsync(assetName, streamingEndpointName);
            }
        }
    }
}
