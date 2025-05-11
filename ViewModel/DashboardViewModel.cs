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
    public class DashboardViewModel : BaseViewModel
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
        public ICommand MainViewCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand RoomViewCommand { get; set; }
        public ICommand TenantViewCommand { get; set; }
        public ICommand InvoiceViewCommand { get; set; }
        public ICommand UserViewCommand { get; set; }
        public ICommand ContractViewCommand { get; set; }
        public ICommand ServiceViewCommand { get; set; }
        public ICommand UnitViewCommand { get; set; }
        public ICommand StatisticViewCommand { get; set; }
        public ICommand FixViewCommand { get; set; }
        public ICommand PunishViewCommand { get; set; }
        #endregion

        public DashboardViewModel()
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
                      MainViewCommand?.Execute(null);
                  }
                  else
                  {
                      p.Close();
                  }
              });

            MainViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  EmptyListRoom();
                  CountEmptyRoom();
                  CalculateLastMonthRevenue();
                  ListOutDateContract();
                  CountTotalOutDateContract();
                  CurrentView = new MainViewModel();
              });

            RoomViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new RoomViewModel();
              });

            TenantViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new TenantViewModel();
              });

            InvoiceViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new InvoiceViewModel();
              });

            UserViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new UserViewModel();
              });

            ContractViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new ContractViewModel();
              });

            ServiceViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new ServiceViewModel();
              });

            UnitViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new UnitViewModel();
              });

            StatisticViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new StatisticViewModel();
              });

            FixViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new FixViewModel();
              });

            PunishViewCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  CurrentView = new PunishViewModel();
              });

        }
        public void EmptyListRoom()
        {
            ListEmptyRoom = new ObservableCollection<EmptyRoomDisplay>();
            var list = DataProvider.Ins.DB.Rooms.ToList();
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
                int count = DataProvider.Ins.DB.Rooms.Count(room => room.Status == 1);
                TotalemtpyRoom = count;
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
                int lastMonth = DateTime.Now.Month - 1;
                int Year = DateTime.Now.Year;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    Year--;
                }
                LastRevenue = DataProvider.Ins.DB.Invoices.Where(invoice => invoice.CreateDate.Year == Year && invoice.CreateDate.Month == lastMonth && invoice.Status == 1).Sum(invoice => invoice.AmountPaid);
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
                var listContract = DataProvider.Ins.DB.Contracts.Where(contract => contract.EndDate.Year == year && contract.EndDate.Month == lastMonth).ToList();
                foreach (var item in listContract)
                {
                    Contract contract = new Contract();
                    contract.ContractCode = item.ContractCode;
                    contract.StartDate = item.StartDate;
                    contract.EndDate = item.EndDate;
                    ListContract.Add(contract);
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
                var year = DateTime.Now.Year;
                var lastMonth = DateTime.Now.Month - 1;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    year--;
                }
                TotalOutDateContract = DataProvider.Ins.DB.Contracts.Count(contract => contract.EndDate.Year == year && contract.EndDate.Month == lastMonth);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đếm số hợp đồng hết hạn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalOutDateContract = 0;
            }
        }
    }
}