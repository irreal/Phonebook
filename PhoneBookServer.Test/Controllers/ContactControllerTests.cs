using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using PhoneBook.DataAccess.Interface;
using PhoneBook.Models;
using PhoneBookServer.Controllers;
using Xunit;

namespace PhoneBookServer.Test.Controllers
{
    public class ContactControllerTests
    {
        [Fact]
        public async Task GetAllContactsReturnsAllContacts()
        {
            List<Contact> allContacts = GetTestContacts();
            var mockContacts = new Mock<IContactsRepository>();
            mockContacts.Setup(c => c.GetAll()).Returns(Task.FromResult((IList<Contact>)allContacts));

            var controller = new ContactsController(mockContacts.Object);

            var resultContacts = await controller.Get();

            Assert.Equal(allContacts.Count, resultContacts.Count);

        }

        private List<Contact> GetTestContacts()
        {
            return new List<Contact>
            {
                new Contact("Miloš", "Spasojević",
                    new[] {new PhoneNumber(PhoneNumberType.Cellphone, "+381 60 412 84 22"),})
            };
        }
    }
}
