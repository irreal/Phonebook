using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Models
{
    public interface IValidatable
    {
        List<string> Validate();
        bool TryValidate();
        bool TryValidate(out List<string> errors);
    }
}
