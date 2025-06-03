using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Enum;

namespace QuanLyPhongTro.Model
{
    [Table("Invoices")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string InvoiceCode { get; set; } // Unique
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? DueDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }
        public InvoiceStatus Status { get; set; } // 0: Chưa TT, 1: Đã TT, 2: Quá hạn
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; } // Đánh dấu hóa đơn đã bị xoá
        public virtual ICollection<Invoice_detail> InvoiceDetails { get; set; }
        public virtual ICollection<Fix> Fixes { get; set; } // Chi phí sửa chữa có thể được thêm vào hóa đơn
        public virtual ICollection<Punish> Punishes { get; set; } // Khoản phạt có thể được thêm vào hóa đơn

        public Invoice()
        {
            InvoiceDetails = new HashSet<Invoice_detail>();
            Fixes = new HashSet<Fix>();
            Punishes = new HashSet<Punish>();
            CreateDate = DateTime.Now;
            Status = InvoiceStatus.UnPaid; // Mặc định là chưa thanh toán
            TotalAmount = 0;
            AmountPaid = 0;
        }
    }
}
