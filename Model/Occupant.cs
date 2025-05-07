using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Occupants")] // Người ở cùng
    public class Occupant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string CCCD { get; set; } // Unique
        public DateTime? Birthday { get; set; }
        public byte? Sex { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Relationship { get; set; }
    }
}
