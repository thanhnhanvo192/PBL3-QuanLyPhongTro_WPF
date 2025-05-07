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



namespace QuanLyPhongTro.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<EmptyRoomDisplay> _ListRoom;
        public ObservableCollection<EmptyRoomDisplay> ListRoom
        {
            get => _ListRoom;
            set
            {
                _ListRoom = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoaded = false;

        #region
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand RoomWindowCommand { get; set; }
        public ICommand TenantWindowCommand { get; set; }
        public ICommand InvoiceCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand ContractWindowCommand { get; set; }
        public ICommand ServiceWindowCommand { get; set; }
        public ICommand UnitWindowCommand { get; set; }
        public ICommand StatisticWindowCommand { get; set; }
        public ICommand FixWindowCommand { get; set; }
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

            InvoiceCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  InvoiceWindow invoiceWindow = new InvoiceWindow();
                  invoiceWindow.ShowDialog();
              });

            UserCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  UserWindow userWindow = new UserWindow();
                  userWindow.ShowDialog();
              });

            ContractWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  ContractWindow contractWindow = new ContractWindow();
                  contractWindow.ShowDialog();
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

            FixWindowCommand = new RelayCommand<UserControl>(
              (p) => { return true; },
              (p) =>
              {
                  FixWindow fixWindow = new FixWindow();
                  fixWindow.ShowDialog();
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
            ListRoom = new ObservableCollection<EmptyRoomDisplay>();
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
                    ListRoom.Add(emptyRoomDisplay);
                }
            }
        }
    }
}