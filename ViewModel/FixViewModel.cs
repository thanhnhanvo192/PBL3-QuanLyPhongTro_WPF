using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class FixViewModel : BaseViewModel
    {
        private ObservableCollection<Fix> _Fixes;
        private ObservableCollection<Fix> _AllFixes;
        private ObservableCollection<Tenant> _Tenants;
        private ObservableCollection<Room> _Rooms;
        private ObservableCollection<Invoice> _InvoiceCurrentMonth;
        private ObservableCollection<FaultTypeDisplay> _FaultTypes;
        private ObservableCollection<Contract> _Contracts;
        private ObservableCollection<Room> _RoomOccupiedList;
        private ObservableCollection<Service> _Services;
        private Service _SelectedService;
        private DateTime? _SearchDateFrom;
        private DateTime? _SearchDateTo;

        public ObservableCollection<Contract> Contracts
        {
            get => _Contracts;
            set { _Contracts = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Fix> Fixes
        {
            get => _Fixes;
            set { _Fixes = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Fix> AllFixes
        {
            get => _AllFixes;
            set { _AllFixes = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Tenant> Tenants
        {
            get => _Tenants;
            set { _Tenants = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Room> Rooms
        {
            get => _Rooms;
            set { _Rooms = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Invoice> InvoiceCurrentMonth
        {
            get => _InvoiceCurrentMonth;
            set { _InvoiceCurrentMonth = value; OnPropertyChanged(); }
        }
        public ObservableCollection<FaultTypeDisplay> FaultTypes
        {
            get => _FaultTypes;
            set { _FaultTypes = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Room> RoomOccupiedList
        {
            get => _RoomOccupiedList;
            set { _RoomOccupiedList = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Service> Services
        {
            get => _Services;
            set { _Services = value; OnPropertyChanged(); }
        }
        public Service SelectedService
        {
            get => _SelectedService;
            set { _SelectedService = value; OnPropertyChanged(); }
        }
        public DateTime? SearchDateFrom
        {
            get => _SearchDateFrom;
            set { _SearchDateFrom = value; OnPropertyChanged(); }
        }
        public DateTime? SearchDateTo
        {
            get => _SearchDateTo;
            set { _SearchDateTo = value; OnPropertyChanged(); }
        }
        private Fix _SelectedFix;
        public Fix SelectedFix
        {
            get => _SelectedFix;
            set
            {
                _SelectedFix = value;
                OnPropertyChanged();
                if (SelectedFix != null)
                {
                    Description = SelectedFix.Description;
                    Cost = SelectedFix.Cost;
                    FixDate = SelectedFix.FixDate;
                    SelectedFaultType = FaultTypes.FirstOrDefault(x => x.Value == SelectedFix.WhoFault);
                    var room = DataProvider.Ins.DB.Rooms.FirstOrDefault(r => r.Id == SelectedFix.RoomID);
                    if (room != null)
                        SelectedRoom = room;
                    SelectedTenant = Tenants.FirstOrDefault(t => t.Id == SelectedFix.TenantID);
                    var invoice = DataProvider.Ins.DB.Invoices.FirstOrDefault(i => i.Id == SelectedFix.InvoiceId);
                    if (invoice != null)
                        SelectedInvoice = invoice;
                }
            }
        }
        private Room _SelectedRoomOccupied;
        public Room SelectedRoomOccupied
        {   get => _SelectedRoomOccupied;
            set
            {
                _SelectedRoomOccupied = value;
                OnPropertyChanged();
                if (SelectedRoomOccupied != null)
                {
                    var tenant = DataProvider.Ins.DB.Tenants.FirstOrDefault(t => t.Contracts.Any(c => c.RoomId == SelectedRoomOccupied.Id));
                    if (tenant != null)
                        SelectedTenant = tenant;
                    var invoice = InvoiceCurrentMonth.FirstOrDefault(i => i.Contract.RoomId == SelectedRoomOccupied.Id);
                    if (invoice != null)
                        SelectedInvoice = invoice;
                    else
                    {
                        MessageBox.Show("Vui lòng tạo hoá đơn phòng " + SelectedRoomOccupied.RoomNumber + " cho tháng này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        SelectedInvoice = null;
                        return;
                    } 
                        
                }
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

        private string _Description;
        public string Description
        {
            get => _Description;
            set { _Description = value; OnPropertyChanged(); }
        }

        private decimal _Cost;
        public decimal Cost
        {
            get => _Cost;
            set { _Cost = value; OnPropertyChanged(); }
        }

        private DateTime _FixDate = DateTime.Now;
        public DateTime FixDate
        {
            get => _FixDate;
            set { _FixDate = value; OnPropertyChanged(); }
        }

        private FaultTypeDisplay _SelectedFaultType;
        public FaultTypeDisplay SelectedFaultType
        {
            get => _SelectedFaultType;
            set { _SelectedFaultType = value; OnPropertyChanged(); }
        }

        public ICommand AddFixCommand { get; set; }
        public ICommand AddNewFixCommand { get; set; }
        public ICommand UpdateFixCommand { get; set; }
        public ICommand SearchFixCommadn { get; set; }
        public ICommand DeleteFixCommand { get; set; }

        public FixViewModel()
        {
            Fixes = new ObservableCollection<Fix>(DataProvider.Ins.DB.Fixes.Where(f => f.IsDeleted == false).ToList());
            AllFixes = new ObservableCollection<Fix>(DataProvider.Ins.DB.Fixes.Where(f => f.IsDeleted == false).ToList());
            Rooms = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.Where(r => r.IsDeleted == false).ToList());
            RoomOccupiedList = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.Where(r => r.Status == RoomFilterStatus.Occupied && r.IsDeleted == false).ToList());
            Tenants = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.Include(t => t.Contracts).Where(t => t.IsDeleted == false).ToList());
            InvoiceCurrentMonth = new ObservableCollection<Invoice>(DataProvider.Ins.DB.Invoices.Where(i => i.CreateDate.Month == DateTime.Now.Month && i.CreateDate.Year == DateTime.Now.Year && i.Contract.Status == ContractStatus.Active && i.IsDeleted == false).ToList());
            FaultTypes = FaultOptions.GetFaultTypes();
            Services = new ObservableCollection<Service>(DataProvider.Ins.DB.Services.Where(s => s.IsDeleted == false).ToList());


            AddFixCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var addFixWindow = new AddFixWindow{};
               addFixWindow.ShowDialog();
           });

            UpdateFixCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedFix == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var fix = DataProvider.Ins.DB.Fixes.Where(x => x.IsDeleted == false && x.Id == SelectedFix.Id).SingleOrDefault();
                if (fix != null)
                {
                    fix.RoomID = SelectedRoom.Id;
                    fix.Room = SelectedRoom;
                    fix.TenantID = SelectedTenant.Id;
                    fix.Tenant = SelectedTenant;
                    fix.InvoiceId = SelectedInvoice.Id;
                    fix.Cost = Cost;
                    fix.WhoFault = SelectedFaultType.Value;
                    fix.Description = Description;
                    fix.FixDate = FixDate;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            AddNewFixCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(Description) || SelectedFaultType == null || SelectedRoomOccupied == null)
                    return false;
                return true;
            },
            (p) =>
            {
                if (SelectedInvoice == null)
                {
                    MessageBox.Show("Vui lòng tạo hoá đơn phòng " + SelectedRoomOccupied.RoomNumber + " cho tháng này!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SelectedInvoice = null;
                    return;
                }
                var newFix = new Fix
                {
                    RoomID = SelectedRoomOccupied.Id,
                    TenantID = SelectedTenant?.Id,
                    Description = Description,
                    Cost = Cost,
                    FixDate = FixDate,
                    WhoFault = SelectedFaultType.Value,
                    InvoiceId = SelectedInvoice?.Id,
                    IsDeleted = false
                };
                var invoice_details = new Invoice_detail
                {
                    InvoiceId = SelectedInvoice.Id,
                    ServiceId = SelectedService.Id,
                    Notes = "Chi phí sửa chữa"
                };

                DataProvider.Ins.DB.Fixes.Add(newFix);
                DataProvider.Ins.DB.Invoice_details.Add(invoice_details);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Thêm yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Fixes.Add(newFix);
                var dep = p as DependencyObject;
                if (dep != null)
                {
                    var window = Window.GetWindow(dep);
                    if (window != null)
                        window.Close();
                }
            });
            SearchFixCommadn = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    var fixFiltered = AllFixes.ToList();

                    if (SearchDateFrom != null)
                    {
                        fixFiltered = fixFiltered.Where(x => x.FixDate >= SearchDateFrom).ToList();
                    }
                    if (SearchDateTo != null)
                    {
                        fixFiltered = fixFiltered.Where(x => x.FixDate <= SearchDateTo).ToList();
                    }
                    if (SelectedFaultType != null)
                    {
                        fixFiltered = fixFiltered.Where(x => x.WhoFault == SelectedFaultType.Value).ToList();
                    }
                    Fixes.Clear();
                    Fixes = new ObservableCollection<Fix>(fixFiltered);
                });
            DeleteFixCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedFix == null)
                    return false;
                return true;
            },
            (p) =>
            {
                SelectedFix.IsDeleted = true;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xoá yêu cầu sửa chữa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Fixes = new ObservableCollection<Fix>(DataProvider.Ins.DB.Fixes.Where(f => f.IsDeleted == false).ToList());
            });
        }
    }
}
