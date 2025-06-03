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
        public ICommand AddNewTenantCommand { get; set; }
        public ICommand UpdateTenantCommand { get; set; }
        public ICommand SearchTenantCommand { get; set; }
        public ICommand DeleteTenantComamnd { get; set; }
        public TenantViewModel()
        {
            TenantList = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.Where(t => t.IsDeleted == false).ToList());
            ListSexOption = SexOptions.GetSexEnums();
            SelectedSex = ListSexOption.FirstOrDefault();
            AddTenantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var addTenantWindow = new AddTenantWindow{};
               addTenantWindow.ShowDialog();
           });

            UpdateTenantCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var Tenant = DataProvider.Ins.DB.Tenants.Where(x => x.IsDeleted == false && x.Id == SelectedItem.Id).SingleOrDefault();
                var TenantList = DataProvider.Ins.DB.Tenants.Where(x => x.IsDeleted == false && x.CCCD == CCCD && CCCD != SelectedItem.CCCD && Phone != SelectedItem.Phone && Email != SelectedItem.Email);
                if (TenantList == null || TenantList.Count() == 0)
                {
                    MessageBox.Show("CCCD, SĐT hoặc Email đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    Tenant.IsDeleted = false;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.FirstName = FirstName;
                    SelectedItem.LastName = LastName;
                    SelectedItem.CCCD = CCCD;
                    SelectedItem.Birthday = Birthday;
                    SelectedItem.Phone = Phone;
                    SelectedItem.Email = Email;
                    SelectedItem.PermanentAddress = PermanentAddress;
                }
            });

            SearchTenantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var allTenants = DataProvider.Ins.DB.Tenants.Where(t => t.IsDeleted == false).ToList();
                if (!string.IsNullOrEmpty(TenantNameSearch))
                {
                    var Tenant = allTenants.Where(x => x.LastName.ToLower().Contains(TenantNameSearch.ToLower()) || x.FirstName.ToLower().Contains(TenantNameSearch.ToLower())).ToList();
                    if (Tenant.Count > 0)
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

            AddNewTenantCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(CCCD) || SelectedSex == null)
                    return false;
                return true;
            },
           (p) =>
           {
               var existingTenant = DataProvider.Ins.DB.Tenants.Where(t => t.IsDeleted == false).FirstOrDefault(x => x.CCCD == CCCD && CCCD != CCCD && Phone != Phone && Email != Email);
               if (existingTenant != null)
               {
                   MessageBox.Show("CCCD đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               Tenant newTenant = new Tenant()
               {
                   FirstName = FirstName,
                   LastName = LastName,
                   CCCD = CCCD,
                   PermanentAddress = PermanentAddress,
                   Email = Email,
                   Phone = Phone,
                   Birthday = Birthday,
                   Sex = SelectedSex.Value,
                   IsDeleted = false
               };
               DataProvider.Ins.DB.Tenants.Add(newTenant);
               DataProvider.Ins.DB.SaveChanges();
               MessageBox.Show("Thêm khách thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
               TenantList.Add(newTenant);
               var dep = p as DependencyObject;
               if (dep != null)
               {
                   var window = Window.GetWindow(dep);
                   if (window != null)
                       window.Close();
               }
           });

            DeleteTenantComamnd = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) => 
            {
                SelectedItem.IsDeleted = true;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xóa khách thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                TenantList = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.Where(t => t.IsDeleted == false).ToList());
            });
        }
    }
}
