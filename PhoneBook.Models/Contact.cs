using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PhoneBook.Models.Resources;

namespace PhoneBook.Models
{
    public class Contact : BaseValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. This overload is used by data access services to generate an object from existing Contact data with an ID
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="phoneNumbers">The phone numbers.</param>
        public Contact(Guid id, string firstName, string lastName, IEnumerable<PhoneNumber> phoneNumbers)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Id = id;
            this.PhoneNumbers = phoneNumbers != null ? new List<PhoneNumber>(phoneNumbers) : null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. This overload should be used when creating a new user which does not have an existing Id.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="phoneNumbers">The phone numbers.</param>
        public Contact(string firstName, string lastName, IEnumerable<PhoneNumber> phoneNumbers = null) : this(Guid.NewGuid(), firstName, lastName, phoneNumbers)
        {
        }

        public Contact() // Temp hack for deserialization, I would normally implement a custom json deserializer for this object
        {
                
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("phoneNumbers")]
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public string FullName => $"{FirstName} {LastName ?? string.Empty}".Trim(); // .Trim() removes empty trailing space if there is no last name defined
        public override List<string> Validate()
        {
          var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(this.FirstName))
            {
                errors.Add(ModelResources.Person_No_Valid_FirstName);
            }
            return errors;
        }
    }
}
