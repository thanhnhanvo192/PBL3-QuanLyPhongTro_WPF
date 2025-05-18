using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class InvoiceViewModel : BaseViewModel
    {
        private ObservableCollection<Invoice> _InvoiceList;
        private ObservableCollection<Contract> _ContractActiveList;
        private ObservableCollection<InvoiceStatusDisplay> _InvoiceStatusList;
        private ObservableCollection<MeterReading> _MeterReadingLastMonthList;
        private ObservableCollection<MeterReading> _MeterReadingCurrentMonthList;
        private ObservableCollection<Invoice> _AllInvoice;
        private Invoice _SelectedItem;
        private Contract _SelectedContract;
        private string _InvoiceCode;
        private DateTime _CreateDate;
        private DateTime? _DueDate;
        private decimal _TotalAmount;
        private decimal _AmountPaid;
        private InvoiceStatusDisplay _Status; // 0: Chưa TT, 1: Đã TT, 2: Quá hạn
        private int _ContractId;
        private string _Notes;
        private DateTime? _StartDateSearch;
        private DateTime? _EndDateSearch;
        public string InvoiceCode
        {
            get { return _InvoiceCode; }
            set
            {
                _InvoiceCode = value;
                OnPropertyChanged(nameof(InvoiceCode));
            }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                _CreateDate = value;
                OnPropertyChanged(nameof(CreateDate));
            }
        }
        public DateTime? DueDate
        {
            get { return _DueDate; }
            set
            {
                _DueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }
        public decimal TotalAmount
        {
            get { return _TotalAmount; }
            set
            {
                _TotalAmount = value;
                OnPropertyChanged(nameof(TotalAmount));
            }
        }
        public decimal AmountPaid
        {
            get { return _AmountPaid; }
            set
            {
                _AmountPaid = value;
                OnPropertyChanged(nameof(AmountPaid));
            }
        }
        public InvoiceStatusDisplay Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public int ContractId
        {
            get { return _ContractId; }
            set
            {
                _ContractId = value;
                OnPropertyChanged(nameof(ContractId));
            }
        }
        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
        public DateTime? StartDateSearch
        {
            get { return _StartDateSearch; }
            set
            {
                _StartDateSearch = value;
                OnPropertyChanged();
            }
        }
        public DateTime? EndDateSearch
        {
            get { return _EndDateSearch; }
            set
            {
                _EndDateSearch = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Invoice> InvoiceList
        {
            get { return _InvoiceList; }
            set
            {
                _InvoiceList = value;
                OnPropertyChanged(nameof(InvoiceList));
            }
        }
        public ObservableCollection<Contract> ContractActiveList
        {
            get { return _ContractActiveList; }
            set
            {
                _ContractActiveList = value;
                OnPropertyChanged(nameof(ContractActiveList));
            }
        }
        public ObservableCollection<InvoiceStatusDisplay> InvoiceStatusList
        {
            get { return _InvoiceStatusList; }
            set
            {
                _InvoiceStatusList = value;
                OnPropertyChanged(nameof(InvoiceStatusList));
            }
        }
        public ObservableCollection<MeterReading> MeterReadingLastMonthList
        {
            get { return _MeterReadingLastMonthList; }
            set
            {
                _MeterReadingLastMonthList = value;
                OnPropertyChanged(nameof(MeterReadingLastMonthList));
            }
        }
        public ObservableCollection<MeterReading> MeterReadingCurrentMonthList
        {
            get { return _MeterReadingCurrentMonthList; }
            set
            {
                _MeterReadingCurrentMonthList = value;
                OnPropertyChanged(nameof(MeterReadingCurrentMonthList));
            }
        }
        public ObservableCollection<Invoice> AllInvoice
        {
            get { return _AllInvoice; }
            set
            {
                _AllInvoice = value;
                OnPropertyChanged(nameof(AllInvoice));
            }
        }
        public Invoice SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
                if (SelectedItem != null)
                {     
                    InvoiceCode = SelectedItem.InvoiceCode;
                    CreateDate = SelectedItem.CreateDate;
                    DueDate = SelectedItem.DueDate;
                    TotalAmount = SelectedItem.TotalAmount;
                    AmountPaid = SelectedItem.AmountPaid;
                    Status = InvoiceStatusList.FirstOrDefault(s => s.Value == SelectedItem.Status);
                    ContractId = SelectedItem.ContractId;
                    Notes = SelectedItem.Notes;
                }
            }
        }
        public Contract SelectedContract
        {
            get { return _SelectedContract; }
            set
            {
                _SelectedContract = value;
                OnPropertyChanged(nameof(SelectedContract));
            }
        }
        #region
        public ICommand MakingInvoiceCommand { get; set; }
        public ICommand SearchInvoiceCommand { get; set; }
        public ICommand AddInvoiceCommand { get; set; }
        public ICommand AddNewInvoiceCommand { get; set; }
        public ICommand UpdateInvoiceCommand { get; set; }
        #endregion
        public InvoiceViewModel()
        {
            InvoiceList = new ObservableCollection<Invoice>(DataProvider.Ins.DB.Invoices.ToList());
            ContractActiveList = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.Where(c => c.Status == Enum.ContractStatus.Active).ToList());
            InvoiceStatusList = InvoiceStatusOptions.GetInvoiceStatuses();
            AllInvoice = new ObservableCollection<Invoice>(DataProvider.Ins.DB.Invoices.ToList());
            var now = DateTime.Now;
            CreateDate = now;
            DueDate = now.AddDays(30);
            MeterReadingLastMonthList = new ObservableCollection<MeterReading>(
                [.. DataProvider.Ins.DB.MeterReadings
                    .Include(m => m.Service)
                    .Where(m => m.ReadingDate < new DateTime(now.Year, now.Month, 1)) // tất cả trước tháng này
                    .GroupBy(m => new { m.RoomId, m.ServiceId }) // nhóm theo phòng và dịch vụ
                    .Select(g => g.OrderByDescending(m => m.ReadingDate).FirstOrDefault())]);
            MeterReadingCurrentMonthList = new ObservableCollection<MeterReading>(DataProvider.Ins.DB.MeterReadings.Include(m => m.Service).Where(m => m.ReadingDate.Month == now.Month && m.ReadingDate.Year == now.Year).ToList());

            MakingInvoiceCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                foreach (var item in ContractActiveList)
                {
                    bool invoiceExists = DataProvider.Ins.DB.Invoices.Any(i => i.ContractId == item.Id && i.CreateDate.Month == DateTime.Now.Month && i.CreateDate.Year == DateTime.Now.Year);
                    if (invoiceExists)
                        continue;

                    var invoice = new Invoice()
                    {
                        InvoiceCode = $"HD{DateTime.Now:yyyyMMddHHmmss}_{item.Id}",
                        CreateDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(30),
                        TotalAmount = item.Room.Price - item.Deposit,
                        AmountPaid = 0,
                        Status = InvoiceStatus.UnPaid,
                        ContractId = item.Id,
                        Notes = "Hóa đơn tự động tạo",
                    };

                    DataProvider.Ins.DB.Invoices.Add(invoice);
                    DataProvider.Ins.DB.SaveChanges();

                    foreach (var meterReading in MeterReadingCurrentMonthList.Where(m => m.RoomId == item.RoomId))
                    {
                        var lastReadingValue = MeterReadingLastMonthList
                            .FirstOrDefault(m => m.RoomId == item.RoomId && m.ServiceId == meterReading.ServiceId)?.ReadingValue ?? 0;

                        var invoice_detail = new Invoice_detail()
                        {
                            InvoiceId = invoice.Id,
                            ServiceId = meterReading.ServiceId,
                            Quantity = meterReading.ReadingValue - lastReadingValue,
                            UnitPrice = meterReading.Service.Price,
                            Notes = "Hóa đơn tự động tạo"
                        };

                        invoice.TotalAmount += invoice_detail.Amount;

                        DataProvider.Ins.DB.Invoice_details.Add(invoice_detail);
                    }

                    DataProvider.Ins.DB.SaveChanges();
                }
                MessageBox.Show("Tạo hóa đơn thành công cho tất cả phòng đang được thuê.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            SearchInvoiceCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var filteredInvoices = AllInvoice.ToList();
                if (Status != null)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.Status == Status.Value).ToList();
                }
                if (StartDateSearch != null)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDate >= StartDateSearch).ToList();
                }
                if (EndDateSearch != null)
                {
                    filteredInvoices = filteredInvoices.Where(i => i.CreateDate <= EndDateSearch).ToList();
                }
                InvoiceList = new ObservableCollection<Invoice>(filteredInvoices);
            });
            AddInvoiceCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    var addInvoiceWindow = new AddInvoiceWindow{};
                    addInvoiceWindow.ShowDialog();
                });
            AddNewInvoiceCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    if (InvoiceCode == null || CreateDate == null || DueDate == null || TotalAmount <= 0 || AmountPaid < 0 || Status == null || SelectedContract.Id <= 0)
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ thông tin hóa đơn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (InvoiceList.Any(i => i.InvoiceCode == InvoiceCode))
                    {
                        MessageBox.Show("Mã hóa đơn đã tồn tại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var invoice = new Invoice()
                    {
                        InvoiceCode = InvoiceCode,
                        CreateDate = CreateDate,
                        DueDate = DueDate,
                        TotalAmount = TotalAmount,
                        AmountPaid = AmountPaid,
                        Status = Status.Value,
                        ContractId = SelectedContract.Id,
                        Notes = Notes,
                    };
                    DataProvider.Ins.DB.Invoices.Add(invoice);
                    DataProvider.Ins.DB.SaveChanges();
                    if (invoice != null && !InvoiceList.Contains(invoice))
                    {
                        InvoiceList.Add(invoice);
                        OnPropertyChanged();
                    }
                    MessageBox.Show("Thêm hóa đơn thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    var dep = p as DependencyObject;
                    if (dep != null)
                    {
                        var window = Window.GetWindow(dep);
                        if (window != null)
                            window.Close();
                    }
                });
            UpdateInvoiceCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    if (SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn hóa đơn để cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var invoiceToUpdate = DataProvider.Ins.DB.Invoices.FirstOrDefault(i => i.Id == SelectedItem.Id);
                    if (invoiceToUpdate != null)
                    {
                        invoiceToUpdate.InvoiceCode = InvoiceCode;
                        invoiceToUpdate.CreateDate = CreateDate;
                        invoiceToUpdate.DueDate = DueDate;
                        invoiceToUpdate.TotalAmount = TotalAmount;
                        invoiceToUpdate.AmountPaid = AmountPaid;
                        invoiceToUpdate.Status = Status.Value;
                        invoiceToUpdate.ContractId = SelectedItem.ContractId;
                        invoiceToUpdate.Notes = Notes;
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Cập nhật hóa đơn thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                });
        }
    }
}
