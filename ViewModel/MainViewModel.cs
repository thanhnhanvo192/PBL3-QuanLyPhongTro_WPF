﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Model;
using System.Windows;
using QuanLyPhongTro.Enum;

namespace QuanLyPhongTro.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private int _totalEmptyRoom;
        private Decimal _lastRevenue;
        private int _totalOutDateContract;
        private ObservableCollection<EmptyRoomDisplay> _ListEmptyRoom;
        private ObservableCollection<Contract> _ListContract;
        public ObservableCollection<EmptyRoomDisplay> ListEmptyRoom
        {
            get => _ListEmptyRoom;
            set
            {
                _ListEmptyRoom = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Contract> ListContract
        {
            get => _ListContract;
            set
            {
                _ListContract = value;
                OnPropertyChanged();
            }
        }
        public int TotalemtpyRoom
        {
            get => _totalEmptyRoom;
            set
            {
                _totalEmptyRoom = value;
                OnPropertyChanged();
            }
        }
        public Decimal LastRevenue
        {
            get => _lastRevenue;
            set
            {
                _lastRevenue = value;
                OnPropertyChanged();
            }
        }
        public int TotalOutDateContract
        {
            get => _totalOutDateContract;
            set
            {
                _totalOutDateContract = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            EmptyListRoom();
            CountEmptyRoom();
            CalculateLastMonthRevenue();
            ListOutDateContract();
            CountTotalOutDateContract();
        }
        public void EmptyListRoom()
        {
            ListEmptyRoom = new ObservableCollection<EmptyRoomDisplay>();
            var list = DataProvider.Ins.DB.Rooms.Where(r => r.IsDeleted == false).ToList();
            int stt = 1;
            foreach (var item in list)
            {
                if (item.Status == Enum.RoomFilterStatus.Vacant)
                {
                    EmptyRoomDisplay emptyRoomDisplay = new EmptyRoomDisplay();
                    emptyRoomDisplay.STT = stt;
                    emptyRoomDisplay.RoomNumber = item.RoomNumber;
                    stt++;
                    ListEmptyRoom.Add(emptyRoomDisplay);
                }
            }
        }
        public void CountEmptyRoom()
        {
            try
            {
                int count = DataProvider.Ins.DB.Rooms.Where(r => r.IsDeleted == false).Count(room => room.Status == Enum.RoomFilterStatus.Vacant);
                TotalemtpyRoom = count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đếm số phòng trống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalemtpyRoom = 0;
            }
        }
        public void CalculateLastMonthRevenue()
        {
            try
            {
                int lastMonth = DateTime.Now.Month - 1;
                int Year = DateTime.Now.Year;
                if (lastMonth == 0)
                {
                    lastMonth = 12;
                    Year--;
                }
                LastRevenue = DataProvider.Ins.DB.Invoices.Where(invoice => invoice.IsDeleted == false && invoice.CreateDate.Year == Year && invoice.CreateDate.Month == lastMonth && invoice.Status == InvoiceStatus.Paid).Sum(invoice => invoice.AmountPaid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tính doanh thu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                LastRevenue = 0;
            }
        }
        public void ListOutDateContract()
        {
            try
            {
                var year = DateTime.Now.Year;
                var nextMonth = DateTime.Now.Month + 1;
                if (nextMonth == 13)
                {
                    nextMonth = 1;
                    year++;
                }
                ListContract = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.Where(contract => contract.IsDeleted == false && contract.EndDate.Year == year && contract.EndDate.Month == nextMonth).ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lấy danh sách hợp đồng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                ListContract = new ObservableCollection<Contract>();
            }
        }
        public void CountTotalOutDateContract()
        {
            try
            {
                var year = DateTime.Now.Year;
                var nextMonth = DateTime.Now.Month + 1;
                if (nextMonth == 13)
                {
                    nextMonth = 1;
                    year++;
                }
                TotalOutDateContract = DataProvider.Ins.DB.Contracts.Count(contract => contract.IsDeleted == false && contract.EndDate.Year == year && contract.EndDate.Month == nextMonth);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể đếm số hợp đồng hết hạn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalOutDateContract = 0;
            }
        }
    }
}
