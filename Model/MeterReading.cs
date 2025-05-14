using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("MeterReadings")]
    public class MeterReading
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public int ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
        public DateTime ReadingDate { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal ReadingValue { get; set; }
        // RecordedByUserId và RecordedByUser đã được bỏ
        [MaxLength(255)]
        public string? Notes { get; set; }

        public MeterReading()
        {
            ReadingDate = DateTime.Now;
        }
    }
}
