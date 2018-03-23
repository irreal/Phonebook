using System.Threading.Tasks;
using PhoneBook.DataAccess.InMemoryImplementation;
using PhoneBook.Models;
using Xunit;

namespace PhoneBookServer.Test.Repositories
{
    public class PersonRepositoryTest
    {
        [Fact]
        public async Task AddingNewPersonWorks()
        {
            ContactsRepository cr = new ContactsRepository();

            var contact = new Contact("Test", "Contact");

            var id  = await cr.CreateContact(contact);

            var returnedContact = await cr.GetContact(id);
            
            Assert.NotNull(returnedContact);
            Assert.Equal(contact.FirstName, returnedContact.FirstName);
            Assert.Equal(contact.LastName, returnedContact.LastName);
        }
    }
}
