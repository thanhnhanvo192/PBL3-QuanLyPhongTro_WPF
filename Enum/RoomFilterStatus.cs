using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum RoomFilterStatus
    {
        All,
        Vacant,
        Occupied,
        Fixing
    }
    public class RoomFilterOptionDisplay
    {
        public RoomFilterStatus Value { get; set; }
        public string DisplayName { get; set; }
    }
    public static class RoomFilterOption
    {
        public static ObservableCollection<RoomFilterOptionDisplay> GetRoomFilterOptions()
        {
            return new ObservableCollection<RoomFilterOptionDisplay>
            {
                new RoomFilterOptionDisplay { Value = RoomFilterStatus.All, DisplayName = "Tất cả" },
                new RoomFilterOptionDisplay { Value = RoomFilterStatus.Vacant, DisplayName = "Phòng trống" },
                new RoomFilterOptionDisplay { Value = RoomFilterStatus.Occupied, DisplayName = "Phòng đã thuê" },
                new RoomFilterOptionDisplay { Value = RoomFilterStatus.Fixing, DisplayName = "Đang sữa chữa" }
            };
        }

    }
}
