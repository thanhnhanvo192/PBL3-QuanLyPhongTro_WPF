using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class OccupantViewModel : BaseViewModel
    {
        private ObservableCollection<Room> _ListRoom;
        private Room _SelectedRoom;
        private SexOptionDisplay _SelectedSex;
        private ObservableCollection<SexOptionDisplay> _ListSexOption;

        public ObservableCollection<Room> ListRoom
        {
            get { return _ListRoom; }
            set
            {
                _ListRoom = value;
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
        public SexOptionDisplay SelectedSex
        {
            get { return _SelectedSex; }
            set
            {
                _SelectedSex = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SexOptionDisplay> ListSexOption
        {
            get { return _ListSexOption; }
            set
            {
                _ListSexOption = value;
                OnPropertyChanged();
            }
        }
        private string _FirstName;
        private string _LastName;
        private string _CCCD;
        private SexEnum Sex;
        private DateTime? _Birthday;
        private string? _Phone;
        private string? _PermanentAddress;
        private string _OccupantNameSearch;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        public string CCCD
        {
            get { return _CCCD; }
            set
            {
                _CCCD = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set
            {
                _Birthday = value;
                OnPropertyChanged();
            }
        }
        public string? Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }
        public string? PermanentAddress
        {
            get { return _PermanentAddress; }
            set
            {
                _PermanentAddress = value;
                OnPropertyChanged();
            }
        }
        public string OccupantNameSearch
        {
            get { return _OccupantNameSearch; }
            set
            {
                _OccupantNameSearch = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Occupant> _OccupantList;
        public ObservableCollection<Occupant> OccupantList
        {
            get { return _OccupantList; }
            set
            {
                _OccupantList = value;
                OnPropertyChanged();
            }
        }
        private Occupant _SelectedItem;
        public Occupant SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    FirstName = SelectedItem.FirstName;
                    LastName = SelectedItem.LastName;
                    CCCD = SelectedItem.CCCD;
                    Birthday = SelectedItem.Birthday;
                    Phone = SelectedItem.Phone;
                    SelectedSex = ListSexOption.FirstOrDefault(x => x.Value == SelectedItem.Sex);
                    PermanentAddress = SelectedItem.PermanentAddress;
                }
            }
        }
        public ICommand SearchCommand { get; set; }
        public ICommand AddOccupantCommand { get; set; }
        public ICommand AddNewOccupantCommand { get; set; }
        public ICommand UpdateOccupantCommand { get; set; }
        public ICommand SearchOccupantCommand { get; set; }
        public OccupantViewModel()
        {
            OccupantList = new ObservableCollection<Occupant>(DataProvider.Ins.DB.Occupants.ToList());
            ListSexOption = SexOptions.GetSexEnums();
            ListRoom = new ObservableCollection<Room>(DataProvider.Ins.DB.Rooms.Where(r => r.Status == RoomFilterStatus.Occupied).ToList());
            SelectedSex = ListSexOption.FirstOrDefault();
            AddOccupantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
           (p) =>
           {
               var addOccupantWindow = new AddOccupantWindow{};
               addOccupantWindow.ShowDialog();
           });
            UpdateOccupantCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;
            },
            (p) =>
            {
                var Occupant = DataProvider.Ins.DB.Occupants.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                var OccupantList = DataProvider.Ins.DB.Occupants.Where(x => x.Id != SelectedItem.Id && CCCD == SelectedItem.CCCD && Phone == SelectedItem.Phone);
                if (OccupantList == null || OccupantList.Count() == 0)
                {
                    MessageBox.Show("CCCD hoặc SĐT đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (Occupant != null)
                {
                    Occupant.FirstName = FirstName;
                    Occupant.LastName = LastName;
                    Occupant.CCCD = CCCD;
                    Occupant.Birthday = Birthday;
                    Occupant.Phone = Phone;
                    Occupant.PermanentAddress = PermanentAddress;
                    Occupant.Sex = SelectedSex.Value;
                    DataProvider.Ins.DB.SaveChanges();
                    SelectedItem.FirstName = FirstName;
                    SelectedItem.LastName = LastName;
                    SelectedItem.CCCD = CCCD;
                    SelectedItem.Birthday = Birthday;
                    SelectedItem.Phone = Phone;
                    SelectedItem.PermanentAddress = PermanentAddress;
                }
            });
            SearchOccupantCommand = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                var allOccupants = DataProvider.Ins.DB.Occupants.ToList();
                if (!string.IsNullOrEmpty(OccupantNameSearch))
                {
                    var Occupant = allOccupants.Where(x => x.LastName.ToLower().Contains(OccupantNameSearch.ToLower()) || x.FirstName.ToLower().Contains(OccupantNameSearch.ToLower())).ToList();
                    if (Occupant.Count > 0)
                    {
                        OccupantList.Clear();
                        foreach (var item in Occupant)
                        {
                            OccupantList.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    OccupantList.Clear();
                    foreach (var item in allOccupants)
                    {
                        OccupantList.Add(item);
                    }
                }
            });
            AddNewOccupantCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(CCCD) || SelectedSex == null)
                    return false;
                return true;
            },
           (p) =>
           {
               var existingOccupant = DataProvider.Ins.DB.Occupants.FirstOrDefault(x => x.CCCD == CCCD && CCCD != CCCD && Phone != Phone);
               if (existingOccupant != null)
               {
                   MessageBox.Show("CCCD đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                   return;
               }
               var contract = DataProvider.Ins.DB.Contracts.FirstOrDefault(x => x.RoomId == SelectedRoom.Id);
               Occupant newOccupant = new Occupant()
               {
                   FirstName = FirstName,
                   LastName = LastName,
                   CCCD = CCCD,
                   PermanentAddress = PermanentAddress,
                   Phone = Phone,
                   Birthday = Birthday,
                   ContractId = contract.Id,
                   Sex = SelectedSex.Value
               };
               DataProvider.Ins.DB.Occupants.Add(newOccupant);
               DataProvider.Ins.DB.SaveChanges();
               MessageBox.Show("Thêm khách thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
               OccupantList.Add(newOccupant);
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
