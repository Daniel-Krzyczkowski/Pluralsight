using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services.Interfaces;
using CarsIsland.WPF.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WPF.Core.Services
{
    public class ContactsDataService : BaseDataService, IContactsDataService
    {
        public async Task<IList<Contact>> GetContacts()
        {
            var response = await _httpClient.GetAsync(AppConfiguration.ContactsDataEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();
            var contacts = JsonConvert.DeserializeObject<IList<Contact>>(responseContent);
            return contacts;
        }
    }
}
