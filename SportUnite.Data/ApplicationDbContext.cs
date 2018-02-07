using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SportUnite.Domain;

namespace SportUnite.Data
{
	public class ApplicationDbContext : DbContext
	{
		
		public virtual DbSet<SportEvent> SportEvents { get; set; }
		public virtual DbSet<Reservation> Reservations { get; set; }
		public virtual DbSet<Hall> Halls { get; set; }
		public virtual DbSet<Building> Buildings { get; set; }
		public virtual DbSet<Invoice> Invoices { get; set; }
		public virtual DbSet<Log> Logs { get; set; }
		public virtual DbSet<OpeningHours> OpeningHours { get; set; }
		public virtual DbSet<HallOpeningHours> HallOpeningHours { get; set; }
		public virtual DbSet<Sport> Sports { get; set; }
		public virtual DbSet<SportHall> SportHalls { get; set; }
		public virtual DbSet<SportObject> SportObjects { get; set; }
		public virtual DbSet<SportObjectHall> SportObjectHalls { get; set; }
		public virtual DbSet<SportObjectSport> SportObjectSports { get; set; }
		public virtual DbSet<Address> Addresses { get; set; }

   

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// making sure a Sport cannot be deleted when there are still SportEvents with that Sport
			modelBuilder.Entity<SportEvent>()
				.HasOne(se => se.Sport)
				.WithMany(s => s.SportEvents)
				.OnDelete(DeleteBehavior.Restrict);

			// making sure a SportEvent cannot be deleted when there is still a Reservation connected to that SportEvent
			modelBuilder.Entity<Reservation>()
				.HasOne(r => r.SportEvent)
				.WithOne(se => se.Reservation)
				.OnDelete(DeleteBehavior.Restrict);

			// making sure a Hall cannot be deleted when there are still Reservations connected to that Hall
			modelBuilder.Entity<Reservation>()
				.HasOne(r => r.Hall)
				.WithMany(se => se.Reservations)
				.OnDelete(DeleteBehavior.Restrict);

			// making sure a Reservation cannot be deleted when there is still a Invoice connected to that Reservation
			modelBuilder.Entity<Invoice>()
				.HasOne(r => r.Reservation)
				.WithOne(se => se.Invoice)
				.OnDelete(DeleteBehavior.Restrict);

			// SportObjectSport
			modelBuilder.Entity<SportObjectSport>()
				.HasKey(sps => new { sps.SportId, sps.SportObjectId });

			modelBuilder.Entity<SportObjectSport>()
				.HasOne(sps => sps.SportObject)
				.WithMany(so => so.SportObjectSports)
				.HasForeignKey(so => so.SportObjectId);

			modelBuilder.Entity<SportObjectSport>()
				.HasOne(sps => sps.Sport)
				.WithMany(s => s.SportObjectSports)
				.HasForeignKey(bc => bc.SportId);

			// SportObjectHall
			modelBuilder.Entity<SportObjectHall>()
				.HasKey(soh => new { soh.SportObjectId, soh.HallId });

			modelBuilder.Entity<SportObjectHall>()
				.HasOne(soh => soh.SportObject)
				.WithMany(so => so.SportObjectHalls)
				.HasForeignKey(soh => soh.SportObjectId);

			modelBuilder.Entity<SportObjectHall>()
				.HasOne(soh => soh.Hall)
				.WithMany(h => h.SportObjectHalls)
				.HasForeignKey(soh => soh.HallId);

            // Building
		    modelBuilder.Entity<Building>()
		        .HasOne(b => b.Address)
		        .WithOne(a => a.Building)
		        .HasForeignKey<Address>(b => b.BuildingId)
		        .OnDelete(DeleteBehavior.Cascade);

		    modelBuilder.Entity<Building>()
		        .HasMany(h => h.Halls)
		        .WithOne(b => b.Building)
		        .OnDelete(DeleteBehavior.Cascade);

            // SportHall
            modelBuilder.Entity<SportHall>()
				.HasKey(sh => new { sh.HallId, sh.SportId });

			modelBuilder.Entity<SportHall>()
				.HasOne(sh => sh.Sport)
				.WithMany(s => s.SportHalls)
				.HasForeignKey(sh => sh.SportId);

			modelBuilder.Entity<SportHall>()
				.HasOne(sh => sh.Hall)
				.WithMany(h => h.SportHalls)
				.HasForeignKey(sh => sh.HallId);

			// HallOpeningsHours
			modelBuilder.Entity<HallOpeningHours>()
				.HasKey(hoh => new { hoh.HallId, hoh.OpeningHoursId });

			modelBuilder.Entity<HallOpeningHours>()
				.HasOne(hoh => hoh.OpeningHours)
				.WithMany(oh => oh.HallOpeningHours)
				.HasForeignKey(hoh => hoh.OpeningHoursId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<HallOpeningHours>()
				.HasOne(hoh => hoh.Hall)
				.WithMany(h => h.HallOpeningHours)
				.HasForeignKey(hoh => hoh.HallId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}

	
}
