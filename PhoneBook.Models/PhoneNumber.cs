using System;
using System.Collections.Generic;
using System.Linq;
using PhoneBook.Models.Resources;

namespace PhoneBook.Models
{
    public class PhoneNumber : BaseValidatableObject, IValidatable
    {
        private readonly Char[] _allowedExtraCharacters = { '(', ')', '-', '/' };
        public PhoneNumber(PhoneNumberType phoneNumberType, string phoneNumber)
        {
            this.NumberType = phoneNumberType;
            this.Number = phoneNumber;
        }

        public string Number { get; set; }

        public PhoneNumberType NumberType { get; set; }
        public override List<string> Validate()
        {
            var errors = new List<string>(1);
            if (string.IsNullOrWhiteSpace(this.Number) ||
                this.Number.Any(ch => !Char.IsDigit(ch) && !_allowedExtraCharacters.Contains(ch)))
            {
                errors.Add($"{ModelResources.PhoneNumber_No_Valid_Number}. {ModelResources.PhoneNumber_Valid_Characters}");
            }

            return errors;
        }
    }
}
