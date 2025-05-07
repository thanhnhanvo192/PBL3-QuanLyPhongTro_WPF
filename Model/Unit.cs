using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Units")]
    public class Unit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DisplayName { get; set; }
        
        public virtual ICollection<Service> Services { get; set; }
        public Unit()
        {
            Services = new HashSet<Service>();
        }
    }
}
