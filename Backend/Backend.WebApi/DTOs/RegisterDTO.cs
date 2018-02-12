using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.WebApi.DTOs
{
    public class RegisterDTO : IValidatableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Username == null || Password == null)
                yield return new ValidationResult("Must provide username/password");
            if (ClientId == null)
               yield return new ValidationResult("ClientId required");
        }
    }
}
