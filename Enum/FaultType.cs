using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum FaultType
    {
        TenantFault,
        OwnerFault
    }
    public class FaultTypeDisplay
    {
        public FaultType Value { get; set; }
        public string DisplayName { get; set; }
    }
    public static class FaultOptions
    {
        public static ObservableCollection<FaultTypeDisplay> GetFaultTypes()
        {
            return new ObservableCollection<FaultTypeDisplay>
            {
                new FaultTypeDisplay { Value = FaultType.TenantFault, DisplayName = "Lỗi của khách thuê" },
                new FaultTypeDisplay { Value = FaultType.OwnerFault, DisplayName = "Lỗi của chủ trọ" }
            };
        }
    }
}
