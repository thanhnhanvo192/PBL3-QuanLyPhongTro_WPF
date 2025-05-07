using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Tenants")] // Khách thuê
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string CCCD { get; set; } // Unique
        public DateTime? Birthday { get; set; } // Nullable DateTime
        public byte? Sex { get; set; } // 0: Nam, 1: Nữ, 2: Khác (nullable)
        [MaxLength(20)]
        public string Phone { get; set; } // Unique
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } // Unique
        public string PermanentAddress { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Fix> Fixes { get; set; }

        public Tenant()
        {
            Contracts = new HashSet<Contract>();
            Fixes = new HashSet<Fix>();
        }
    }
}
