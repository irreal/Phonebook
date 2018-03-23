using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBook.DataAccess.Interface;
using PhoneBook.Models;

namespace PhoneBook.DataAccess.InMemoryImplementation
{
    public class ContactsRepository : IContactsRepository
    {
        private static readonly List<Contact> Contacts = new List<Contact>
        { // Initial mock data
             new Contact("Miloš", "Spasojević", new []{ new PhoneNumber(PhoneNumberType.Cellphone, "+381 60 412 84 22"),  })
        };

        public Task<Guid> CreateContact(Contact contact)
        {
            ValidateContact(contact);

            if (Contacts.Any(c => c.Id == contact.Id))
            {
                throw new InvalidOperationException("Cannot create contact. There exists a contact with the same Id already in the database.");
            }
            Contacts.Add(contact);


            return Task.FromResult(contact.Id);
        }

        public Task UpdateContact(Guid id, Contact contact)
        {
            ValidateContact(contact);

            var existingContact = Contacts.FirstOrDefault(c => c.Id == id);

            if (existingContact == null)
            {
                throw new Exception("The contact you are trying to update does not exist in the database");
            }

            if (contact.Id != existingContact.Id && Contacts.Any(c => c.Id == contact.Id))
            {
                throw new Exception("There is already another contact with the updated Id you are trying to set.");
            }

            var index = Contacts.IndexOf(existingContact);
            Contacts.RemoveAt(index);
            Contacts.Insert(index, contact);

            return Task.FromResult(true);
        }

        public Task<IList<Contact>> GetAll()
        {
            return Task.FromResult((IList<Contact>)Contacts);
        }

        public Task<Contact> GetContact(Guid id)
        {
            return Task.FromResult(Contacts.FirstOrDefault(c => c.Id == id));
        }

        public Task<IList<Contact>> SearchPersons(string searchTerm)
        {
            var searchTermLowered = searchTerm.ToLower();

            var foundContacts = Contacts.Where(c =>
                c.FullName.ToLower().Contains(searchTermLowered) ||
                c.PhoneNumbers.Any(
                    pn => pn.Number.Contains(searchTermLowered)
                    )
                );

            return Task.FromResult((IList<Contact>)foundContacts.ToList());
        }

        public Task<bool> DeleteContact(Guid id)
        {
            var existingContact = Contacts.FirstOrDefault(c => c.Id == id);
            if (existingContact != null)
            {
                Contacts.Remove(existingContact);
            }

            return Task.FromResult(existingContact != null);
        }

        private static void ValidateContact(Contact contact)
        {
            if (!contact.TryValidate())
            {
                throw new InvalidOperationException("The contact you have tried adding is not valid.");
            }
        }
    }
}
