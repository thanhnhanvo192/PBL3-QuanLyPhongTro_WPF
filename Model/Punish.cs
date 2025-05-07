using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Punishes")]
    public class Punish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContractID { get; set; }
        [ForeignKey("ContractID")]
        public virtual Contract Contract { get; set; }
        public DateTime PunishDate { get; set; }
        [Required]
        public string Reason { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        public Punish()
        {
            PunishDate = DateTime.Now;
            Amount = 0;
        }
    }
}
