﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20231002064438_add_registerr_model")]
    partial class add_registerr_model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Model.Education", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Degree")
                        .HasColumnType("int");

                    b.Property<string>("GPA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("University_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("University_id");

                    b.ToTable("Educations");
                });

            modelBuilder.Entity("API.Model.Profilling", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Education_id")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.HasIndex("Education_id");

                    b.ToTable("profillings");
                });

            modelBuilder.Entity("API.Model.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("universities");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NIK");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FisrtName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("salary")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.HasIndex("Phone", "Email")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("API.Model.Education", b =>
                {
                    b.HasOne("API.Model.University", "universities")
                        .WithMany("Educations")
                        .HasForeignKey("University_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("universities");
                });

            modelBuilder.Entity("API.Model.Profilling", b =>
                {
                    b.HasOne("API.Model.Education", "Educations")
                        .WithMany("profillings")
                        .HasForeignKey("Education_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Account", "Accounts")
                        .WithOne("profillings")
                        .HasForeignKey("API.Model.Profilling", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Accounts");

                    b.Navigation("Educations");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.HasOne("Employee", "employees")
                        .WithOne("Accounts")
                        .HasForeignKey("Account", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("employees");
                });

            modelBuilder.Entity("API.Model.Education", b =>
                {
                    b.Navigation("profillings");
                });

            modelBuilder.Entity("API.Model.University", b =>
                {
                    b.Navigation("Educations");
                });

            modelBuilder.Entity("Account", b =>
                {
                    b.Navigation("profillings")
                        .IsRequired();
                });

            modelBuilder.Entity("Employee", b =>
                {
                    b.Navigation("Accounts")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
