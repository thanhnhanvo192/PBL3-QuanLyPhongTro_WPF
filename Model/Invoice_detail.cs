using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Invoice_details")] // Tên bảng rõ ràng
    public class Invoice_detail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        public int? ServiceId { get; set; } 
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [NotMapped]
        public decimal Amount => UnitPrice * Quantity;
        [MaxLength(255)]
        public string Notes { get; set; }

        public Invoice_detail()
        {
            UnitPrice = 0;
            Quantity = 0;
        }
    }
}
