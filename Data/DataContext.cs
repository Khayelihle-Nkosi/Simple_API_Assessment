using Microsoft.EntityFrameworkCore;
using Simple_API_Assessment.Model;

namespace Simple_API_Assessment.Data;

public class DataContext : DbContext {
	public DbSet<Applicant> Applicants => Set<Applicant>();
	public DbSet<Skill> Skills => Set<Skill>();

	public DataContext(DbContextOptions<DataContext> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema("public");

		modelBuilder.Entity<Applicant>().HasKey(a => a.Id);
		modelBuilder.Entity<Applicant>().Property(a => a.Name)
		            .HasMaxLength(255)
		            .IsRequired();

		modelBuilder.Entity<Applicant>().HasData(
			new Applicant { Id = 1L, Name = "Khayelihle Nkosi" });

		modelBuilder.Entity<Skill>().HasKey(s => s.Id);
		modelBuilder.Entity<Skill>().Property(s => s.Name)
		            .HasMaxLength(255)
		            .IsRequired();
		modelBuilder.Entity<Skill>().Property(s => s.ApplicantId)
		            .IsRequired();

		modelBuilder.Entity<Skill>()
		            .HasOne<Applicant>(s => s.Applicant)
		            .WithMany(a => a.Skills)
		            .HasForeignKey(s => s.ApplicantId)
		            .OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<Skill>().HasData(
			new Skill() { Id = 1, Name = "C# Development", ApplicantId = 1L },
			new Skill() { Id = 2, Name = ".Net Core", ApplicantId = 1L },
			new Skill() { Id = 3, Name = "Entity Framework", ApplicantId = 1L }
		);
	}
}