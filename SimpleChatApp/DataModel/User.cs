using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name="Логин")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(18, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 18 символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        public ICollection<Message> Messages { get; set; }
        public User()
        {
            Messages = new List<Message>();
        }
    }
}
