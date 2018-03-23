namespace PhoneBook.Models
{
    public class PhoneNumber
    {
        public PhoneNumber(PhoneNumberType phoneNumberType, string phoneNumber)
        {
            this.NumberType = phoneNumberType;
            this.Number = phoneNumber;
        }

        public string Number { get; set; }
        
        public PhoneNumberType NumberType { get; set; }
    }
}
