using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class AddContractViewModel : BaseViewModel
    {
        private ObservableCollection<Contract> _ContractList;
        private ObservableCollection<Tenant> _TenantList;
        private ObservableCollection<Room> _VacantRoomList;
        public int Id { get; set; }
        private Room _SelectedRoom;
        private Tenant _SelectedTenant;
        public string _ContractCode;
        public DateTime _StartDate;
        public DateTime _EndDate;
        public decimal? _Deposit;
        public ContractStatusDisplay _Status;
        public string _Notes;

        public ObservableCollection<Contract> ContractList
        {
            get { return _ContractList; }
            set
            {
                _ContractList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Tenant> TenantList
        {
            get { return _TenantList; }
            set
            {
                _TenantList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Room> VacantRoomList
        {
            get { return _VacantRoomList; }
            set
            {
                _VacantRoomList = value;
                OnPropertyChanged();
            }
        }
        public Room SelectedRoom
        {
            get { return _SelectedRoom; }
            set
            {
                _SelectedRoom = value;
                OnPropertyChanged();
            }
        }
        public Tenant SelectedTenant
        {
            get { return _SelectedTenant; }
            set
            {
                _SelectedTenant = value;
                OnPropertyChanged();
            }
        }
        public string ContractCode
        {
            get { return _ContractCode; }
            set
            {
                _ContractCode = value;
                OnPropertyChanged();
            }
        }
        public DateTime StartDate
        {
            get { return _StartDate; }
            set
            {
                _StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                OnPropertyChanged();
            }
        }
        public decimal? Deposit
        {
            get { return _Deposit; }
            set
            {
                _Deposit = value;
                OnPropertyChanged();
            }
        }
        public ContractStatusDisplay Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }
        public string Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddContractCommand { get; set; }
        public AddContractViewModel()
        {
            TenantList = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.ToList());
            VacantRoomList = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.Where(r => r.Status == RoomFilterStatus.Vacant));
            ContractList = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.ToList());
            Status = ContractStatusOptions.GetContractStatuses().First();
            Deposit = 0;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddMonths(1);
            AddContractCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               if (SelectedRoom == null)
               {
                   MessageBox.Show("Vui lòng chọn phòng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                   return;
               }
               if (string.IsNullOrWhiteSpace(ContractCode))
                {
                     MessageBox.Show("Mã hợp đồng không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                     return;
               }
               var contractCodeExists = ContractList.Any(c => string.Equals(c.ContractCode, ContractCode, StringComparison.OrdinalIgnoreCase));
               if (contractCodeExists)
               {
                   MessageBox.Show("Mã hợp đồng đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               if (StartDate == null || EndDate == null)
               {
                     MessageBox.Show("Ngày bắt đầu và ngày kết thúc không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               if (StartDate > EndDate)
               {
                   MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               if (EndDate < DateTime.Now)
               {
                     MessageBox.Show("Ngày kết thúc không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                     return;
               }
               if (SelectedTenant == null)
               {
                   MessageBox.Show("Vui lòng chọn khách thuê!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               if (Deposit < 0)
               {
                   MessageBox.Show("Tiền cọc không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               Contract newContract = new Contract()
               {
                   ContractCode = ContractCode,
                   StartDate = StartDate,
                   EndDate = EndDate,
                   Deposit = Deposit.Value,
                   RoomId = SelectedRoom.Id,
                   TenantId = SelectedTenant.Id,
                   Status = ContractStatus.Active,
                   Notes = Notes
               };
               try
               {
                   var roomInDb = DataProvider.Ins.DB.Rooms.FirstOrDefault(r => r.Id == SelectedRoom.Id);
                   roomInDb.Status = RoomFilterStatus.Occupied;
                   DataProvider.Ins.DB.Contracts.Add(newContract);
                   DataProvider.Ins.DB.SaveChanges();
                   this.Id = newContract.Id;
                   MessageBox.Show("Thêm hợp đồng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
               }
               catch (Exception ex)
               {
                   MessageBox.Show("Lỗi khi lưu hợp đồng: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
               }
               var dep = p as DependencyObject;
               if (dep != null)
               {
                   var window = Window.GetWindow(dep);
                   if (window != null)
                       window.Close();
               }
           });
        }
    }
}
