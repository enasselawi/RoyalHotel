using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoyalHotel.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abouthistory> Abouthistories { get; set; }

    public virtual DbSet<Blogpost> Blogposts { get; set; }

    public virtual DbSet<Contactformsubmission> Contactformsubmissions { get; set; }

    public virtual DbSet<Contactinfo> Contactinfos { get; set; }

    public virtual DbSet<Footer> Footers { get; set; }

    public virtual DbSet<Gallery> Galleries { get; set; }

    public virtual DbSet<Homenavc> Homenavcs { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Hoteltitle> Hoteltitles { get; set; }

    public virtual DbSet<Logo> Logos { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Reservationdetail> Reservationdetails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    public virtual DbSet<Userlogin> Userlogins { get; set; }
    public virtual DbSet<Testimonials> Testimonials { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SID=xe)));User Id=C##ENAS;Password=2001;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##ENAS")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Abouthistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008436");

            entity.ToTable("ABOUTHISTORY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Blogpost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008438");

            entity.ToTable("BLOGPOSTS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.PostDate)
                .HasColumnType("DATE")
                .HasColumnName("POST_DATE");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Contactformsubmission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008444");

            entity.ToTable("CONTACTFORMSUBMISSIONS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Message)
                .HasColumnType("CLOB")
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("SYSDATE  -- Stores the date and time when the form was submitted\n")
                .HasColumnType("DATE")
                .HasColumnName("SUBMISSION_DATE");
        });

        modelBuilder.Entity<Contactinfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008442");

            entity.ToTable("CONTACTINFO");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.OfficeHours)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OFFICE_HOURS");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PHONE");
        });

        modelBuilder.Entity<Footer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008446");

            entity.ToTable("FOOTER");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");
            entity.Property(e => e.SectionName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SECTION_NAME");
        });

        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008440");

            entity.ToTable("GALLERY");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Homenavc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008430");

            entity.ToTable("HOMENAVC");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.BackgroundImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("BACKGROUND_IMAGE");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Heading1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HEADING1");
            entity.Property(e => e.Heading2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("HEADING2");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("SYS_C008461");

            entity.ToTable("HOTELS");

            entity.Property(e => e.HotelId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTEL_ID");
            entity.Property(e => e.Contact)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CONTACT");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Hoteltitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008434");

            entity.ToTable("HOTELTITLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<Logo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008432");

            entity.ToTable("LOGO");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.LogoName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("LOGO_NAME");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("SYS_C008468");

            entity.ToTable("RESERVATIONS");

            entity.Property(e => e.ReservationId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESERVATION_ID");
            entity.Property(e => e.HotelId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTEL_ID");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PAYMENT_STATUS");
            entity.Property(e => e.ReservationDate)
                .HasDefaultValueSql("SYSDATE\n")
                .HasColumnType("DATE")
                .HasColumnName("RESERVATION_DATE");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("TOTAL_PRICE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("SYS_C008470");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008469");
        });

        modelBuilder.Entity<Reservationdetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("SYS_C008473");

            entity.ToTable("RESERVATIONDETAILS");

            entity.Property(e => e.DetailId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("DETAIL_ID");
            entity.Property(e => e.CheckInDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_IN_DATE");
            entity.Property(e => e.CheckOutDate)
                .HasColumnType("DATE")
                .HasColumnName("CHECK_OUT_DATE");
            entity.Property(e => e.ReservationId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RESERVATION_ID");
            entity.Property(e => e.RoomId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.RoomPrice)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("ROOM_PRICE");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Reservationdetails)
                .HasForeignKey(d => d.ReservationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008474");

            entity.HasOne(d => d.Room).WithMany(p => p.Reservationdetails)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("SYS_C008475");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008450");

            entity.ToTable("ROLES");

            entity.HasIndex(e => e.RoleName, "SYS_C008451").IsUnique();

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("SYS_C008464");

            entity.ToTable("ROOMS");

            entity.Property(e => e.RoomId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROOM_ID");
            entity.Property(e => e.Booked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("'N'")
                .IsFixedLength()
                .HasColumnName("BOOKED");
            entity.Property(e => e.Capacity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAPACITY");
            entity.Property(e => e.HotelId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("HOTEL_ID");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.RoomType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROOM_TYPE");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("SYS_C008465");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008448");

            entity.ToTable("SERVICES");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasColumnType("CLOB")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008453");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "SYS_C008454").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("SYS_C008455");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("SYS_C008477");

            entity.ToTable("USERACCOUNTS");

            entity.Property(e => e.AccountId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ACCOUNT_ID");
            entity.Property(e => e.Balance)
                .HasDefaultValueSql("0.00\n")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("BALANCE");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.CardNumber)
        .HasColumnType("VARCHAR2(16)")
        .HasColumnName("CARD_NUMBER");

            entity.Property(e => e.ExpiryDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRY_DATE");

            entity.Property(e => e.CVV)
                .HasColumnType("VARCHAR2(4)")
                .HasColumnName("CVV");


            entity.HasOne(d => d.User).WithMany(p => p.Useraccounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008478");
        });

        modelBuilder.Entity<Userlogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("SYS_C008457");

            entity.ToTable("USERLOGIN");

            entity.HasIndex(e => e.Username, "SYS_C008458").IsUnique();

            entity.Property(e => e.LoginId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.User).WithMany(p => p.Userlogins)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C008459");
        });




        // Configure the Testimonial entity
        modelBuilder.Entity<Testimonials>(entity =>
        {
            entity.HasKey(e => e.TestimonialId)
                  .HasName("SYS_C009000");

            entity.ToTable("TESTIMONIALS");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");

            entity.Property(e => e.UserId)
                  .HasColumnType("NUMBER(38)")
                  .HasColumnName("USER_ID");

            entity.Property(e => e.Content)
                .HasColumnType("CLOB")
                .HasColumnName("CONTENT");

            entity.Property(e => e.IsApproved)
                .HasColumnType("CHAR(1)")
                .HasDefaultValue("N")
                .HasColumnName("IS_APPROVED");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("SYSTIMESTAMP")
                .HasColumnName("CREATED_AT");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("SYS_C009001");

            entity.HasCheckConstraint("chk_is_approved", "IS_APPROVED IN ('Y', 'N')");
        });







        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
