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
        public ICommand BillCommand { get; set; }
        public ICommand UserCommand { get; set; }
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
            BillCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    BillWindow billWindow = new BillWindow();
                    billWindow.ShowDialog();
                });
            UserCommand = new RelayCommand<UserControl>(
                (p) => { return true; },
                (p) =>
                {
                    UserWindow userWindow = new UserWindow();
                    userWindow.ShowDialog();
                });
        }
    }
}
