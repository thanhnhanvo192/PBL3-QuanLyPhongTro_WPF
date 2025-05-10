using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyPhongTro.Model;
using QuanLyPhongTro.View;

namespace QuanLyPhongTro.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private int _totalEmptyRoom;
        private Decimal _lastRevenue;
        private int _totalOutDateContract;
        private ObservableCollection<EmptyRoomDisplay> _ListEmptyRoom;
        private ObservableCollection<Contract> _ListContract;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public int TotalemtpyRoom
        {
            get => _totalEmptyRoom;
            set
            {
                _totalEmptyRoom = value;
                OnPropertyChanged();
            }
        }
        public Decimal LastRevenue
        {
            get => _lastRevenue;
            set
            {
                _lastRevenue = value;
                OnPropertyChanged();
            }
        }
        public int TotalOutDateContract
        {
            get => _totalOutDateContract;
            set
            {
                _totalOutDateContract = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EmptyRoomDisplay> ListEmptyRoom
        {
            get => _ListEmptyRoom;
            set
            {
                _ListEmptyRoom = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Contract> ListContract
        {
            get => _ListContract;
            set
            {
                _ListContract = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoaded = false;

        #region
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand RoomWindowCommand { get; set; }
        public ICommand TenantWindowCommand { get; set; }
        public ICommand InvoiceViewCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand ContractViewCommand { get; set; }
        public ICommand ServiceWindowCommand { get; set; }
        public ICommand UnitWindowCommand { get; set; }
        public ICommand StatisticWindowCommand { get; set; }
        public ICommand FixViewCommand { get; set; }
        public ICommand PunishWindowCommand { get; set; }
        #endregion

        public MainViewModel()
        {

            LoadedWindowCommand = new RelayCommand<Window>(
              (p) => { return true; },
              (p) =>
              {
                  IsLoaded = true;
                  if (p == null) return;
                  p.Hide();
                  LoginWindow loginWindow = new LoginWindow();
                  loginWindow.ShowDialog();

                  if (loginWindow.DataContext == null) return;
                  var loginVM = loginWindow.DataContext as LoginViewModel;
                  if (loginVM.IsLogin == true)
                  {
                      p.Show();
                      EmptyListRoom();
                      CountEmptyRoom();
                      CalculateLastMonthRevenue();
                      ListOutDateContract();
                      CountTotalOutDateContract();
                  }
                  else
                  {
                      p.Close();
                  }
              });

            RoomWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  RoomWindow roomWindow = new RoomWindow();
                  roomWindow.ShowDialog();
              });

            TenantWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  TenantWindow tenantWindow = new TenantWindow();
                  tenantWindow.ShowDialog();
              });

            InvoiceViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new InvoiceView();
              });

            UserCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  UserWindow userWindow = new UserWindow();
                  userWindow.ShowDialog();
              });

            ContractViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new ContractView();
              });

            ServiceWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  ServiceWindow serviceWindow = new ServiceWindow();
                  serviceWindow.ShowDialog();
              });

            UnitWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  UnitWindow unitWindow = new UnitWindow();
                  unitWindow.ShowDialog();
              });

            StatisticWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  StatisticWindow statisticWindow = new StatisticWindow();
                  statisticWindow.ShowDialog();
              });

            FixViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new FixView();
              });

            PunishWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  PunishWindow punishWindow = new PunishWindow();
                  punishWindow.ShowDialog();
              });

        }
        public void EmptyListRoom()
        {
            ListEmptyRoom = new ObservableCollection<EmptyRoomDisplay>();
            var db = new AppDbContext();
            var list = db.Rooms.ToList();
            int stt = 1;
            foreach (var item in list)
            {
                if (item.Status == 0)
                {
                    EmptyRoomDisplay emptyRoomDisplay = new EmptyRoomDisplay();
                    emptyRoomDisplay.STT = stt;
                    emptyRoomDisplay.RoomNumber = item.RoomNumber;
                    stt++;
                    ListEmptyRoom.Add(emptyRoomDisplay);
                }
            }
        }
        public void CountEmptyRoom()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    int count = db.Rooms.Count(room => room.Status == 1);
                    TotalemtpyRoom = count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đếm số phòng trống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalemtpyRoom = 0;
            }
        }
        public void CalculateLastMonthRevenue()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    int lastMonth = DateTime.Now.Month - 1;
                    int Year = DateTime.Now.Year;
                    if (lastMonth == 0)
                    {
                        lastMonth = 12;
                        Year--;
                    }
                    LastRevenue = db.Invoices.Where(invoice => invoice.CreateDate.Year == Year && invoice.CreateDate.Month == lastMonth && invoice.Status == 1).Sum(invoice => invoice.AmountPaid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tính doanh thu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                LastRevenue = 0;
            } 
        }
        public void ListOutDateContract()
        {
            try
            {
                ListContract = new ObservableCollection<Contract>();
                var year = DateTime.Now.Year;
                var lastMonth = DateTime.Now.Month - 1;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    year--;
                }
                using (var db = new AppDbContext())
                {
                    var listContract = db.Contracts.Where(contract => contract.EndDate.Year == year && contract.EndDate.Month == lastMonth).ToList();
                    foreach (var item in listContract)
                    {
                        Contract contract = new Contract();
                        contract.ContractCode = item.ContractCode;
                        contract.StartDate = item.StartDate;
                        contract.EndDate = item.EndDate;
                        ListContract.Add(contract);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lấy danh sách hợp đồng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                ListContract = new ObservableCollection<Contract>();
            }
        }
        public void CountTotalOutDateContract()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var year = DateTime.Now.Year;
                    var lastMonth = DateTime.Now.Month - 1;
                    if (lastMonth == 0)
                    {
                        lastMonth = 12;
                        year--;
                    }
                    TotalOutDateContract = db.Contracts.Count(contract => contract.EndDate.Year == year && contract.EndDate.Month == lastMonth);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đếm số hợp đồng hết hạn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalOutDateContract = 0;
            }
        }
    }
}