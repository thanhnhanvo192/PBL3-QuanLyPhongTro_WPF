using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Model
{
    [NotMapped]
    public class RevenueStatistic
    {
        public string RoomNumber { get; set; }
        public decimal TotalRevenue { get; set; }
        public int InvoiceCount { get; set; }
    }
}
