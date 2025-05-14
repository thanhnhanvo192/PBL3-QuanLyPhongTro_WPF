using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class AddRoomViewModel : BaseViewModel
    {
        private string _RoomNumber;
        private decimal _Price;
        private decimal _Area;
        private int _MaxOccupants;
        private int _Floor;
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
        public ICommand AddNewRoomCommand { get; set; }
        public AddRoomViewModel()
        {
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
