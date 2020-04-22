using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityNetCore.ViewModels
{
    public class SignupViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "O endereço de e-mail está ausente ou é inválido.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Senha incorreta ou ausente.")]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
