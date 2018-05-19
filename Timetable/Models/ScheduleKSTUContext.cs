using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataModel.Models
{
    public partial class ScheduleKSTUContext : DbContext
    {
        public virtual DbSet<Auditorium> Auditorium { get; set; }
        public virtual DbSet<AuditoriumSubjectTypes> AuditoriumSubjectTypes { get; set; }
        public virtual DbSet<AuditoriumType> AuditoriumType { get; set; }
        public virtual DbSet<AuthGroup> AuthGroup { get; set; }
        public virtual DbSet<AuthGroupPermissions> AuthGroupPermissions { get; set; }
        public virtual DbSet<AuthPermission> AuthPermission { get; set; }
        public virtual DbSet<AuthUser> AuthUser { get; set; }
        public virtual DbSet<AuthUserGroups> AuthUserGroups { get; set; }
        public virtual DbSet<AuthUserUserPermissions> AuthUserUserPermissions { get; set; }
        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<CourseGroup> CourseGroup { get; set; }
        public virtual DbSet<Criteria> Criteria { get; set; }
        public virtual DbSet<DayOfWeek> DayOfWeek { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DjangoAdminLog> DjangoAdminLog { get; set; }
        public virtual DbSet<DjangoContentType> DjangoContentType { get; set; }
        public virtual DbSet<DjangoMigrations> DjangoMigrations { get; set; }
        public virtual DbSet<DjangoSession> DjangoSession { get; set; }
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

            modelBuilder.Entity<AuthGroup>(entity =>
            {
                entity.ToTable("auth_group");

                entity.HasIndex(e => e.Name)
                    .HasName("UQ__auth_gro__72E12F1B543748EF")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<AuthGroupPermissions>(entity =>
            {
                entity.ToTable("auth_group_permissions");

                entity.HasIndex(e => e.GroupId)
                    .HasName("auth_group_permissions_group_id_b120cbf9");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("auth_group_permissions_permission_id_84c5c92e");

                entity.HasIndex(e => new { e.GroupId, e.PermissionId })
                    .HasName("auth_group_permissions_group_id_permission_id_0cd325b0_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_group_id_b120cbf9_fk_auth_group_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthGroupPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_group_permissions_permission_id_84c5c92e_fk_auth_permission_id");
            });

            modelBuilder.Entity<AuthPermission>(entity =>
            {
                entity.ToTable("auth_permission");

                entity.HasIndex(e => e.ContentTypeId)
                    .HasName("auth_permission_content_type_id_2f476e4b");

                entity.HasIndex(e => new { e.ContentTypeId, e.Codename })
                    .HasName("auth_permission_content_type_id_codename_01ab375a_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codename)
                    .IsRequired()
                    .HasColumnName("codename")
                    .HasMaxLength(100);

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.AuthPermission)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_permission_content_type_id_2f476e4b_fk_django_content_type_id");
            });

            modelBuilder.Entity<AuthUser>(entity =>
            {
                entity.ToTable("auth_user");

                entity.HasIndex(e => e.Username)
                    .HasName("auth_user_username_6821ab7c_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateJoined)
                    .HasColumnName("date_joined")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(254);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(30);

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.IsStaff).HasColumnName("is_staff");

                entity.Property(e => e.IsSuperuser).HasColumnName("is_superuser");

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(128);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<AuthUserGroups>(entity =>
            {
                entity.ToTable("auth_user_groups");

                entity.HasIndex(e => e.GroupId)
                    .HasName("auth_user_groups_group_id_97559544");

                entity.HasIndex(e => e.UserId)
                    .HasName("auth_user_groups_user_id_6a12ed8b");

                entity.HasIndex(e => new { e.UserId, e.GroupId })
                    .HasName("auth_user_groups_user_id_group_id_94350c0c_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_group_id_97559544_fk_auth_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_groups_user_id_6a12ed8b_fk_auth_user_id");
            });

            modelBuilder.Entity<AuthUserUserPermissions>(entity =>
            {
                entity.ToTable("auth_user_user_permissions");

                entity.HasIndex(e => e.PermissionId)
                    .HasName("auth_user_user_permissions_permission_id_1fbb5f2c");

                entity.HasIndex(e => e.UserId)
                    .HasName("auth_user_user_permissions_user_id_a95ead1b");

                entity.HasIndex(e => new { e.UserId, e.PermissionId })
                    .HasName("auth_user_user_permissions_user_id_permission_id_14a6b632_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permissions_permission_id_1fbb5f2c_fk_auth_permission_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserUserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("auth_user_user_permissions_user_id_a95ead1b_fk_auth_user_id");
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

            modelBuilder.Entity<DjangoAdminLog>(entity =>
            {
                entity.ToTable("django_admin_log");

                entity.HasIndex(e => e.ContentTypeId)
                    .HasName("django_admin_log_content_type_id_c4bce8eb");

                entity.HasIndex(e => e.UserId)
                    .HasName("django_admin_log_user_id_c564eba6");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionFlag).HasColumnName("action_flag");

                entity.Property(e => e.ActionTime)
                    .HasColumnName("action_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChangeMessage)
                    .IsRequired()
                    .HasColumnName("change_message");

                entity.Property(e => e.ContentTypeId).HasColumnName("content_type_id");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.ObjectRepr)
                    .IsRequired()
                    .HasColumnName("object_repr")
                    .HasMaxLength(200);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.DjangoAdminLog)
                    .HasForeignKey(d => d.ContentTypeId)
                    .HasConstraintName("django_admin_log_content_type_id_c4bce8eb_fk_django_content_type_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.DjangoAdminLog)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("django_admin_log_user_id_c564eba6_fk");
            });

            modelBuilder.Entity<DjangoContentType>(entity =>
            {
                entity.ToTable("django_content_type");

                entity.HasIndex(e => new { e.AppLabel, e.Model })
                    .HasName("django_content_type_app_label_model_76bd3d3b_uniq")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppLabel)
                    .IsRequired()
                    .HasColumnName("app_label")
                    .HasMaxLength(100);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasColumnName("model")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DjangoMigrations>(entity =>
            {
                entity.ToTable("django_migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.App)
                    .IsRequired()
                    .HasColumnName("app")
                    .HasMaxLength(255);

                entity.Property(e => e.Applied)
                    .HasColumnName("applied")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DjangoSession>(entity =>
            {
                entity.HasKey(e => e.SessionKey);

                entity.ToTable("django_session");

                entity.HasIndex(e => e.ExpireDate)
                    .HasName("django_session_expire_date_a5c62663");

                entity.Property(e => e.SessionKey)
                    .HasColumnName("session_key")
                    .HasMaxLength(40)
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expire_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.SessionData)
                    .IsRequired()
                    .HasColumnName("session_data");
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
