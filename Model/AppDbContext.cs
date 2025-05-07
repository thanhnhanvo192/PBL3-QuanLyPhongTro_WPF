using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QuanLyPhongTro.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Service> Services { get; set; }
        // DbSet cho Role, Account, User, Location đã được bỏ
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Invoice_detail> Invoice_details { get; set; }
        public DbSet<Fix> Fixes { get; set; }
        public DbSet<Punish> Punishes { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Occupant> Occupants { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["QuanLyPhongTroDB"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.UserName)
                .IsUnique(); // Đảm bảo UserName là duy nhất

            // Cấu hình Fluent API
            // Các cấu hình cho Role, Account, User đã được bỏ

            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.CCCD)
                .IsUnique();
            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Phone)
                .IsUnique();
            modelBuilder.Entity<Tenant>()
                .HasIndex(t => t.Email)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber) // Số phòng giờ là unique trên toàn hệ thống
                .IsUnique();

            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.ContractCode)
                .IsUnique();

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.InvoiceCode)
                .IsUnique();

            modelBuilder.Entity<MeterReading>()
                .HasIndex(m => new { m.RoomId, m.ServiceId, m.ReadingDate })
                .IsUnique();

            modelBuilder.Entity<Occupant>()
                .HasIndex(o => o.CCCD)
                .IsUnique()
                .HasFilter("[CCCD] IS NOT NULL");

            // Seed data cho Unit vẫn giữ lại
            modelBuilder.Entity<Unit>().HasData(
                new Unit { UnitId = 1, DisplayName = "kWh" },
                new Unit { UnitId = 2, DisplayName = "m3" },
                new Unit { UnitId = 3, DisplayName = "Phòng/Tháng" },
                new Unit { UnitId = 4, DisplayName = "Người/Tháng" },
                new Unit { UnitId = 5, DisplayName = "Tháng" }
            );
            // Seed data cho Role đã được bỏ
        }
    }
}
