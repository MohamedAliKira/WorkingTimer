using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTimer.Shared
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(25)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 6)]
        public string Passord { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
