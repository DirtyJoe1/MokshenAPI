using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserForRegistrationDto
    {
        [Required(ErrorMessage = "Логин - обязательное поле")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Пароль - обязательное поле")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Эл. почта - обязательное поле")]
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
