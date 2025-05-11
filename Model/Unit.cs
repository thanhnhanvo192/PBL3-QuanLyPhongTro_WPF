using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.Model
{
    [Table("Units")]
    public class Unit : BaseViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }
        [Required]
        [MaxLength(50)]
        private string _displayName;
        public string DisplayName { get => _displayName; set { _displayName = value; OnPropertyChanged(); } }
        
        public virtual ICollection<Service> Services { get; set; }
        public Unit()
        {
            Services = new HashSet<Service>();
        }
    }
}
