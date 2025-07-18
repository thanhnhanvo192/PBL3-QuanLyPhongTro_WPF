﻿using System;
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
    [Table("Contracts")]
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string ContractCode { get; set; } // Unique
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Deposit { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public int TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenant { get; set; }
        // UserId và CreatedByUser đã được bỏ
        public ContractStatus Status { get; set; } // 0: Hết hạn/Huỷ, 1: Đang hiệu lực
        public string Notes { get; set; }
        public bool IsDeleted { get; set; } // Đánh dấu hợp đồng đã bị xoá
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Occupant> Occupants { get; set; }
        public virtual ICollection<Punish> Punishes { get; set; }

        public Contract()
        {
            Invoices = new HashSet<Invoice>();
            Occupants = new HashSet<Occupant>();
            Punishes = new HashSet<Punish>();
            Status = ContractStatus.Active; // Mặc định là đang hiệu lực
            Deposit = 0;
        }
    }

}
