using QuanLyPhongTro.Model; // Để sử dụng AppDbContext
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography; // Để hash mật khẩu (ví dụ đơn giản)
using System.Text;
using System.Windows.Controls;
using QuanLyPhongTro.Enum;
using System.Collections.ObjectModel;
using QuanLyPhongTro.Dialog; // Để làm việc với Encoding

namespace QuanLyPhongTro.ViewModel
{
    public class RoomViewModel : BaseViewModel
    {
        private RoomFilterOptionDisplay _SelectedFilterStatus;
        public RoomFilterOptionDisplay SelectedFilterStatus
        {
            get { return _SelectedFilterStatus; }
            set
            {
                _SelectedFilterStatus = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<RoomFilterOptionDisplay> _FilterOptions;
        public ObservableCollection<Room> _RoomList;
        public ObservableCollection<RoomFilterOptionDisplay> FilterOptions
        {
            get { return _FilterOptions; }
            set
            {
                _FilterOptions = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Room> RoomList
        {
            get { return _RoomList; }
            set
            {
                _RoomList = value;
                OnPropertyChanged();
            }
        }

        private string _RoomNumber;
        private decimal _Price;
        private decimal _Area;
        private int _MaxOccupants;
        private int _Floor;
        private string _DisplayStatus;
        private string _Utilities;
        private string _Description;
        private string _RoomNumberSearch;
        public string RoomNumber
        {
            get { return _RoomNumber; }
            set
            {
                _RoomNumber = value;
                OnPropertyChanged();
            }
        }
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }
        public decimal Area
        {
            get { return _Area; }
            set
            {
                _Area = value;
                OnPropertyChanged();
            }
        }
        public int MaxOccupants
        {
            get { return _MaxOccupants; }
            set
            {
                _MaxOccupants = value;
                OnPropertyChanged();
            }
        }
        public int Floor
        {
            get { return _Floor; }
            set
            {
                _Floor = value;
                OnPropertyChanged();
            }
        }
        public string Utilities
        {
            get { return _Utilities; }
            set
            {
                _Utilities = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }
        public string RoomNumberSearch
        {
            get { return _RoomNumberSearch; }
            set
            {
                _RoomNumberSearch = value;
                OnPropertyChanged();
            }
        }

        private Room _SelectedItem;
        public Room SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    RoomNumber = SelectedItem.RoomNumber;
                    Price = SelectedItem.Price;
                    Area = SelectedItem.Area ?? 0;
                    MaxOccupants = SelectedItem.MaxOccupants ?? 0;
                    Floor = SelectedItem.Floor ?? 0;
                    Utilities = SelectedItem.Utilities;
                    Description = SelectedItem.Description;
                    SelectedFilterStatus = FilterOptions.FirstOrDefault(x => x.Value == SelectedItem.Status);
                }
            }
        }
        public string DisplayStatus
        {
            get { return _DisplayStatus; }
            set
            {
                _DisplayStatus = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand AddRoomCommand { get; set; }
        public ICommand AddNewRoomCommand { get; set; }
        public ICommand UpdateRoomCommand { get; set; }
        public ICommand SearchRoomCommand { get; set; }
        public RoomViewModel()
        {
            RoomList = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.ToList());
            FilterOptions = RoomFilterOption.GetRoomFilterOptions();
            SelectedFilterStatus = RoomFilterOption.GetRoomFilterOptions().First();
            AddRoomCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var addRoomWindow = new AddRoomWindow{};
               addRoomWindow.ShowDialog();
           });
            UpdateRoomCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var room = DataProvider.Ins.DB.Rooms.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var roomList = DataProvider.Ins.DB.Rooms.Where(x => x.RoomNumber == RoomNumber && RoomNumber != SelectedItem.RoomNumber);
                if (roomList == null || roomList.Count() != 0)
                {
                    MessageBox.Show("Số phòng đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (room != null)
                {
                    room.RoomNumber = RoomNumber;
                    room.Price = Price;
                    room.Area = Area;
                    room.MaxOccupants = MaxOccupants;
                    room.Floor = Floor;
                    room.Utilities = Utilities;
                    room.Description = Description;
                    room.Status = SelectedFilterStatus.Value;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.RoomNumber = RoomNumber;
                    SelectedItem.Price = Price;
                    SelectedItem.Area = Area;
                    SelectedItem.MaxOccupants = MaxOccupants;
                    SelectedItem.Floor = Floor;
                    SelectedItem.Utilities = Utilities;
                    SelectedItem.Description = Description;
                    SelectedItem.Status = SelectedFilterStatus.Value;
                    OnPropertyChanged();
                }
            });
            SearchRoomCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var allRooms = DataProvider.Ins.DB.Rooms.ToList();
                var filteredRooms = SelectedFilterStatus?.Value == RoomFilterStatus.All ? allRooms : allRooms.Where(x => x.Status == SelectedFilterStatus.Value).ToList();
                if (!string.IsNullOrEmpty(RoomNumberSearch))
                {
                    var room = filteredRooms.Where(x => x.RoomNumber.Contains(RoomNumberSearch)).FirstOrDefault();
                    if (room != null)
                    {
                        RoomList.Clear();
                        RoomList.Add(room);
                        SelectedItem = room;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    RoomList.Clear();
                    foreach (var room in filteredRooms)
                    {
                        RoomList.Add(room);
                    }
                }
            });
            AddNewRoomCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(RoomNumber))
                    return false;
                if (Price <= 0)
                    return false;
                if (decimal.TryParse(Price.ToString(), out decimal price) == false)
                    return false;
                return true;
            },
           (p) =>
           {
               var existingRoom = DataProvider.Ins.DB.Rooms.FirstOrDefault(x => x.RoomNumber == RoomNumber);
               if (existingRoom != null)
               {
                   MessageBox.Show("Số phòng đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                   return;
               }
               Room newRoom = new Room()
               {
                   RoomNumber = RoomNumber,
                   Price = Price,
                   Area = Area,
                   MaxOccupants = MaxOccupants,
                   Floor = Floor,
                   Utilities = Utilities
               };
               DataProvider.Ins.DB.Rooms.Add(newRoom);
               DataProvider.Ins.DB.SaveChanges();
               MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
               RoomList.Add(newRoom);
               var dep = p as DependencyObject;
               if (dep != null)
               {
                   var window = Window.GetWindow(dep);
                   if (window != null)
                       window.Close();
               }
           });
        }
    }
}
