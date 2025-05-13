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
               var viewModel = new AddRoomViewModel();
               var addRoomWindow = new AddRoomWindow
               {
                   DataContext = viewModel
               };

               addRoomWindow.ShowDialog();
               var room = DataProvider.Ins.DB.Rooms.FirstOrDefault(x => x.RoomNumber == viewModel.RoomNumber);
               if (room != null && !RoomList.Contains(room))
               {
                   RoomList.Add(room);
               }
           });
        }
    }
}
