using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WorkingTimer.Shared
{
    [Table("Events")]
    public class CalenderEvents 
    {
        [Key]
        public string Id { get; set; }

        public string Subject { get; set; }

        [Required]
        public DateTime Journee { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [StringLength(5)]
        public string Duree { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime ModifiedDate { get; set; }

        [Required]
        [StringLength(40)]
        public string UserId { get; set; }

        public bool IsModified { get; set; }

    }
}
