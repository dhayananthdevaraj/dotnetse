using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

namespace dotnetapp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configure relationships for User and Student
    modelBuilder.Entity<User>()
        .HasOne(u => u.Student)
        .WithOne(s => s.User)
        .HasForeignKey<Student>(s => s.UserId)
        .OnDelete(DeleteBehavior.Cascade);

    // Configure relationships for Student and Enquiry
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Enquiries)
        .WithOne(e => e.Student)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Enquiry>()
        .HasOne(e => e.Student)
        .WithMany(s => s.Enquiries)
        .HasForeignKey(e => e.StudentId)
        .OnDelete(DeleteBehavior.Cascade);

    // Configure relationships for Course, Admission, and Payment
    modelBuilder.Entity<Course>()
        .HasMany(c => c.Students)
        .WithMany(s => s.Courses)
        .UsingEntity(j => j.ToTable("StudentCourses"));

    modelBuilder.Entity<Course>()
        .HasMany(c => c.Enquiries)
        .WithOne(e => e.Course)
        .HasForeignKey(e => e.CourseID)
        .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Course>()
        .HasMany(c => c.Admissions)
        .WithOne(a => a.Course)
        .HasForeignKey(a => a.CourseID)
        .OnDelete(DeleteBehavior.Cascade);

    // modelBuilder.Entity<Course>()
    //     .HasMany(c => c.Payments)
    //     .WithOne(p => p.Course)
    //     .HasForeignKey(p => p.CourseID)
    //     .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Admission>()
        .HasMany(a => a.Payments)
        .WithOne(p => p.Admission)
        .HasForeignKey(p => p.AdmissionID)
        .OnDelete(DeleteBehavior.Cascade);

        // Modify the foreign key constraint for CourseID in the Payments table
        // modelBuilder.Entity<Payment>()
        //     .HasOne(p => p.Course)
        //     .WithMany(c => c.Payments)
        //     .HasForeignKey(p => p.CourseID)
        //     .OnDelete(DeleteBehavior.NoAction);

    // Modify the foreign key constraint for AdmissionID in the Payments table
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Admission)
            .WithMany(a => a.Payments)
            .HasForeignKey(p => p.AdmissionID)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Admission>()
            .HasIndex(a => new { a.StudentId, a.CourseID })
            .IsUnique();


    base.OnModelCreating(modelBuilder);

    
}

    }
}
