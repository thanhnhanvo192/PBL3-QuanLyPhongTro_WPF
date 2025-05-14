using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;
using System.Windows.Input;
using System.Linq.Expressions;
using System.Windows;

namespace QuanLyPhongTro.ViewModel
{
    public class MeterReadingViewModel : BaseViewModel
    {
        private ObservableCollection<MeterReading> _MeterReadings;
        private ObservableCollection<Room> _Rooms;
        private ObservableCollection<Service> _Services;
        public ObservableCollection<MeterReading> MeterReadings
        {
            get { return _MeterReadings; }
            set
            {
                _MeterReadings = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Room> Rooms
        {
            get { return _Rooms; }
            set
            {
                _Rooms = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Service> Services
        {
            get { return _Services; }
            set
            {
                _Services = value;
                OnPropertyChanged();
            }
        }
        private Room _SelectedRoom;
        private Service _SelectedService;
        private decimal _ReadingValue;
        private string _ReadingDate;
        private string _Notes;
        public Room SelectedRoom
        {
            get { return _SelectedRoom; }
            set
            {
                _SelectedRoom = value;
                OnPropertyChanged();
            }
        }
        public Service SelectedService
        {
            get { return _SelectedService; }
            set
            {
                _SelectedService = value;
                OnPropertyChanged();
            }
        }
        public decimal ReadingValue
        {
            get { return _ReadingValue; }
            set
            {
                _ReadingValue = value;
                OnPropertyChanged();
            }
        }
        public DateTime ReadingDate
        {
            get { return DateTime.Parse(_ReadingDate); }
            set
            {
                _ReadingDate = value.ToString("yyyy-MM-dd");
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddMeterReadingCommand { get; set; }
        public ICommand UpdateMeterReadingCommand { get; set; }
        public MeterReadingViewModel()
        {
            Rooms = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.ToList());
            Services = new ObservableCollection<Service>(DataProvider.Ins.DB.Services.ToList());
            MeterReadings = new ObservableCollection<MeterReading>(DataProvider.Ins.DB.MeterReadings.ToList());
            ReadingDate = DateTime.Now;

            AddMeterReadingCommand = new RelayCommand<object>((p) =>
            {
                return SelectedRoom != null && SelectedService != null && ReadingValue != null;
            },
            (p) =>
            {
                var newReading = new MeterReading
                {
                    RoomId = SelectedRoom.Id,
                    ServiceId = SelectedService.Id,
                    ReadingDate = ReadingDate,
                    ReadingValue = ReadingValue
                };
                if (newReading.ReadingValue < 0)
                {
                    MessageBox.Show("Chỉ số không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (newReading.ReadingDate > DateTime.Now)
                {
                    MessageBox.Show("Ngày đọc chỉ số không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (DataProvider.Ins.DB.MeterReadings.Any(m => m.RoomId == newReading.RoomId && m.ServiceId == newReading.ServiceId && m.ReadingDate.Month == newReading.ReadingDate.Month && m.ReadingDate.Year == newReading.ReadingDate.Year))
                {
                    MessageBox.Show("Đã ghi chỉ số cho tháng này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                DataProvider.Ins.DB.MeterReadings.Add(newReading);
                DataProvider.Ins.DB.SaveChanges();
                MeterReadings.Add(newReading);

                MessageBox.Show("Đã thêm chỉ số!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }
    }
}
