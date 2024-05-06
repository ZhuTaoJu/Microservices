﻿// <auto-generated />
using System;
using Email.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Email.API.Infrastructure.EntityMigration
{
    [DbContext(typeof(EmailDBContext))]
    partial class EmailDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Email.API.Domain.AggregatesModel.EmailAggregate.EmailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CallName")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<DateTime?>("CreateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreateUser")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Email")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Phone")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UpdateUser")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Email", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
