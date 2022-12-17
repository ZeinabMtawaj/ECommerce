using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ApplicationDbContext.Models
{
    public partial class ECommerceDBContext : IdentityDbContext<User,UserRole,int>
    {
        //public ECommerceDBContext()
        //{
        //}

        public ECommerceDBContext(DbContextOptions<ECommerceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<CategorySpecificationValue> CategorySpecificationValues { get; set; } = null!;
        public virtual DbSet<Discount> Discounts { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductGroup> ProductGroups { get; set; } = null!;
        public virtual DbSet<ProductOrder> ProductOrders { get; set; } = null!;
        public virtual DbSet<ProductSpecificationValue> ProductSpecificationValues { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Shipping> Shippings { get; set; } = null!;
        public virtual DbSet<Specification> Specifications { get; set; } = null!;
        public virtual DbSet<Trend> Trends { get; set; } = null!;
        //public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserHasNotification> UserHasNotifications { get; set; } = null!;
        public virtual DbSet<WishList> WishLists { get; set; } = null!;

        public override int SaveChanges()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
                else
                    Entry(((BaseEntity)entityEntry.Entity)).Property(x => x.CreatedDate).IsModified = false;
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-2DNG6PA\\SQLEXPRESS;Database=ECommerceDB;User Id=sa;Password=12345678;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Location)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Address_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");
            });

            modelBuilder.Entity<CategorySpecificationValue>(entity =>
            {
                entity.ToTable("CategorySpecificationValue");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategorySpecificationValues)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_CategorySpecificationValue_Category");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.CategorySpecificationValues)
                    .HasForeignKey(d => d.SpecificationId)
                    .HasConstraintName("FK_CategorySpecificationValue_Specification");
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.DiscountPercent)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("discount_percent");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Content)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Path)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Photos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Photo_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasIndex(e => e.Sku, "IX_Product")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Image)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.Sku)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SKU");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.ProductGroup)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductGroupId)
                    .HasConstraintName("FK_Product_ProductGroup");
            });

            modelBuilder.Entity<ProductGroup>(entity =>
            {
                entity.ToTable("ProductGroup");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.ProductGroups)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ProductGroup_Discount");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.ToTable("ProductOrder");

                entity.HasIndex(e => new { e.ProductId, e.OrderId }, "IX_ProductOrder")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Price).HasColumnType("decimal(15, 5)");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ProductOrder_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductOrder_Product");
            });

            modelBuilder.Entity<ProductSpecificationValue>(entity =>
            {
                entity.ToTable("ProductSpecificationValue");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);

               entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSpecificationValues)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductSpecificationValue_Product");

                entity.HasOne(d => d.Specification)
                    .WithMany(p => p.ProductSpecificationValues)
                    .HasForeignKey(d => d.SpecificationId)
                    .HasConstraintName("FK_ProductSpecificationValue_Specification");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("Rating");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Rating_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Rating_Customer");
            });

            modelBuilder.Entity<Shipping>(entity =>
            {
                entity.ToTable("Shipping");

                entity.HasIndex(e => e.OrderId, "IX_Shipping")
                    .IsUnique();

                entity.HasIndex(e => e.AddressId, "IX_Shipping_1")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Address)
                    .WithOne(p => p.Shipping)
                    .HasForeignKey<Shipping>(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Shipping_Address");

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Shipping)
                    .HasForeignKey<Shipping>(d => d.OrderId)
                    .HasConstraintName("FK_Shipping_Order");
            });

            modelBuilder.Entity<Specification>(entity =>
            {
                entity.ToTable("Specification");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                      .HasColumnType("datetime")
                      .HasColumnName("CreatedDate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");
            });

            modelBuilder.Entity<Trend>(entity =>
            {
                entity.ToTable("Trend");

                entity.HasIndex(e => e.ProductId, "IX_Trend")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Product)
                    .WithOne(p => p.Trend)
                    .HasForeignKey<Trend>(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Trend_Product");
            });

            //modelBuilder.Entity<User>(entity =>
            //{
            //    entity.ToTable("User");

            //    entity.HasIndex(e => e.Email, "IX_Customer")
            //        .IsUnique();

            //    entity.Property(e => e.CreatedDate)
            //        .HasColumnType("datetime")
            //        .HasColumnName("CreatedDate");

            //    entity.Property(e => e.Email)
            //        .HasMaxLength(200)
            //        .IsUnicode(false);

            //    entity.Property(e => e.FirstName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.LastName)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.Password)
            //        .HasMaxLength(200)
            //        .IsUnicode(false);

            //    entity.Property(e => e.PhoneNumber)
            //        .HasMaxLength(50)
            //        .IsUnicode(false);

            //    entity.Property(e => e.UpdatedDate)
            //        .HasColumnType("datetime")
            //        .HasColumnName("UpdatedDate");
            //});

            modelBuilder.Entity<UserHasNotification>(entity =>
            {
                entity.ToTable("UserHasNotification");

                entity.Property(e => e.CreatedDate)
                      .HasColumnType("datetime")
                      .HasColumnName("CreatedDate");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasIndex(e => new { e.NotificationId, e.UserId }, "IX_UserHasNotification")
                    .IsUnique();

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.UserHasNotifications)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserHasNotification_Notification");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHasNotifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserHasNotification_Customer");
            });

            modelBuilder.Entity<WishList>(entity =>
            {
                entity.ToTable("WishList");

                entity.HasIndex(e => new { e.ProductId, e.UserId }, "IX_WishList")
                    .IsUnique();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CreatedDate");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UpdatedDate");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_WishList_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WishLists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_WishList_User");
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("User", "dbo");
            modelBuilder.Entity<UserRole>().ToTable("Role", "dbo");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_token", "dbo");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_role", "dbo");
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claim", "dbo");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claim", "dbo");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("user_login", "dbo");

            OnModelCreatingPartial(modelBuilder);
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
