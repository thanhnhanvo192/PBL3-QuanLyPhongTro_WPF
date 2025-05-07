using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyPhongTro.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
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
            LoadedWindowCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    IsLoaded = true;
                    LoginWindow loginWindow = new LoginWindow();
                    loginWindow.ShowDialog();
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
    }
}
