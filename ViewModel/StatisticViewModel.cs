using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class StatisticViewModel : BaseViewModel
    {
        private ObservableCollection<DateTime> _MonthYearList;
        private DateTime _SelectedMonthYear;
        private ObservableCollection<DateTime> _YearList;
        private DateTime _SelectedYear;
        private DateTime _DateFrom;
        private DateTime _DateTo;
        public ObservableCollection<DateTime> MonthYearList
        {
            get { return _MonthYearList; }
            set { _MonthYearList = value; }
        }
        public DateTime SelectedMonthYear
        {
            get { return _SelectedMonthYear; }
            set
            {
                _SelectedMonthYear = value;
                OnPropertyChanged(nameof(SelectedMonthYear));
            }
        }
        public ObservableCollection<DateTime> YearList
        {
            get { return _YearList; }
            set { _YearList = value; }
        }
        public DateTime SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                _SelectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }
        public DateTime DateFrom
        {
            get { return _DateFrom; }
            set
            {
                _DateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }
        public DateTime DateTo
        {
            get { return _DateTo; }
            set
            {
                _DateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }
        public StatisticViewModel()
        {
            var rawDates = DataProvider.Ins.DB.Invoices
             .Select(i => i.CreateDate)
             .ToList();

            MonthYearList = new ObservableCollection<DateTime>(
                rawDates
                    .Select(d => new DateTime(d.Year, d.Month, 1))
                    .Distinct()
                    .OrderByDescending(d => d)
            );
            YearList = new ObservableCollection<DateTime>(
                rawDates
                    .Select(d => new DateTime(d.Year, 1, 1))
                    .Distinct()
                    .OrderByDescending(d => d)
            );
            DateFrom = DateTime.Now.AddMonths(-1);
            DateTo = DateTime.Now;
        }
    }
}
