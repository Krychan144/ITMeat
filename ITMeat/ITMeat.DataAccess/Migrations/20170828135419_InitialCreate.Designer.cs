﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ITMeat.DataAccess.Context;

namespace ITMeat.DataAccess.Migrations
{
    [DbContext(typeof(ITMeatDbContext))]
    [Migration("20170828135419_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ITMeat.DataAccess.Models.Adds", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<decimal>("Expense")
                        .HasColumnType("DECIMAL(16,2)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<Guid>("PubId");

                    b.HasKey("Id");

                    b.HasIndex("PubId");

                    b.ToTable("Adds");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.EmailMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("FailError");

                    b.Property<string>("FailErrorMessage");

                    b.Property<int>("FailureCount");

                    b.Property<string>("From");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Recipient")
                        .IsRequired();

                    b.Property<string>("Subject")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("EmailMessages");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Meal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<decimal>("Expense")
                        .HasColumnType("DECIMAL(16,2)");

                    b.Property<Guid>("MealTypeId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<Guid>("PubId");

                    b.HasKey("Id");

                    b.HasIndex("MealTypeId");

                    b.HasIndex("PubId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.MealType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MealType");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("EndDateTime");

                    b.Property<decimal>("Expense")
                        .HasColumnType("DECIMAL(16 ,2)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("OwnerId");

                    b.Property<DateTime?>("SubmitDateTime");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Pub", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<decimal>("FreeDelivery")
                        .HasColumnType("DECIMAL(16 ,2)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<decimal>("Phone")
                        .HasColumnType("DECIMAL(11,0)");

                    b.HasKey("Id");

                    b.ToTable("Pub");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.PubOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("PubId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("PubId");

                    b.ToTable("PubOrder");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<DateTime?>("EmailConfirmedOn");

                    b.Property<DateTime?>("LockedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(128)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<decimal>("Expense")
                        .HasColumnType("DECIMAL(16 ,2)");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<Guid>("OrderId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("UserOrders");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrderAdds", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddsId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("Quantity");

                    b.Property<Guid>("UserOrderId");

                    b.HasKey("Id");

                    b.HasIndex("AddsId");

                    b.HasIndex("UserOrderId");

                    b.ToTable("UserOrderAdds");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrderMeal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<Guid>("MealId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("Quantity");

                    b.Property<Guid>("UserOrderId");

                    b.HasKey("Id");

                    b.HasIndex("MealId");

                    b.HasIndex("UserOrderId");

                    b.ToTable("UserOrderMeal");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("SecretToken")
                        .HasColumnType("NVARCHAR(256)");

                    b.Property<DateTime?>("SecretTokenTimeStamp")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Adds", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.Pub", "Pub")
                        .WithMany()
                        .HasForeignKey("PubId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Meal", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.MealType", "MealType")
                        .WithMany()
                        .HasForeignKey("MealTypeId");

                    b.HasOne("ITMeat.DataAccess.Models.Pub", "Pub")
                        .WithMany("Meals")
                        .HasForeignKey("PubId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.Order", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.PubOrder", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("ITMeat.DataAccess.Models.Pub", "Pub")
                        .WithMany("PubOrders")
                        .HasForeignKey("PubId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrder", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.HasOne("ITMeat.DataAccess.Models.User", "User")
                        .WithMany("UserOrders")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrderAdds", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.Adds", "Adds")
                        .WithMany()
                        .HasForeignKey("AddsId");

                    b.HasOne("ITMeat.DataAccess.Models.UserOrder", "UserOrder")
                        .WithMany("OrdersAdds")
                        .HasForeignKey("UserOrderId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserOrderMeal", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId");

                    b.HasOne("ITMeat.DataAccess.Models.UserOrder", "UserOrder")
                        .WithMany("OrdersMeals")
                        .HasForeignKey("UserOrderId");
                });

            modelBuilder.Entity("ITMeat.DataAccess.Models.UserToken", b =>
                {
                    b.HasOne("ITMeat.DataAccess.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId");
                });
        }
    }
}
