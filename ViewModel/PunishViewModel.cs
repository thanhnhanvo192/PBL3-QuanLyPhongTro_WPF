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
    public class PunishViewModel : BaseViewModel
    {
        private ObservableCollection<Punish> _PunishList;
        private ObservableCollection<Punish> _PunishListDisplay;
        private ObservableCollection<Room> _RoomOccupideList;
        private Room _SelectedOccupiedRoom;
        private ObservableCollection<Contract> _ContractList;
        private Contract _SelectedContract;
        private Punish _SelectedItem;
        private DateTime? _SearchDateFrom;
        private DateTime? _SearchDateTo;
        private DateTime _PunishDate;
        private string _Reason;
        private decimal _Amount;
        private string _RoomNumber;
        public ObservableCollection<Punish> PunishList
        {
            get => _PunishList;
            set
            {
                _PunishList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Punish> PunishListDisplay
        {
            get => _PunishListDisplay;
            set
            {
                _PunishListDisplay = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Room> RoomOccupideList
        {
            get => _RoomOccupideList;
            set
            {
                _RoomOccupideList = value;
                OnPropertyChanged();
            }
        }
        public Room SelectedOccupiedRoom
        {
            get => _SelectedOccupiedRoom;
            set
            {
                _SelectedOccupiedRoom = value;
                OnPropertyChanged();
                if (SelectedOccupiedRoom != null)
                {
                    var contract = DataProvider.Ins.DB.Contracts.FirstOrDefault(c => c.RoomId == SelectedOccupiedRoom.Id && c.Status == ContractStatus.Active);
                    if (contract != null)
                    {
                        SelectedContract = contract;
                    }
                }
            }
        }
        public ObservableCollection<Contract> ContractList
        {
            get => _ContractList;
            set
            {
                _ContractList = value;
                OnPropertyChanged();
            }
        }
        public Contract SelectedContract
        {
            get => _SelectedContract;
            set
            {
                _SelectedContract = value;
                OnPropertyChanged();
            }
        }
        public Punish SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    RoomNumber = SelectedItem.Contract.Room.RoomNumber;
                    PunishDate = SelectedItem.PunishDate;
                    Reason = SelectedItem.Reason;
                    Amount = SelectedItem.Amount;
                }
            }
        }
        public DateTime? SearchDateFrom
        {
            get => _SearchDateFrom;
            set
            {
                _SearchDateFrom = value;
                OnPropertyChanged();
            }
        }
        public DateTime? SearchDateTo
        {
            get => _SearchDateTo;
            set
            {
                _SearchDateTo = value;
                OnPropertyChanged();
            }
        }
        public DateTime PunishDate
        {
            get => _PunishDate;
            set
            {
                _PunishDate = value;
                OnPropertyChanged();
            }
        }
        public string Reason
        {
            get => _Reason;
            set
            {
                _Reason = value;
                OnPropertyChanged();
            }
        }
        public decimal Amount
        {
            get => _Amount;
            set
            {
                _Amount = value;
                OnPropertyChanged();
            }
        }
        public string RoomNumber
        {
            get => _RoomNumber;
            set
            {
                _RoomNumber = value;
                OnPropertyChanged();
            }
        }
        public ICommand SearchPunishCommand { get; set; }
        public ICommand AddPunishCommand { get; set; }
        public ICommand AddNewPunishCommand { get; set; }
        public ICommand UpdatePunishCommand { get; set; }
        public PunishViewModel()
        {
            PunishList = new ObservableCollection<Punish>(DataProvider.Ins.DB.Punishes.Include(c => c.Contract).ToList());
            PunishListDisplay = new ObservableCollection<Punish>(PunishList);
            RoomOccupideList = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.Where(r => r.Status == RoomFilterStatus.Occupied).ToList());
            ContractList = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.Include(c => c.Invoices).ToList());
            SearchPunishCommand = new RelayCommand<object> ((p) => true, (p) =>
            {
                if (SearchDateFrom != null)
                {
                    PunishListDisplay = new ObservableCollection<Punish>(PunishList.Where(x => x.PunishDate >= SearchDateFrom));
                }
                if (SearchDateTo != null)
                {
                    PunishListDisplay = new ObservableCollection<Punish>(PunishListDisplay.Where(x => x.PunishDate <= SearchDateTo));
                }
            });
            AddPunishCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PunishDate = DateTime.Now;
                var addPunishWindow = new AddPunishWindow();
                addPunishWindow.ShowDialog();
            });
            AddNewPunishCommand = new RelayCommand<object> ((p) => true, (p) =>
            {
                if (SelectedOccupiedRoom == null)
                {
                    MessageBox.Show("Vui lòng chọn phòng đã thuê để thêm phạt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(Reason))
                {
                    MessageBox.Show("Vui lòng nhập lý do phạt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                Invoice_detail invoice_detail = new Invoice_detail()
                {
                    InvoiceId = SelectedContract.Invoices.FirstOrDefault().Id,
                    Quantity = 1,
                    UnitPrice = Amount,
                    Notes = "Hoá đơn xử phạt:" + " " + Reason + ", " + "Ngày phạt:" + " " + PunishDate.ToString("dd/MM/yyyy"),
                };
                Punish newPunish = new Punish()
                {
                    ContractID = SelectedContract.Id,
                    PunishDate = PunishDate,
                    Reason = Reason,
                    Amount = Amount,
                    InvoiceId = null
                };
                DataProvider.Ins.DB.Invoice_details.Add(invoice_detail);
                DataProvider.Ins.DB.Punishes.Add(newPunish);
                DataProvider.Ins.DB.SaveChanges();
                PunishList.Add(newPunish);
                PunishListDisplay.Add(newPunish);
                var dep = p as DependencyObject;
                if (dep != null)
                {
                    var window = Window.GetWindow(dep);
                    if (window != null)
                        window.Close();
                }
                MessageBox.Show("Thêm bản phạt thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            UpdatePunishCommand = new RelayCommand<object>(
            (p) => SelectedItem != null,
            (p) =>
            {
                if (Amount < 0)
                {
                    MessageBox.Show("Số tiền phạt không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Reason))
                {
                    MessageBox.Show("Vui lòng nhập lý do phạt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var punish = DataProvider.Ins.DB.Punishes.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (punish != null)
                {
                    punish.PunishDate = PunishDate;
                    punish.Reason = Reason;
                    punish.Amount = Amount;

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }
    }
}
