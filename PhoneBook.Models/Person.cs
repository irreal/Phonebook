namespace PhoneBook.Models
{
    public class Person
    {
        public Person(string firstName, string lastName = null)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName ?? string.Empty}".Trim(); // .Trim() removes empty trailing space if there is no last name defined
    }
}
