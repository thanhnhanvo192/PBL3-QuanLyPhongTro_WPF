using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;
using System.Windows.Input;
using System.Windows;
using QuanLyPhongTro.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace QuanLyPhongTro.ViewModel
{
    public class AddFixViewModel : BaseViewModel
    {
        private int _Id;
        private int _RoomID;
        private int? _TenantID;
        private DateTime _FixDate;
        private string _Description;
        private decimal _Cost;
        private FaultType _WhoFault;
        private int? _InvoiceId;
        public int Id
        {
            get => _Id;
            set
            {
                _Id = value;
                OnPropertyChanged();
            }
        }
        public int RoomID
        {
            get => _RoomID;
            set
            {
                _RoomID = value;
                OnPropertyChanged();
            }
        }
        public int? TenantID
        {
            get => _TenantID;
            set
            {
                _TenantID = value;
                OnPropertyChanged();
            }
        }
        public DateTime FixDate
        {
            get => _FixDate;
            set
            {
                _FixDate = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                OnPropertyChanged();
            }
        }
        public decimal Cost
        {
            get => _Cost;
            set
            {
                _Cost = value;
                OnPropertyChanged();
            }
        }
        public FaultType WhoFault
        {
            get => _WhoFault;
            set
            {
                _WhoFault = value;
                OnPropertyChanged();
            }
        }
        public int? InvoiceId
        {
            get => _InvoiceId;
            set
            {
                _InvoiceId = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<FaultTypeDisplay> _FaultTypes;
        public ObservableCollection<FaultTypeDisplay> FaultTypes
        {
            get => _FaultTypes;
            set
            {
                _FaultTypes = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Room> _Rooms;
        public ObservableCollection<Room> Rooms
        {
            get => _Rooms;
            set
            {
                _Rooms = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Tenant> _Tenants;
        public ObservableCollection<Tenant> Tenants
        {
            get => _Tenants;
            set
            {
                _Tenants = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Invoice> _Invoices;
        public ObservableCollection<Invoice> Invoices
        {
            get => _Invoices;
            set
            {
                _Invoices = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Fix> _Fixes;
        public ObservableCollection<Fix> Fixes
        {
            get => _Fixes;
            set
            {
                _Fixes = value;
                OnPropertyChanged();
            }
        }
        private Room _SelectedRoom;
        public Room SelectedRoom
        {
            get => _SelectedRoom;
            set
            {
                _SelectedRoom = value;
                OnPropertyChanged();
            }
        }
        private Tenant _SelectedTenant;
        public Tenant SelectedTenant
        {
            get => _SelectedTenant;
            set
            {
                _SelectedTenant = value;
                OnPropertyChanged();
            }
        }
        private Invoice _SelectedInvoice;
        public Invoice SelectedInvoice
        {
            get => _SelectedInvoice;
            set
            {
                _SelectedInvoice = value;
                OnPropertyChanged();
            }
        }
        private FaultTypeDisplay _SelectedFaultType;
        public FaultTypeDisplay SelectedFaultType
        {
            get => _SelectedFaultType;
            set
            {
                _SelectedFaultType = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddNewFixCommand { get; set; }
        public AddFixViewModel()
        {
            FaultTypes = FaultOptions.GetFaultTypes();
            Rooms = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.ToList());
            Tenants = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.ToList());
            Invoices = new ObservableCollection<Invoice>(DataProvider.Ins.DB.Invoices.ToList());
            Fixes = new ObservableCollection<Fix>(DataProvider.Ins.DB.Fixes.ToList());
            AddNewFixCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Description) || SelectedFaultType == null || SelectedRoom == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var newFix = new Fix
                {
                    RoomID = SelectedRoom.Id,
                    TenantID = SelectedTenant?.Id,
                    Description = Description,
                    Cost = Cost,
                    FixDate = FixDate,
                    WhoFault = SelectedFaultType.Value,
                    InvoiceId = SelectedInvoice?.Id,
                };
                Id = newFix.Id;

                DataProvider.Ins.DB.Fixes.Add(newFix);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
