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
using Microsoft.Extensions.Logging.Abstractions;
using QuanLyPhongTro.Dialog;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.Model;

namespace QuanLyPhongTro.ViewModel
{
    public class ContractViewModel : BaseViewModel
    {
        public ObservableCollection<Contract> AllContract;
        private ObservableCollection<Contract> _ContractList;
        private ObservableCollection<ContractStatusDisplay> _ContractStatusList;
        private ContractStatusDisplay _SelectedContractStatus;
        private ObservableCollection<Tenant> _TenantList;
        private Contract _SelectedContract;
        private Room _SelectedRoom;
        private Tenant _SelectedTenant;
        private DateTime? _StartDateSearch;
        private DateTime? _EndDateSearch;
        private string? _RoomNumberSearch;
        public string _ContractCode;
        public DateTime _StartDate;
        public DateTime _EndDate;
        public decimal _Deposit;
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
        public ObservableCollection<ContractStatusDisplay> ContractStatusList
        {
            get { return _ContractStatusList; }
            set
            {
                _ContractStatusList = value;
                OnPropertyChanged();
            }
        }
        public ContractStatusDisplay SelectedContractStatus
        {
            get { return _SelectedContractStatus; }
            set
            {
                _SelectedContractStatus = value;
                OnPropertyChanged();
            }
        }
        public Contract SelectedContract
        {
            get { return _SelectedContract; }
            set
            {
                _SelectedContract = value;
                OnPropertyChanged();
                if (SelectedContract != null)
                {
                    IsEditable = false;
                    ContractCode = SelectedContract.ContractCode;
                    StartDate = SelectedContract.StartDate;
                    EndDate = SelectedContract.EndDate;
                    Deposit = SelectedContract.Deposit;
                    SelectedRoom = SelectedContract.Room;
                    SelectedTenant = SelectedContract.Tenant;
                    Status = ContractStatusList.FirstOrDefault(x => x.Value == SelectedContract.Status);
                    Notes = SelectedContract.Notes;
                }
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
        public ObservableCollection<Tenant> TenantList
        {
            get { return _TenantList; }
            set
            {
                _TenantList = value;
                OnPropertyChanged();
            }
        }
        public DateTime? StartDateSearch
        {
            get { return _StartDateSearch; }
            set
            {
                _StartDateSearch = value;
                OnPropertyChanged();
            }
        }
        public DateTime? EndDateSearch
        {
            get { return _EndDateSearch; }
            set
            {
                _EndDateSearch = value;
                OnPropertyChanged();
            }
        }
        public string? RoomNumberSearch
        {
            get { return _RoomNumberSearch; }
            set
            {
                _RoomNumberSearch = value;
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
        public decimal Deposit
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
        private bool _isEditable = true;
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }

        #region
        public ICommand SearchContractCommand { get; set; }
        public ICommand AddContractCommand { get; set; }
        public ICommand UpdateContractCommand { get; set; }
        public ICommand DeleteContractCommand { get; set; }
        #endregion
        public ContractViewModel()
        {
            AllContract = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.ToList());
            ContractList = new ObservableCollection<Contract>(DataProvider.Ins.DB.Contracts.Include(c => c.Room).Include(c => c.Tenant).ToList());
            ContractStatusList = ContractStatusOptions.GetContractStatuses();
            TenantList = new ObservableCollection<Tenant>(DataProvider.Ins.DB.Tenants.ToList());

            SearchContractCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var contractFiltered = AllContract.ToList();

                if (SelectedContractStatus?.Value != null)
                    contractFiltered = contractFiltered.Where(c => c.Status == SelectedContractStatus.Value).ToList();

                if (!string.IsNullOrEmpty(RoomNumberSearch))
                    contractFiltered = contractFiltered.Where(c => c.Room.RoomNumber.Contains(RoomNumberSearch)).ToList();

                if (StartDateSearch != null)
                    contractFiltered = contractFiltered.Where(c => c.StartDate.Date >= StartDateSearch.Value.Date).ToList();

                if (EndDateSearch != null)
                    contractFiltered = contractFiltered.Where(c => c.EndDate.Date <= EndDateSearch.Value.Date).ToList();

                ContractList = new ObservableCollection<Contract>(contractFiltered);
            });
            AddContractCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    var viewModel = new AddContractViewModel();
                    var addContractWindow = new AddContractWindow
                    {
                        DataContext = viewModel
                    };

                    addContractWindow.ShowDialog();
                    var contract = DataProvider.Ins.DB.Contracts.FirstOrDefault(x => x.Id == viewModel.Id);
                    if (contract != null && !ContractList.Contains(contract))
                    {
                        ContractList.Add(contract);
                        OnPropertyChanged();
                    }
                });
            UpdateContractCommand = new RelayCommand<object>(
                (p) =>
                {
                    if (SelectedContract == null)
                        return false;
                    return true;
                },
                (p) =>
                {
                    if (StartDate > EndDate || StartDate > DateTime.Now)
                    {
                        MessageBox.Show("Ngày ký hợp đồng không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (Deposit < 0)
                    {
                        MessageBox.Show("Tiền cọc không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    SelectedContract.ContractCode = ContractCode;
                    SelectedContract.StartDate = StartDate;
                    SelectedContract.EndDate = EndDate;
                    SelectedContract.Deposit = Deposit;
                    SelectedContract.RoomId = SelectedRoom.Id;
                    SelectedContract.TenantId = SelectedTenant.Id;
                    SelectedContract.Notes = Notes;
                    SelectedContract.Status = Status.Value;
                    if (Status.Value == ContractStatus.CancelledOrExpired)
                    {
                        SelectedContract.Room.Status = RoomFilterStatus.Vacant;
                    }
                        DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật hợp đồng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                });
        }
    }
}
