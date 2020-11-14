﻿// <auto-generated />
using System;
using CQRSSplitWise.Client.Command.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CQRSSplitWise.Client.Command.Migrations
{
	[DbContext(typeof(SplitWiseSQLContext))]
    [Migration("20201016082250_TestMigration3")]
    partial class TestMigration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.GroupUser", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupUsers");
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DestinationWalletId")
                        .HasColumnType("int");

                    b.Property<int?>("SourceWalletId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("DestinationWalletId");

                    b.HasIndex("SourceWalletId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FirstName = "Paško",
                            LastName = "Patak"
                        },
                        new
                        {
                            UserId = 2,
                            FirstName = "Crni",
                            LastName = "Stvor"
                        });
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");

                    b.HasData(
                        new
                        {
                            WalletId = 1,
                            Name = "Credit",
                            UserId = 1
                        },
                        new
                        {
                            WalletId = 2,
                            Name = "Credit",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.GroupUser", b =>
                {
                    b.HasOne("CQRSSplitWise.DAL.Models.Group", "Group")
                        .WithMany("GroupUsers")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CQRSSplitWise.DAL.Models.User", "User")
                        .WithMany("GroupUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.Transaction", b =>
                {
                    b.HasOne("CQRSSplitWise.DAL.Models.Wallet", "DestinationWallet")
                        .WithMany()
                        .HasForeignKey("DestinationWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CQRSSplitWise.DAL.Models.Wallet", "SourceWallet")
                        .WithMany()
                        .HasForeignKey("SourceWalletId");
                });

            modelBuilder.Entity("CQRSSplitWise.DAL.Models.Wallet", b =>
                {
                    b.HasOne("CQRSSplitWise.DAL.Models.User", "User")
                        .WithOne("Wallet")
                        .HasForeignKey("CQRSSplitWise.DAL.Models.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
