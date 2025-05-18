using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.Model
{
    [Table("Occupants")] // Người ở cùng
    public class Occupant : BaseViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }
        [MaxLength(100)]
        public string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        [Required]
        [MaxLength(100)]
        public string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        [MaxLength(20)]
        public string _CCCD;
        public string CCCD
        {
            get { return _CCCD; }
            set
            {
                _CCCD = value;
                OnPropertyChanged();
            }
        }
        public DateTime? _Birthday;
        public DateTime? Birthday
        {
            get { return _Birthday; }
            set
            {
                _Birthday = value;
                OnPropertyChanged();
            }
        }
        public SexEnum? _Sex;
        public SexEnum? Sex
        {
            get { return _Sex; }
            set
            {
                _Sex = value;
                OnPropertyChanged();
            }
        }
        [MaxLength(20)]
        public string? _Phone;
        public string? Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                OnPropertyChanged();
            }
        }
        [MaxLength(100)]
        public string? _PermanentAddress;
        public string? PermanentAddress
        {
            get { return _PermanentAddress; }
            set
            {
                _PermanentAddress = value;
                OnPropertyChanged();
            }
        }
        [NotMapped]
        public string DisplayName
        {
            get { return $"{LastName} {FirstName}"; }
        }
    }
}
