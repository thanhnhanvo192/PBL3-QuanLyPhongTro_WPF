using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum InvoiceStatus
    {
        UnPaid,
        Paid,
        Overdue
    }
    public class InvoiceStatusDisplay
    {
        public InvoiceStatus Value { get; set; }
        public string DisplayName { get; set; }
    }
    public static class InvoiceStatusOptions
    {
        public static ObservableCollection<InvoiceStatusDisplay> GetInvoiceStatuses()
        {
            return new ObservableCollection<InvoiceStatusDisplay>
            {
                new InvoiceStatusDisplay { Value = InvoiceStatus.UnPaid, DisplayName = "Chưa thanh toán" },
                new InvoiceStatusDisplay { Value = InvoiceStatus.Paid, DisplayName = "Đã thanh toán" },
                new InvoiceStatusDisplay { Value = InvoiceStatus.Overdue, DisplayName = "Quá hạn" }
            };
        }
    }
}
