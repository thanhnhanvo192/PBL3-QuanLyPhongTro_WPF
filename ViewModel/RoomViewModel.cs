using QuanLyPhongTro.Model; // Để sử dụng AppDbContext
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography; // Để hash mật khẩu (ví dụ đơn giản)
using System.Text;
using System.Windows.Controls;
using QuanLyPhongTro.Enum;
using System.Collections.ObjectModel; // Để làm việc với Encoding

namespace QuanLyPhongTro.ViewModel
{
    public class RoomViewModel : BaseViewModel
    {
        private RoomFilterStatus _selectedFilterStatus;
        public RoomFilterStatus SelectedFilterStatus
        {
            get { return _selectedFilterStatus; }
            set
            {
                _selectedFilterStatus = value;
                OnPropertyChanged();
            }
        }
        public List<RoomFilterOptionDisplay> FilterOptions { get; set; }
        public ObservableCollection<Room> _roomList;
        public ObservableCollection<Room> RoomList
        {
            get { return _roomList; }
            set
            {
                _roomList = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }
        //public RoomViewModel()
        //{
        //    FilterOptions = new List<RoomFilterOptionDisplay>
        //    {
        //        new RoomFilterOptionDisplay { Value = RoomFilterStatus.Vacant, DisplayName = "Phòng trống" },
        //        new RoomFilterOptionDisplay { Value = RoomFilterStatus.Occupied, DisplayName = "Phòng đã thuê" },
        //        new RoomFilterOptionDisplay { Value = RoomFilterStatus.All, DisplayName = "Tất cả phòng" }
        //    };
        //    SelectedFilterStatus = RoomFilterStatus.All;
        //    SearchCommand = new RelayCommand<object>((p) => true, (p) => SearchRoom());
        //}
        //private void SearchRoom()
        //{
        //    using (var context = new AppDbContext())
        //    {
        //        var query = context.Rooms.AsQueryable();
        //        // Lọc theo trạng thái phòng
        //        switch (SelectedFilterStatus)
        //        {
        //            case RoomFilterStatus.Vacant:
        //                query = query.Where(r => r.IsVacant == true);
        //                break;
        //            case RoomFilterStatus.Occupied:
        //                query = query.Where(r => r.IsVacant == false);
        //                break;
        //            case RoomFilterStatus.All:
        //                break;
        //        }
        //        // Cập nhật danh sách phòng
        //        RoomList = new ObservableCollection<Room>(query.ToList());
        //    }
        //}
    }
}
