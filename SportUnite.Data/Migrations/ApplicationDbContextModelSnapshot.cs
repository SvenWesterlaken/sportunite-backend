using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SportUnite.Data;

namespace SportUnite.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SportUnite.Domain.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BuildingId");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int>("HouseNumber");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("StreetName")
                        .IsRequired();

                    b.Property<string>("Suffix");

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("AddressId");

                    b.HasIndex("BuildingId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SportUnite.Domain.Building", b =>
                {
                    b.Property<int>("BuildingId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("BuildingId");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("SportUnite.Domain.Hall", b =>
                {
                    b.Property<int>("HallId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Available");

                    b.Property<int>("BuildingId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<string>("Size")
                        .IsRequired();

                    b.HasKey("HallId");

                    b.HasIndex("BuildingId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("SportUnite.Domain.HallOpeningHours", b =>
                {
                    b.Property<int>("HallId");

                    b.Property<int>("OpeningHoursId");

                    b.HasKey("HallId", "OpeningHoursId");

                    b.HasIndex("OpeningHoursId");

                    b.ToTable("HallOpeningHours");
                });

            modelBuilder.Entity("SportUnite.Domain.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTime");

                    b.Property<double>("Price");

                    b.Property<int>("ReservationId");

                    b.HasKey("InvoiceId");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SportUnite.Domain.Log", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action");

                    b.Property<DateTime>("Date");

                    b.Property<string>("User");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SportUnite.Domain.OpeningHours", b =>
                {
                    b.Property<int>("OpeningHoursId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClosingTime")
                        .IsRequired();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Day")
                        .IsRequired();

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("OpeningTime")
                        .IsRequired();

                    b.HasKey("OpeningHoursId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("SportUnite.Domain.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Definite");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int>("HallId");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<int>("SportEventId");

                    b.Property<DateTime>("StartTime");

                    b.Property<DateTime>("TimeFinish");

                    b.HasKey("ReservationId");

                    b.HasIndex("HallId");

                    b.HasIndex("SportEventId")
                        .IsUnique();

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("SportUnite.Domain.Sport", b =>
                {
                    b.Property<int>("SportId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("SportId");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("SportUnite.Domain.SportEvent", b =>
                {
                    b.Property<int>("SportEventId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(224);

                    b.Property<DateTime>("EventEndTime");

                    b.Property<DateTime>("EventStartTime");

                    b.Property<int>("MaxAttendees");

                    b.Property<int>("MinAttendees");

                    b.Property<DateTime>("ModifiedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(224);

                    b.Property<int>("SportId");

                    b.HasKey("SportEventId");

                    b.HasIndex("SportId");

                    b.ToTable("SportEvents");
                });

            modelBuilder.Entity("SportUnite.Domain.SportHall", b =>
                {
                    b.Property<int>("HallId");

                    b.Property<int>("SportId");

                    b.HasKey("HallId", "SportId");

                    b.HasIndex("SportId");

                    b.ToTable("SportHalls");
                });

            modelBuilder.Entity("SportUnite.Domain.SportObject", b =>
                {
                    b.Property<int>("SportObjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("SportObjectId");

                    b.ToTable("SportObjects");
                });

            modelBuilder.Entity("SportUnite.Domain.SportObjectHall", b =>
                {
                    b.Property<int>("SportObjectId");

                    b.Property<int>("HallId");

                    b.HasKey("SportObjectId", "HallId");

                    b.HasIndex("HallId");

                    b.ToTable("SportObjectHalls");
                });

            modelBuilder.Entity("SportUnite.Domain.SportObjectSport", b =>
                {
                    b.Property<int>("SportId");

                    b.Property<int>("SportObjectId");

                    b.HasKey("SportId", "SportObjectId");

                    b.HasIndex("SportObjectId");

                    b.ToTable("SportObjectSports");
                });

            modelBuilder.Entity("SportUnite.Domain.Address", b =>
                {
                    b.HasOne("SportUnite.Domain.Building", "Building")
                        .WithOne("Address")
                        .HasForeignKey("SportUnite.Domain.Address", "BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.Hall", b =>
                {
                    b.HasOne("SportUnite.Domain.Building", "Building")
                        .WithMany("Halls")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.HallOpeningHours", b =>
                {
                    b.HasOne("SportUnite.Domain.Hall", "Hall")
                        .WithMany("HallOpeningHours")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.OpeningHours", "OpeningHours")
                        .WithMany("HallOpeningHours")
                        .HasForeignKey("OpeningHoursId");
                });

            modelBuilder.Entity("SportUnite.Domain.Invoice", b =>
                {
                    b.HasOne("SportUnite.Domain.Reservation", "Reservation")
                        .WithOne("Invoice")
                        .HasForeignKey("SportUnite.Domain.Invoice", "ReservationId");
                });

            modelBuilder.Entity("SportUnite.Domain.Reservation", b =>
                {
                    b.HasOne("SportUnite.Domain.Hall", "Hall")
                        .WithMany("Reservations")
                        .HasForeignKey("HallId");

                    b.HasOne("SportUnite.Domain.SportEvent", "SportEvent")
                        .WithOne("Reservation")
                        .HasForeignKey("SportUnite.Domain.Reservation", "SportEventId");
                });

            modelBuilder.Entity("SportUnite.Domain.SportEvent", b =>
                {
                    b.HasOne("SportUnite.Domain.Sport", "Sport")
                        .WithMany("SportEvents")
                        .HasForeignKey("SportId");
                });

            modelBuilder.Entity("SportUnite.Domain.SportHall", b =>
                {
                    b.HasOne("SportUnite.Domain.Hall", "Hall")
                        .WithMany("SportHalls")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.Sport", "Sport")
                        .WithMany("SportHalls")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.SportObjectHall", b =>
                {
                    b.HasOne("SportUnite.Domain.Hall", "Hall")
                        .WithMany("SportObjectHalls")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.SportObject", "SportObject")
                        .WithMany("SportObjectHalls")
                        .HasForeignKey("SportObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SportUnite.Domain.SportObjectSport", b =>
                {
                    b.HasOne("SportUnite.Domain.Sport", "Sport")
                        .WithMany("SportObjectSports")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SportUnite.Domain.SportObject", "SportObject")
                        .WithMany("SportObjectSports")
                        .HasForeignKey("SportObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
