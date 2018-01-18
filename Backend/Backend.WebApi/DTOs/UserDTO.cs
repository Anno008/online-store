using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class UserDTO : IValidatableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Username == null || Password == null)
                yield return new ValidationResult("Must provide username/password");
        }
    }
}
