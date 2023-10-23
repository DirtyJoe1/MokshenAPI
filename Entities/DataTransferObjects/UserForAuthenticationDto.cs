using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Имя пользователя - обязательное поле")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Пароль - обязательное поле")]
        public string Password { get; set; }
    }
}
