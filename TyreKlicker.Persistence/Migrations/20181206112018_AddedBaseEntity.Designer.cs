﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TyreKlicker.Persistence;

namespace TyreKlicker.Persistence.Migrations
{
    [DbContext(typeof(TyreKlickerDbContext))]
    [Migration("20181206112018_AddedBaseEntity")]
    partial class AddedBaseEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview3-35497")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TyreKlicker.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AcceptedByUserId");

                    b.Property<bool>("Complete");

                    b.Property<Guid>("CreatedBy");

                    b.Property<Guid>("CreatedByUserId");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<Guid>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Registration");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedByUserId");

                    b.HasIndex("CreatedByUserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("TyreKlicker.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TyreKlicker.Domain.Entities.Order", b =>
                {
                    b.HasOne("TyreKlicker.Domain.Entities.User", "AcceptedByUser")
                        .WithMany("OrdersAccepted")
                        .HasForeignKey("AcceptedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("TyreKlicker.Domain.Entities.User", "CreatedByUser")
                        .WithMany("OrdersCreated")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
