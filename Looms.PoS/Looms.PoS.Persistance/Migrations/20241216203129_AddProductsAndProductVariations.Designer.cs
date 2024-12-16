﻿// <auto-generated />
using System;
using Looms.PoS.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Looms.PoS.Persistance.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241216203129_AddProductsAndProductVariations")]
    partial class AddProductsAndProductVariations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Looms.PoS.Domain.Daos.BusinessDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EndHour")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StartHour")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.DiscountDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("DiscountType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Target")
                        .HasColumnType("integer");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.GiftCardDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("InitialBalance")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("IssuedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("GiftCards");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<Guid?>("GiftCardId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<Guid?>("PaymentTerminalId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Tip")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("GiftCardId");

                    b.HasIndex("PaymentTerminalId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentProviderDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("WebhookSecret")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("PaymentProviders");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentTerminalDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PaymentProviderId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderId");

                    b.ToTable("PaymentTerminals");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ProductDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<Guid>("TaxId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("TaxId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ProductVariationDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("VariationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariations");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.RefundDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ProcessedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RefundReason")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RefundStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Refunds");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ReservationDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AppointmentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ServiceDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DurationMin")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid>("TaxId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("TaxId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.TaxDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Percentage")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TaxCategory")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.UserDao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BusinessId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("Role")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.GiftCardDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.BusinessDao", "Business")
                        .WithMany()
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.GiftCardDao", "GiftCard")
                        .WithMany("Payments")
                        .HasForeignKey("GiftCardId");

                    b.HasOne("Looms.PoS.Domain.Daos.PaymentTerminalDao", "PaymentTerminal")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentTerminalId");

                    b.Navigation("GiftCard");

                    b.Navigation("PaymentTerminal");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentProviderDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.BusinessDao", "Business")
                        .WithMany("PaymentProviders")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentTerminalDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.PaymentProviderDao", "PaymentProvider")
                        .WithMany("PaymentTerminals")
                        .HasForeignKey("PaymentProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentProvider");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ProductDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.BusinessDao", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Looms.PoS.Domain.Daos.TaxDao", "Tax")
                        .WithMany("Products")
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ProductVariationDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.ProductDao", "Product")
                        .WithMany("ProductVariations")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ReservationDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.UserDao", "Employee")
                        .WithMany("Reservations")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Looms.PoS.Domain.Daos.ServiceDao", "Service")
                        .WithMany("Reservations")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ServiceDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.BusinessDao", "Business")
                        .WithMany("Services")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Looms.PoS.Domain.Daos.TaxDao", "Tax")
                        .WithMany("Services")
                        .HasForeignKey("TaxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");

                    b.Navigation("Tax");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.UserDao", b =>
                {
                    b.HasOne("Looms.PoS.Domain.Daos.BusinessDao", "Business")
                        .WithMany("Users")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.BusinessDao", b =>
                {
                    b.Navigation("PaymentProviders");

                    b.Navigation("Products");

                    b.Navigation("Services");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.GiftCardDao", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentProviderDao", b =>
                {
                    b.Navigation("PaymentTerminals");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.PaymentTerminalDao", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ProductDao", b =>
                {
                    b.Navigation("ProductVariations");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.ServiceDao", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.TaxDao", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("Looms.PoS.Domain.Daos.UserDao", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
