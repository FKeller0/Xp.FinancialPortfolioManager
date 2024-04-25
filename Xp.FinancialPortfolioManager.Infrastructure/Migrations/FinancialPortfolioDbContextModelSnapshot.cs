﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Xp.FinancialPortfolioManager.Infrastructure.Common.Persistence;

#nullable disable

namespace Xp.FinancialPortfolioManager.Infrastructure.Migrations
{
    [DbContext(typeof(FinancialPortfolioDbContext))]
    partial class FinancialPortfolioDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Admin.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2150e333-8fdc-42a3-9474-1a3956d46de8")
                        });
                });

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Advisors.Advisor", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Advisors");
                });

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Clients.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AdvisorId")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Balance")
                        .HasColumnType("REAL");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AdvisorId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AdvisorId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("_passwordHash")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PasswordHash");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1b41fae8-e02c-448b-944d-6169ebc26ddf"),
                            AdminId = new Guid("2150e333-8fdc-42a3-9474-1a3956d46de8"),
                            Email = "financialportfolio@xpfinancialportfolio.com",
                            FirstName = "Sys",
                            LastName = "Admin",
                            _passwordHash = "$2a$11$fm.DCQn2.Cq7PlPfhwWDuO7pgEjGqs0pCf1P5Q24VejUqc.XxahLm"
                        });
                });

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Clients.Client", b =>
                {
                    b.HasOne("Xp.FinancialPortfolioManager.Domain.Advisors.Advisor", "Advisor")
                        .WithMany("Clients")
                        .HasForeignKey("AdvisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Advisor");
                });

            modelBuilder.Entity("Xp.FinancialPortfolioManager.Domain.Advisors.Advisor", b =>
                {
                    b.Navigation("Clients");
                });
#pragma warning restore 612, 618
        }
    }
}
