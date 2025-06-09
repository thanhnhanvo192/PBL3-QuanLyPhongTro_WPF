using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum ContractStatus
    {
        CancelledOrExpired,
        Active,
    }
    public class ContractStatusDisplay
    {
        public ContractStatus Value { get; set; }
        public string DisplayName { get; set; }
    }
    public static class ContractStatusOptions
    {
        public static ObservableCollection<ContractStatusDisplay> GetContractStatuses()
        {
            return new ObservableCollection<ContractStatusDisplay>
            {
                new ContractStatusDisplay { Value = ContractStatus.CancelledOrExpired, DisplayName = "Hết hạn/Huỷ" },
                new ContractStatusDisplay { Value = ContractStatus.Active, DisplayName = "Đang hiệu lực" }
            };
        }
    }
}