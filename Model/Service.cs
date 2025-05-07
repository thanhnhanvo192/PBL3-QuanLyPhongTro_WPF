using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName { get; set; }

        [Column(TypeName = "decimal(18,2)")] // Chỉ định kiểu dữ liệu SQL
        public decimal Price { get; set; }

        public int UnitId { get; set; } // Khóa ngoại

        [ForeignKey("UnitId")] // Chỉ định khóa ngoại
        public virtual Unit Unit { get; set; } // Navigation property

        // Navigation property: Một Service có thể thuộc nhiều Invoice_details
        public virtual ICollection<Invoice_detail> InvoiceDetails { get; set; }

        public Service()
        {
            InvoiceDetails = new HashSet<Invoice_detail>();
            Price = 0; // Giá trị mặc định
        }
    }
}
