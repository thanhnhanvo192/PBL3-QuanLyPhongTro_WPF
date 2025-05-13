using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;
using System.Windows.Input;
using System.Windows;

namespace QuanLyPhongTro.ViewModel
{
    public class TenantViewModel : BaseViewModel
    {
        private SexOptionDisplay _SelectedSex;
        private ObservableCollection<SexOptionDisplay> _ListSexOption;

        public SexOptionDisplay SelectedSex
        {
            get { return _SelectedSex; }
            set
            {
                _SelectedSex = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SexOptionDisplay> ListSexOption
        {
            get { return _ListSexOption; }
            set
            {
                _ListSexOption = value;
                OnPropertyChanged();
            }
        }
        private string _FirstName;
        private string _LastName;
        private string _CCCD;
        private SexEnum Sex;
        private DateTime? _Birthday;
        private string? _Phone;
        private string? _Email;
        private string? _PermanentAddress;
        private string _TenantNameSearch;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        public string CCCD
        {
            get { return _CCCD; }
            set
            {
                _CCCD = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set
            {
                _Birthday = value;
                OnPropertyChanged();
            }
        }
        public string? Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }
        public string? Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }
        public string? PermanentAddress
        {
            get { return _PermanentAddress; }
            set
            {
                _PermanentAddress = value;
                OnPropertyChanged();
            }
        }
        public string TenantNameSearch
        {
            get { return _TenantNameSearch; }
            set
            {
                _TenantNameSearch = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Tenant> _TenantList;
        public ObservableCollection<Tenant> TenantList
        {
            get { return _TenantList; }
            set
            {
                _TenantList = value;
                OnPropertyChanged();
            }
        }
        private Tenant _SelectedItem;
        public Tenant SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    FirstName = SelectedItem.FirstName;
                    LastName = SelectedItem.LastName;
                    CCCD = SelectedItem.CCCD;
                    Birthday = SelectedItem.Birthday;
                    Phone = SelectedItem.Phone;
                    Email = SelectedItem.Email;
                    SelectedSex = ListSexOption.FirstOrDefault(x => x.Value == SelectedItem.Sex);
                    PermanentAddress = SelectedItem.PermanentAddress;
                }
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand AddTenantCommand { get; set; }
        public ICommand UpdateTenantCommand { get; set; }
        public ICommand SearchTenantCommand { get; set; }
        public TenantViewModel()
        {
            TenantList = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.ToList());
            ListSexOption = SexOptions.GetSexEnums();
            SelectedSex = ListSexOption.FirstOrDefault();
            AddTenantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var viewModel = new AddTenantViewModel();
               var addTenantWindow = new AddTenantWindow
               {
                   DataContext = viewModel
               };

               addTenantWindow.ShowDialog();
               var Tenant = DataProvider.Ins.DB.Tenants.FirstOrDefault(x => x.CCCD == viewModel.CCCD);
               if (Tenant != null && !TenantList.Contains(Tenant))
               {
                   TenantList.Add(Tenant);
               }
           });
            UpdateTenantCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var Tenant = DataProvider.Ins.DB.Tenants.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var TenantList = DataProvider.Ins.DB.Tenants.Where(x => x.CCCD == CCCD && CCCD != SelectedItem.CCCD);
                if (TenantList == null || TenantList.Count() != 0)
                {
                    MessageBox.Show("CCCD đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (Tenant != null)
                {
                    Tenant.FirstName = FirstName;
                    Tenant.LastName = LastName;
                    Tenant.CCCD = CCCD;
                    Tenant.Birthday = Birthday;
                    Tenant.Phone = Phone;
                    Tenant.Email = Email;
                    Tenant.PermanentAddress = PermanentAddress;
                    Tenant.Sex = SelectedSex.Value;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.FirstName = FirstName;
                    SelectedItem.LastName = LastName;
                    SelectedItem.CCCD = CCCD;
                    SelectedItem.Birthday = Birthday;
                    SelectedItem.Phone = Phone;
                    SelectedItem.Email = Email;
                    SelectedItem.PermanentAddress = PermanentAddress;
                    OnPropertyChanged();
                }
            });
            SearchTenantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var allTenants = DataProvider.Ins.DB.Tenants.ToList();
                if (!string.IsNullOrEmpty(TenantNameSearch))
                {
                    var Tenant = allTenants.Where(x => x.LastName.Contains(TenantNameSearch) || x.FirstName.Contains(TenantNameSearch)).ToList();
                    if (Tenant != null)
                    {
                        TenantList.Clear();
                        foreach (var item in Tenant)
                        {
                            TenantList.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    TenantList.Clear();
                    foreach (var item in allTenants)
                    {
                        TenantList.Add(item);
                    }
                }
            });
        }
    }
}
