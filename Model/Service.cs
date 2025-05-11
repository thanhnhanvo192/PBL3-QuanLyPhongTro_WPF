using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyPhongTro.ViewModel;

namespace QuanLyPhongTro.Model
{
    [Table("Services")]
    public class Service : BaseViewModel
    {
        private string _DisplayName;
        private decimal _Price;
        private int _UnitId;
        private Unit _Unit;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                _DisplayName = value;
                OnPropertyChanged();
            }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price 
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged();
            }
        }

        public int UnitId
        {
            get { return _UnitId; }
            set
            {
                _UnitId = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("UnitId")]
        public virtual Unit Unit
        {
            get { return _Unit; }
            set
            {
                _Unit = value;
                OnPropertyChanged();
            }
        }

        // Navigation property: Một Service có thể thuộc nhiều Invoice_details
        public virtual ICollection<Invoice_detail> InvoiceDetails { get; set; }

        public Service()
        {
            InvoiceDetails = new HashSet<Invoice_detail>();
            Price = 0;
        }
    }
}
