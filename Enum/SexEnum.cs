using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyPhongTro.Enum
{
    public enum SexEnum
    {
        Male,
        Female,
        Other
    }
    public class SexOptionDisplay
    {
        public SexEnum Value;
        public string DisplayName;
    }
    public static class SexOptions
    {
        public static ObservableCollection<SexOptionDisplay> GetSexEnums()
        {
            return new ObservableCollection<SexOptionDisplay>
            {
                new SexOptionDisplay { Value = SexEnum.Male, DisplayName = "Nam" },
                new SexOptionDisplay { Value = SexEnum.Female, DisplayName = "Nữ" },
                new SexOptionDisplay { Value = SexEnum.Other, DisplayName = "Khác" }
            };
        }
    }
}
