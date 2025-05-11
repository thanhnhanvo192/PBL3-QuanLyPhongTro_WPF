using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _Units;
        public ObservableCollection<Unit> Units
        {
            get { return _Units; }
            set
            {
                _Units = value;
                OnPropertyChanged();
            }
        }
        private Unit _SelectedUnit;
        public Unit SelectedUnit
        {
            get { return _SelectedUnit; }
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
                if (SelectedUnit != null)
                {
                    DisplayName = SelectedUnit.DisplayName;
                }
            }
        }
        private string _DisplayName;
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                _DisplayName = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddUnitCommand { get; set; }
        public ICommand UpdateUnitCommand { get; set; }
        public UnitViewModel()
        {
            Units = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
            AddUnitCommand = new RelayCommand<object>((p) =>
            { 
                if (string.IsNullOrEmpty(DisplayName))
                    return false;
                var unitList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                if (unitList == null || unitList.Count() != 0)
                    return false;
                return true;
            },
            (p) =>
            {
                var unit = new Unit() { DisplayName = DisplayName };
                DataProvider.Ins.DB.Units.Add(unit);
                DataProvider.Ins.DB.SaveChanges();
                Units.Add(unit);
            });
            UpdateUnitCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || SelectedUnit == null)
                    return false;
                var unitList = DataProvider.Ins.DB.Units.Where(x => x.DisplayName == DisplayName);
                if (unitList == null || unitList.Count() != 0)
                    return false;
                return true;
            },
            (p) =>
            {
                var unit = DataProvider.Ins.DB.Units.Where(x => x.UnitId == SelectedUnit.UnitId).SingleOrDefault();
                unit.DisplayName = DisplayName;
                DataProvider.Ins.DB.SaveChanges();
                SelectedUnit.DisplayName = DisplayName;
                OnPropertyChanged(SelectedUnit.DisplayName);
            });
        }
    }
}
