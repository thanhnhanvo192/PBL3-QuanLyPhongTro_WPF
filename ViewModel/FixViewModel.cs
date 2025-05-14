using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class FixViewModel : BaseViewModel
    {
        public ObservableCollection<Fix> Fixes { get; set; }
        public ObservableCollection<Tenant> Tenants { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public ObservableCollection<Invoice> Invoices { get; set; }
        public ObservableCollection<FaultTypeDisplay> FaultTypes { get; set; }

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
        public ICommand UpdateFixCommand { get; set; }

        public FixViewModel()
        {
            Fixes = new ObservableCollection<Fix>(DataProvider.Ins.DB.Fixes.ToList());
            Rooms = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.ToList());
            Tenants = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.ToList());
            Invoices = new ObservableCollection<Invoice>(DataProvider.Ins.DB.Invoices.ToList());
            FaultTypes = FaultOptions.GetFaultTypes();


            AddFixCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var viewModel = new AddFixViewModel();
               var addFixWindow = new AddFixWindow
               {
                   DataContext = viewModel
               };

               addFixWindow.ShowDialog();
               var fix = DataProvider.Ins.DB.Fixes.FirstOrDefault(x => x.Id == viewModel.Id);
               if (fix != null && !Fixes.Contains(fix))
               {
                   Fixes.Add(fix);
               }
           });

            UpdateFixCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedFix == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var fix = DataProvider.Ins.DB.Fixes.Where(x => x.Id == SelectedFix.Id).SingleOrDefault();
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
                }
            });
        }
    }
}
