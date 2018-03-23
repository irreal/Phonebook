using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using PhoneBook.DataAccess.InMemoryImplementation;
using PhoneBook.DataAccess.Interface;
using PhoneBook.Models;

namespace PhoneBookServer.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private IContactsRepository _contactsRepository;

        public ContactController()
        {
            //In a real application this service would be injected through the constructor by using a DependencyInjection controller and setting up a binding for IContactRepository

            this._contactsRepository = new PhoneBook.DataAccess.InMemoryImplementation.ContactsRepository(); // Mock repository for development
        }

        public ContactController(IContactsRepository contactsRepository)
        {
            this._contactsRepository = contactsRepository;
        }

        [HttpGet]
        public async Task<IList<Contact>> Get()
        {
            return await _contactsRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get(Guid id)
        {
            return await _contactsRepository.GetContact(id);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Contact contact)
        {

            var contactErrors = contact.Validate();
            contactErrors.AddRange(contact.PhoneNumbers.SelectMany(pn=>pn.Validate()));

            if (contactErrors.Count > 0)
            {
                return BadRequest(contactErrors);
            }

            try
            {
                _contactsRepository.CreateContact(contact);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody]Contact value)
        {
            try
            {
                await _contactsRepository.UpdateContact(id, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {

            if (await _contactsRepository.DeleteContact(id))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
