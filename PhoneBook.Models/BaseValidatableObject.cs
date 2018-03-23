using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Models
{
    public abstract class BaseValidatableObject : IValidatable
    {
        public abstract List<string> Validate();

        public virtual bool TryValidate()
        {
            return (this.Validate()?.Count ?? 0) == 0;
        }

        public virtual bool TryValidate(out List<string> errors)
        {
            errors = this.Validate();
            return (errors?.Count ?? 0) == 0;
        }
    }
}
