using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;
using System.Windows.Input;

namespace QuanLyPhongTro.ViewModel
{
    public class ServiceViewModel : BaseViewModel
    {
        private ObservableCollection<Service> _Services;
        private ObservableCollection<Unit> _Units;
        public ObservableCollection<Service> Services
        {
            get { return _Services; }
            set
            {
                _Services = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Unit> Units
        {
            get { return _Units; }
            set
            {
                _Units = value;
                OnPropertyChanged();
            }
        }
        private Service _SelectedService;
        public Service SelectedService
        {
            get { return _SelectedService; }
            set
            {
                _SelectedService = value;
                OnPropertyChanged();
                if (SelectedService != null)
                {
                    DisplayName = SelectedService.DisplayName;
                    Price = SelectedService.Price.ToString();
                    SelectedUnit = SelectedService.Unit;
                }
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
        private string _Price;
        public string Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddServiceCommand { get; set; }
        public ICommand UpdateServiceCommand { get; set; }
        public ServiceViewModel()
        {
            Services = new ObservableCollection<Service>(DataProvider.Ins.DB.Services.ToList());
            Units = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units.ToList());
            AddServiceCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrWhiteSpace(DisplayName) || string.IsNullOrWhiteSpace(Price))
                    return false;
                if (!decimal.TryParse(Price, out decimal price))
                    return false;
                if (DataProvider.Ins.DB.Services.Any(x => x.DisplayName == DisplayName))
                    return false;
                if (SelectedUnit == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var service = new Service() 
                { 
                    DisplayName = DisplayName,
                    Price = decimal.Parse(Price),
                    UnitId = SelectedUnit.UnitId
                };
                DataProvider.Ins.DB.Services.Add(service);
                DataProvider.Ins.DB.SaveChanges();
                Services.Add(service);
            });

            UpdateServiceCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedService == null)
                    return false;
                if (!decimal.TryParse(Price, out decimal price))
                    return false;
                return true;
            },
            (p) =>
            {
                var service = DataProvider.Ins.DB.Services.Where(x => x.Id == SelectedService.Id).SingleOrDefault();
                service.DisplayName = DisplayName;
                service.Price = decimal.Parse(Price);
                service.UnitId = SelectedUnit.UnitId;
                DataProvider.Ins.DB.SaveChanges();
                SelectedService.DisplayName = DisplayName;
                SelectedService.Price = decimal.Parse(Price);
                SelectedService.Unit = SelectedUnit;
            });
        }
    }
}
