using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DomainModel.Domain
{
    public partial class ScheduleKSTUContext : DbContext
    {
        public virtual DbSet<Auditorium> Auditorium { get; set; }
        public virtual DbSet<AuditoriumSubjectTypes> AuditoriumSubjectTypes { get; set; }
        public virtual DbSet<AuditoriumType> AuditoriumType { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseGroup> CourseGroup { get; set; }
        public virtual DbSet<Criteria> Criteria { get; set; }
        public virtual DbSet<DayOfWeek> DayOfWeek { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<GenSubjectClass> GenSubjectClass { get; set; }
        public virtual DbSet<GenTeachers> GenTeachers { get; set; }
        public virtual DbSet<GenTimeslots> GenTimeslots { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Hour> Hour { get; set; }
        public virtual DbSet<Raschasovka> Raschasovka { get; set; }
        public virtual DbSet<RaschasovkaWeeks> RaschasovkaWeeks { get; set; }
        public virtual DbSet<RaschasovkaYears> RaschasovkaYears { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<ScheduleRealization> ScheduleRealization { get; set; }
        public virtual DbSet<ScheduleWeeks> ScheduleWeeks { get; set; }
        public virtual DbSet<ScheduleYears> ScheduleYears { get; set; }
        public virtual DbSet<Semesters> Semesters { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectClass> SubjectClass { get; set; }
        public virtual DbSet<SubjectDepartment> SubjectDepartment { get; set; }
        public virtual DbSet<SubjectType> SubjectType { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherDepartment> TeacherDepartment { get; set; }
        public virtual DbSet<TeacherPersonalTime> TeacherPersonalTime { get; set; }
        public virtual DbSet<Week> Week { get; set; }
        public virtual DbSet<Years> Years { get; set; }
        public virtual DbSet<TableWeight> TableWeight { get; set; }
        public virtual DbSet<TimeslotsWeight> TimeslotsWeight { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AIP5R7G; Initial Catalog=ScheduleKSTU; Integrated Security=true; MultipleActiveResultSets=true; User ID=sa; Password=aezakmi");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.AuditoriumType)
                    .WithMany(p => p.Auditorium)
                    .HasForeignKey(d => d.AuditoriumTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auditorium_AuditoriumType");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Auditorium)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_Auditorium_Building");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Auditorium)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Auditorium_Department");
            });

            modelBuilder.Entity<AuditoriumSubjectTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.AuditoriumType)
                    .WithMany(p => p.AuditoriumSubjectTypes)
                    .HasForeignKey(d => d.AuditoriumTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditoriumSubjectTypes_AuditoriumType");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.AuditoriumSubjectTypes)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AuditoriumSubjectTypes_SubjectType");
            });

            modelBuilder.Entity<AuditoriumType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<CourseGroup>(entity =>
            {
                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseGroup)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseGroup_Course");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.CourseGroup)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseGroup_Group");
            });

            modelBuilder.Entity<Criteria>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Rate).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<DayOfWeek>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Department)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_Department_Faculty");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<GenSubjectClass>(entity =>
            {
                entity.ToTable("Gen_SubjectClass");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<GenTeachers>(entity =>
            {
                entity.ToTable("Gen_Teachers");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.GenTeachers)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gen_Teachers_Teacher");
            });

            modelBuilder.Entity<GenTimeslots>(entity =>
            {
                entity.ToTable("Gen_Timeslots");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Group)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_Department");
            });

            modelBuilder.Entity<Hour>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Begin).HasColumnType("time(0)");
            });

            modelBuilder.Entity<Raschasovka>(entity =>
            {
                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.AuditoriumId)
                    .HasConstraintName("FK_Raschasovka_Auditorium");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Course");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Department");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Group");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Semesters");

                entity.HasOne(d => d.SubjectClass)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.SubjectClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_SubjectClass");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Subject");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_SubjectType");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Raschasovka)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Raschasovka_Teacher");
            });

            modelBuilder.Entity<RaschasovkaWeeks>(entity =>
            {
                entity.HasOne(d => d.Raschasovka)
                    .WithMany(p => p.RaschasovkaWeeks)
                    .HasForeignKey(d => d.RaschasovkaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaWeeks_Raschasovka");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.RaschasovkaWeeks)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaWeeks_Week");
            });

            modelBuilder.Entity<RaschasovkaYears>(entity =>
            {
                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.AuditoriumId)
                    .HasConstraintName("FK_RaschasovkaYears_Auditorium");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Course");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Department");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Group");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Semesters");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Subject");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_SubjectType");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Teacher");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.RaschasovkaYears)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RaschasovkaYears_Year");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.LastChange).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.AuditoriumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Auditorium");

                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_DayOfWeek");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Group");

                entity.HasOne(d => d.Hour)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.HourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Hour");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_SemesterId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Subject");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_SubjectType");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Teacher");
            });

            modelBuilder.Entity<ScheduleRealization>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActualDate).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("nchar(256)");

                entity.HasOne(d => d.ActualAuditorium)
                    .WithMany(p => p.ScheduleRealization)
                    .HasForeignKey(d => d.ActualAuditoriumId)
                    .HasConstraintName("FK_ScheduleRealization_Auditorium");

                entity.HasOne(d => d.ActualTeacher)
                    .WithMany(p => p.ScheduleRealization)
                    .HasForeignKey(d => d.ActualTeacherId)
                    .HasConstraintName("FK_ScheduleRealization_Teacher");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleRealization)
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("FK_ScheduleRealization_Schedule");
            });

            modelBuilder.Entity<ScheduleWeeks>(entity =>
            {
                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.ScheduleWeeks)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleWeeks_Schedule");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.ScheduleWeeks)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SchdeuleWeeks_Week");
            });

            modelBuilder.Entity<ScheduleYears>(entity =>
            {
                entity.Property(e => e.LastChange).HasColumnType("smalldatetime");

                entity.HasOne(d => d.Auditorium)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.AuditoriumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Auditorium");

                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_DayOfWeek");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Group");

                entity.HasOne(d => d.Hour)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.HourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Hour");

                entity.HasOne(d => d.Semester)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.SemesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_SemesterId");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Subject");

                entity.HasOne(d => d.SubjectType)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.SubjectTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_SubjectType");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Teacher");

                entity.HasOne(d => d.Week)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.WeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Week");

                entity.HasOne(d => d.Year)
                    .WithMany(p => p.ScheduleYears)
                    .HasForeignKey(d => d.YearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ScheduleYears_Year");
            });

            modelBuilder.Entity<Semesters>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(15);
            });

            modelBuilder.Entity<SubjectClass>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SubjectDepartment>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubjectDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_SubjectDepartment_Department");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.SubjectDepartment)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectDepartment_Subject");
            });

            modelBuilder.Entity<SubjectType>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FullName).HasColumnType("nchar(40)");

                entity.Property(e => e.Name).HasMaxLength(10);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(40);

                entity.Property(e => e.LastName).HasMaxLength(40);
            });

            modelBuilder.Entity<TeacherDepartment>(entity =>
            {
                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TeacherDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherDepartment_Department");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherDepartment)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherDepartment_Teacher");
            });

            modelBuilder.Entity<TeacherPersonalTime>(entity =>
            {
                entity.HasOne(d => d.DayOfWeek)
                    .WithMany(p => p.TeacherPersonalTime)
                    .HasForeignKey(d => d.DayOfWeekId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPersonalTime_DayOfWeek");

                entity.HasOne(d => d.Hour)
                    .WithMany(p => p.TeacherPersonalTime)
                    .HasForeignKey(d => d.HourId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPersonalTime_Hour");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherPersonalTime)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherPersonalTime_Teacher");
            });

            modelBuilder.Entity<Week>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Years>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10);
            });
        }
    }
}
