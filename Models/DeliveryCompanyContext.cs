using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace deliveryCompany.Models;

public partial class DeliveryCompanyContext : DbContext
{
    public DeliveryCompanyContext()
    {
    }

    public DeliveryCompanyContext(DbContextOptions<DeliveryCompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectModels;Database=deliveryCompany;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB857706A1FE");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerPhone)
            .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("customer_phone");
            entity.Property(e => e.CustomerEmail)
            .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_email");
            entity.Property(e => e.CustomerAddress)
            .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer_address");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.MaintenanceId).HasName("PK__Maintena__9D754BEAE84FD767");

            entity.ToTable("Maintenance");

            entity.Property(e => e.MaintenanceId)
                .ValueGeneratedNever()
                .HasColumnName("maintenance_id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.DescriptionMaintenance)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description_maintenance");
            entity.Property(e => e.MaintenanceDate).HasColumnName("maintenance_date");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK__Maintenan__vehic__2C3393D0");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__465962292DC1FEB8");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnType("float").HasColumnName("price");
            entity.Property(e => e.AssignedDriverId).HasColumnName("assigned_driver_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("order_status");

            entity.HasOne(d => d.AssignedDriver).WithMany(p => p.OrdersNavigation)
                .HasForeignKey(d => d.AssignedDriverId)
                .HasConstraintName("FK__Orders__assigned__32E0915F");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__customer__31EC6D26");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FB32BF5FC");

            entity.HasIndex(e => e.UserName, "UQ__Users__7C9273C4BB9A8F70").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164BCB43FA7").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");
            entity.Property(e => e.UserType).HasColumnName("user_type");

            entity.HasMany(d => d.Orders).WithMany(p => p.Drivers)
                .UsingEntity<Dictionary<string, object>>(
                    "DriverOrder",
                    r => r.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DriverOrd__order__36B12243"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DriverOrd__drive__35BCFE0A"),
                    j =>
                    {
                        j.HasKey("DriverId", "OrderId").HasName("PK__DriverOr__2074539F02862ADB");
                        j.ToTable("DriverOrders");
                        j.IndexerProperty<int>("DriverId").HasColumnName("driver_id");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                    });
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__F2947BC1FED048A9");

            entity.Property(e => e.VehicleId)
                .ValueGeneratedNever()
                .HasColumnName("vehicle_id");
            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("vehicle_number");
            entity.Property(e => e.VehicleStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Available")
                .HasColumnName("vehicle_status");
            entity.Property(e => e.YearModel).HasColumnName("year_model");

            entity.HasOne(d => d.Driver).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Vehicles__driver__29572725");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
