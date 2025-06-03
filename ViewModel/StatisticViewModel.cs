using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class StatisticViewModel : BaseViewModel
    {
        private ObservableCollection<DateTime> _MonthYearList;
        private DateTime? _SelectedMonthYear;
        private ObservableCollection<DateTime> _YearList;
        private DateTime? _SelectedYear;
        private DateTime? _DateFrom;
        private DateTime? _DateTo;
        private ObservableCollection<Invoice> _InvoicesList;
        private bool _isMonthFilter;
        private bool _isYearFilter;
        private bool _isPeriodFilter;
        private ObservableCollection<RevenueStatistic> _revenueStatistics;
        private Decimal _totalRevenue;

        public Decimal TotalRevenue
        {
            get { return _totalRevenue; }
            set
            {
                _totalRevenue = value;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }
        public ObservableCollection<RevenueStatistic> RevenueStatistics
        {
            get { return _revenueStatistics; }
            set
            {
                _revenueStatistics = value;
                OnPropertyChanged(nameof(RevenueStatistics));
            }
        }
        public bool IsMonthFilter
        {
            get => _isMonthFilter;
            set
            {
                if (_isMonthFilter != value)
                {
                    _isMonthFilter = value;
                    OnPropertyChanged();

                    if (!value)
                        SelectedMonthYear = null;
                }
            }
        }
        public bool IsYearFilter
        {
            get => _isYearFilter;
            set
            {
                if (_isYearFilter != value)
                {
                    _isYearFilter = value;
                    OnPropertyChanged();
                    if (!value)
                        SelectedYear = null;
                }
            }
        }
        public bool IsPeriodFilter
        {
            get => _isPeriodFilter;
            set
            {
                if (_isPeriodFilter != value)
                {
                    _isPeriodFilter = value;
                    OnPropertyChanged();
                    if (!value)
                    {
                        DateFrom = DateTime.Now.AddMonths(-1);
                        DateTo = DateTime.Now;
                    }
                }
            }
        }
        public ObservableCollection<DateTime> MonthYearList
        {
            get { return _MonthYearList; }
            set { _MonthYearList = value; }
        }
        public DateTime? SelectedMonthYear
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
        public DateTime? SelectedYear
        {
            get { return _SelectedYear; }
            set
            {
                _SelectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }
        public DateTime? DateFrom
        {
            get { return _DateFrom; }
            set
            {
                _DateFrom = value;
                OnPropertyChanged(nameof(DateFrom));
            }
        }
        public DateTime? DateTo
        {
            get { return _DateTo; }
            set
            {
                _DateTo = value;
                OnPropertyChanged(nameof(DateTo));
            }
        }
        public ObservableCollection<Invoice> InvoicesList
        {
            get { return _InvoicesList; }
            set
            {
                _InvoicesList = value;
                OnPropertyChanged(nameof(InvoicesList));
            }
        }

        public ICommand SearchRevenueComamnd { get; set; }
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

            SearchRevenueComamnd = new RelayCommand<object>((p) => true, (p) =>
            {
                DateTime startDate, endDate;

                if (IsMonthFilter && SelectedMonthYear.HasValue)
                {
                    var month = SelectedMonthYear.Value;
                    startDate = new DateTime(month.Year, month.Month, 1);
                    endDate = startDate.AddMonths(1).AddDays(-1);
                }
                else if (IsYearFilter && SelectedYear.HasValue)
                {
                    var year = SelectedYear.Value.Year;
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 12, 31);
                }
                else if (IsPeriodFilter && DateFrom.HasValue && DateTo.HasValue)
                {
                    startDate = DateFrom.Value;
                    endDate = DateTo.Value;
                }
                else
                {
                    // Default: không có filter → lấy toàn bộ
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                }

                var revenueByRoom = DataProvider.Ins.DB.Invoices
                .Where(i => i.CreateDate >= startDate && i.CreateDate <= endDate)
                .GroupBy(i => i.Contract.Room.RoomNumber)
                .Select(g => new RevenueStatistic
                {
                    RoomNumber = g.Key,
                    TotalRevenue = g.Sum(i => i.TotalAmount),
                    InvoiceCount = g.Count()
                })
                .OrderBy(r => r.RoomNumber)
                .ToList();

                RevenueStatistics = new ObservableCollection<RevenueStatistic>(
                    revenueByRoom.Select(r => new RevenueStatistic
                    {
                        RoomNumber = r.RoomNumber,
                        TotalRevenue = r.TotalRevenue,
                        InvoiceCount = r.InvoiceCount
                    })
                );
                TotalRevenue = 0;
                foreach (var item in RevenueStatistics)
                {
                    TotalRevenue += item.TotalRevenue;
                }
            });
        }
    }
}
