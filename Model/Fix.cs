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
    [Table("Fixes")]
    public class Fix
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomID { get; set; }
        [ForeignKey("RoomID")]
        public virtual Room Room { get; set; }
        public int? TenantID { get; set; }
        [ForeignKey("TenantID")]
        public virtual Tenant Tenant { get; set; }
        public DateTime FixDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Required]
        public FaultType WhoFault { get; set; }
        public int? InvoiceId { get; set; }
        public bool IsDeleted { get; set; } // Đánh dấu sửa chữa đã bị xoá
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        public Fix()
        {
            FixDate = DateTime.Now;
            WhoFault = FaultType.TenantFault;
            Cost = 0;
        }
    }
}
