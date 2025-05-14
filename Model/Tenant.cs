using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.Enum;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.Model
{
    [Table("Tenants")] // Khách thuê
    public class Tenant : BaseViewModel
    {
        private string _FirstName;
        private string _LastName;
        private string _CCCD;
        private DateTime? _Birthday;
        private SexEnum _Sex;
        private string? _Email;
        private string? _Phone;
        private string? _PermanentAddress;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
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
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }
        [Required]
        [MaxLength(20)]
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
        public SexEnum Sex
        {
            get { return _Sex; }
            set
            {
                _Sex = value;
                OnPropertyChanged();
            }
        }
        [MaxLength(20)]
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
        [EmailAddress]
        public string? Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
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

        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Fix> Fixes { get; set; }

        public Tenant()
        {
            Contracts = new HashSet<Contract>();
            Fixes = new HashSet<Fix>();
        }
    }
}
