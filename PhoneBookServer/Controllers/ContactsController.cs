using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.DataAccess.Interface;
using PhoneBook.Models;

namespace PhoneBookServer.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
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
            contactErrors.AddRange(contact.PhoneNumbers?.SelectMany(pn=>pn.Validate()) ?? new List<string>());

            if (contactErrors.Count > 0)
            {
                return BadRequest(contactErrors);
            }

            contact.Id = Guid.NewGuid();

            try
            {
                _contactsRepository.CreateContact(contact);
                return Ok(contact.Id);
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
