using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonderPlane.Shared
{
    public class UserDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; }
    }
}

