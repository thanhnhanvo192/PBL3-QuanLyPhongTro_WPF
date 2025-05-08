using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum RoomFilterStatus
    {
        Vacant,
        Occupied,
        All
    }
    public class RoomFilterOptionDisplay
    {
        public RoomFilterStatus Value { get; set; }
        public string DisplayName { get; set; }
    }
}
