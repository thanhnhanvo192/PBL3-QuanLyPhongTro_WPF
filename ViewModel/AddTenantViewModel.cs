using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using QuanLyPhongTro.Enum;
using System.Collections.ObjectModel;

namespace QuanLyPhongTro.ViewModel
{
    public class AddTenantViewModel : BaseViewModel
    {
        private SexOptionDisplay _SelectedSex;
        private ObservableCollection<SexOptionDisplay> _SexOptions;

        public SexOptionDisplay SelectedSex
        {
            get { return _SelectedSex; }
            set
            {
                _SelectedSex = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SexOptionDisplay> SexOptions
        {
            get { return _SexOptions; }
            set
            {
                _SexOptions = value;
                OnPropertyChanged();
            }
        }
        private string _FirstName;
        private string _LastName;
        private string _CCCD;
        private DateTime? _Birthday;
        private SexEnum _Sex;
        private string? _Phone;
        private string? _Email;
        private string? _PermanentAddress;
        private string FirstName
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
        public DateTime Birthday
        {
            get { return (DateTime)_Birthday; }
            set
            {
                _Birthday = value;
                OnPropertyChanged();
            }
        }
        public SexEnum Sex
        {
            get { return _Sex; }
            set
            {
                _Sex = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
        }
        public string PermanentAddress
        {
            get { return _PermanentAddress; }
            set
            {
                _PermanentAddress = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNewTenantCommand { get; set; }
        public AddTenantViewModel()
        {
            AddNewTenantCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(CCCD))
                    return false;
                return true;
            },
           (p) =>
           {
               var existingTenant = DataProvider.Ins.DB.Tenants.FirstOrDefault(x => x.CCCD == CCCD);
               if (existingTenant != null)
               {
                   MessageBox.Show("CCCD đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                   Sex = SexEnum.Male
               };
               DataProvider.Ins.DB.Tenants.Add(newTenant);
               DataProvider.Ins.DB.SaveChanges();
               MessageBox.Show("Thêm khách thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
 
