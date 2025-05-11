using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string RoomNumber { get; set; } // Sẽ là unique trên toàn hệ thống
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Area { get; set; }
        public int? MaxOccupants { get; set; }
        public int? Floor { get; set; }
        public string Utilities { get; set; }
        public byte Status { get; set; } // 0: Trống, 1: Đã thuê, 2: Đang sửa chữa
        public string Description { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Fix> Fixes { get; set; }
        public virtual ICollection<MeterReading> MeterReadings { get; set; }

        public Room()
        {
            Contracts = new HashSet<Contract>();
            Fixes = new HashSet<Fix>();
            MeterReadings = new HashSet<MeterReading>();
            Status = 0; // Mặc định là phòng trống
            Price = 0;
        }
    }
}
