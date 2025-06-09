using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.Model
{
    [Table("Units")]
    public class Unit : BaseViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitId { get; set; }

        private UnitCode _code;
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public UnitCode Code
        {
            get => _code;
            set { _code = value; OnPropertyChanged(); }
        }

        private string _displayName;
        [Required]
        [MaxLength(50)]
        public string DisplayName
        {
            get => _displayName;
            set { _displayName = value; OnPropertyChanged(); }
        }
        public bool IsDeleted { get; set; } // Đánh dấu hóa đơn đã bị xoá
        // Navigation property: Một đơn vị có thể dùng trong nhiều dịch vụ
        public virtual ICollection<Service> Services { get; set; }

        public Unit()
        {
            Services = new HashSet<Service>();
        }
    }
}
