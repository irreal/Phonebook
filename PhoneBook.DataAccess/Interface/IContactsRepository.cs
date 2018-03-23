﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Models;

namespace PhoneBook.DataAccess.Interface
{
    public interface IContactsRepository
    {
        Task CreateContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task<Contact> GetContact(Guid id);
        Task<IList<Contact>> SearchPersons(string searchTerm);
        Task<bool> DeleteContact(Guid id);
    }
}
