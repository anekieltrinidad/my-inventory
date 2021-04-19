using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyInventory.Models
{
    public class Contact
    {

        [Display(Name = "Sender Name")]
        [Required(ErrorMessage = "Required")]
        public String SenderName { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Format")]
        [Required(ErrorMessage = "Required")]
        public String email { get; set; }

        [Display(Name = "Contact Number")]
        public String ContactNo { get; set; }

        [Required(ErrorMessage = "Required")]
        public String Subject { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.MultilineText)]
        public String Message { get; set; }
    }
}
