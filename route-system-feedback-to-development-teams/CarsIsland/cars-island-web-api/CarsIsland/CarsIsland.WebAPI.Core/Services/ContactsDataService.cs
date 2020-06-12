using AutoMapper;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using CarsIsland.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Services
{
    public class ContactsDataService : BaseDataService, IContactsDataService
    {
        public ContactsDataService(CarsIslandDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<ContactDto>> GetContacts()
        {
            var contacts = await _dbContext.Contacts
                                        .Include(c => c.Location)
                                        .Select(c => _mapper.Map<ContactDto>(c))
                                        .ToListAsync();
            return contacts;
        }
    }
}
