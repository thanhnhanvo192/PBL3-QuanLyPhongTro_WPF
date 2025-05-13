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
        public bool IsLoaded = false;
        #region
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand MainViewCommand { get; set; }
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
                      CurrentView = new MainViewModel();
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
    }
}