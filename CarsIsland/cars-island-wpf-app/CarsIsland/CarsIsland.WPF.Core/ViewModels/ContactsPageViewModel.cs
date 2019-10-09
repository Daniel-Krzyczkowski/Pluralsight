using CarsIsland.WPF.Core.Services.Interfaces;
using CarsIsland.WPF.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WPF.Core.ViewModels
{
    public class ContactsPageViewModel : AppViewModel
    {
        private readonly IContactsDataService _contactsDataService;

        public ObservableCollection<Contact> _contactsList;
        public ObservableCollection<Contact> ContactsList
        {
            get
            {
                return _contactsList;
            }

            set
            {
                _contactsList = value;
                RaisePropertyChanged(nameof(ContactsList));
            }
        }

        public ContactsPageViewModel(IContactsDataService contactsDataService)
        {
            _contactsDataService = contactsDataService;
        }

        public async Task GetContactsData()
        {
            var contactsData = await _contactsDataService.GetContacts();
            ContactsList = new ObservableCollection<Contact>(contactsData);
        }
    }
}
