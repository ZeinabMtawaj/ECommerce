﻿// <auto-generated />
using System;
using ApplicationDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplicationDbContext.Migrations
{
    [DbContext(typeof(ECommerceDBContext))]
    [Migration("20221206194850_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationDbContext.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.CategorySpecificationValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecificationId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SpecificationId");

                    b.ToTable("CategorySpecificationValue", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Active")
                        .HasColumnType("int")
                        .HasColumnName("active");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal?>("DiscountPercent")
                        .HasColumnType("decimal(18,0)")
                        .HasColumnName("discount_percent");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.ToTable("Discount", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .IsFixedLength();

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.ToTable("Notification", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(15,5)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Photo", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(15,5)");

                    b.Property<int?>("ProductGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("SKU");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductGroupId");

                    b.HasIndex(new[] { "Sku" }, "IX_Product")
                        .IsUnique()
                        .HasFilter("[SKU] IS NOT NULL");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("UpdatedAt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("Updated_at")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("DiscountId");

                    b.ToTable("ProductGroup", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(15,5)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex(new[] { "ProductId", "OrderId" }, "IX_ProductOrder")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL AND [OrderId] IS NOT NULL");

                    b.ToTable("ProductOrder", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductSpecificationValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecificationId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SpecificationId");

                    b.ToTable("ProductSpecificationValue", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating1")
                        .HasColumnType("int")
                        .HasColumnName("Rating");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<byte[]>("Time")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "OrderId" }, "IX_Shipping")
                        .IsUnique()
                        .HasFilter("[OrderId] IS NOT NULL");

                    b.HasIndex(new[] { "AddressId" }, "IX_Shipping_1")
                        .IsUnique();

                    b.ToTable("Shipping", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Specification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Specification", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Trend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "ProductId" }, "IX_Trend")
                        .IsUnique();

                    b.ToTable("Trend", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Customer")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.UserHasNotification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NotificationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "NotificationId", "UserId" }, "IX_UserHasNotification")
                        .IsUnique();

                    b.ToTable("UserHasNotification", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.WishList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Created_at");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime")
                        .HasColumnName("Updated_at");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex(new[] { "ProductId", "UserId" }, "IX_WishList")
                        .IsUnique();

                    b.ToTable("WishList", (string)null);
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Address", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Address_User");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.CategorySpecificationValue", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Category", "Category")
                        .WithMany("CategorySpecificationValues")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_CategorySpecificationValue_Category");

                    b.HasOne("ApplicationDbContext.Models.Specification", "Specification")
                        .WithMany("CategorySpecificationValues")
                        .HasForeignKey("SpecificationId")
                        .HasConstraintName("FK_CategorySpecificationValue_Specification");

                    b.Navigation("Category");

                    b.Navigation("Specification");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Order", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Order_Customer");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Photo", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Photo_Product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Product", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Product_Category");

                    b.HasOne("ApplicationDbContext.Models.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("ProductGroupId")
                        .HasConstraintName("FK_Product_ProductGroup");

                    b.Navigation("Category");

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductGroup", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Discount", "Discount")
                        .WithMany("ProductGroups")
                        .HasForeignKey("DiscountId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductGroup_Discount");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductOrder", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_ProductOrder_Order");

                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductOrder_Product");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductSpecificationValue", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithMany("ProductSpecificationValues")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductSpecificationValue_Product");

                    b.HasOne("ApplicationDbContext.Models.Specification", "Specification")
                        .WithMany("ProductSpecificationValues")
                        .HasForeignKey("SpecificationId")
                        .HasConstraintName("FK_ProductSpecificationValue_Specification");

                    b.Navigation("Product");

                    b.Navigation("Specification");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Rating", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Rating_Product");

                    b.HasOne("ApplicationDbContext.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Rating_Customer");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Shipping", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Address", "Address")
                        .WithOne("Shipping")
                        .HasForeignKey("ApplicationDbContext.Models.Shipping", "AddressId")
                        .IsRequired()
                        .HasConstraintName("FK_Shipping_Address");

                    b.HasOne("ApplicationDbContext.Models.Order", "Order")
                        .WithOne("Shipping")
                        .HasForeignKey("ApplicationDbContext.Models.Shipping", "OrderId")
                        .HasConstraintName("FK_Shipping_Order");

                    b.Navigation("Address");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Trend", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithOne("Trend")
                        .HasForeignKey("ApplicationDbContext.Models.Trend", "ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Trend_Product");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.UserHasNotification", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Notification", "Notification")
                        .WithMany("UserHasNotifications")
                        .HasForeignKey("NotificationId")
                        .IsRequired()
                        .HasConstraintName("FK_UserHasNotification_Notification");

                    b.HasOne("ApplicationDbContext.Models.User", "User")
                        .WithMany("UserHasNotifications")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserHasNotification_Customer");

                    b.Navigation("Notification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.WishList", b =>
                {
                    b.HasOne("ApplicationDbContext.Models.Product", "Product")
                        .WithMany("WishLists")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_WishList_Product");

                    b.HasOne("ApplicationDbContext.Models.User", "User")
                        .WithMany("WishLists")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_WishList_User");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Address", b =>
                {
                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Category", b =>
                {
                    b.Navigation("CategorySpecificationValues");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Discount", b =>
                {
                    b.Navigation("ProductGroups");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Notification", b =>
                {
                    b.Navigation("UserHasNotifications");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Order", b =>
                {
                    b.Navigation("ProductOrders");

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Product", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("ProductOrders");

                    b.Navigation("ProductSpecificationValues");

                    b.Navigation("Ratings");

                    b.Navigation("Trend");

                    b.Navigation("WishLists");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.ProductGroup", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.Specification", b =>
                {
                    b.Navigation("CategorySpecificationValues");

                    b.Navigation("ProductSpecificationValues");
                });

            modelBuilder.Entity("ApplicationDbContext.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");

                    b.Navigation("Ratings");

                    b.Navigation("UserHasNotifications");

                    b.Navigation("WishLists");
                });
#pragma warning restore 612, 618
        }
    }
}
