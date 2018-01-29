using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.WebApi.DTOs
{
    public class AuthDTO : IValidatableObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }

        // When the grant type is 'password' user want's to login
        // and acquire an access token in that case, both username and password are mandatory
        // When the grant type is 'refresh_token' user wants to get a new access_token based on
        // their refresh token
        [Required]
        public string GrantType { get; set; }
        public string ClientId { get; set; }
        public bool RememberMe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantType != "password" && GrantType != "refresh_token")
                yield return new ValidationResult("Invalid grant type", new[] { nameof(GrantType) });
        }
    }
}
