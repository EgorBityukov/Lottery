//using Lottery.ADO;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace Lottery.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Address> Addresses { get; set; }
//        public DbSet<Draw> Draws { get; set; }
//        public DbSet<Lot> Lots { get; set; }
//        public DbSet<Photo> Photos { get; set; }
//        public DbSet<Ticket> Tickets { get; set; }
//        public DbSet<UserInfo> UserInfos { get; set; }

//    }
//}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lottery.Models;

namespace Lottery.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Draw> Draws { get; set; }
        public virtual DbSet<Lot> Lots { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-Lottery-6A0E8D9A-7ED4-4FD6-A49D-030BABBADA3D;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
//            }
//        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Address>(entity =>
        //    {
        //        entity.ToTable("Address");

        //        entity.Property(e => e.AddressId).ValueGeneratedNever();

        //        entity.Property(e => e.Building)
        //            .HasMaxLength(50)
        //            .IsFixedLength();

        //        entity.Property(e => e.City).HasMaxLength(100);

        //        entity.Property(e => e.Country).HasMaxLength(100);

        //        entity.Property(e => e.PostalCode).HasMaxLength(50);

        //        entity.Property(e => e.Street).HasMaxLength(100);
        //    });

            //modelBuilder.Entity<AspNetRole>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetRoleClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetUser>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);

            //    entity.HasMany(d => d.Roles)
            //        .WithMany(p => p.Users)
            //        .UsingEntity<Dictionary<string, object>>(
            //            "AspNetUserRole",
            //            l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
            //            r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
            //            j =>
            //            {
            //                j.HasKey("UserId", "RoleId");

            //                j.ToTable("AspNetUserRoles");

            //                j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
            //            });
            //});

            //modelBuilder.Entity<AspNetUserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogin>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserToken>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.Name).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

        //    modelBuilder.Entity<Draw>(entity =>
        //    {
        //        entity.ToTable("Draw");

        //        entity.Property(e => e.DrawId).ValueGeneratedNever();

        //        entity.Property(e => e.Date).HasColumnType("date");

        //        entity.Property(e => e.WinnerId).HasMaxLength(450);

        //        entity.HasOne(d => d.Lot)
        //            .WithMany(p => p.Draws)
        //            .HasForeignKey(d => d.LotId)
        //            .HasConstraintName("FK_Draw_Lot");

        //        entity.HasOne(d => d.Ticket)
        //            .WithMany(p => p.Draws)
        //            .HasForeignKey(d => d.TicketId)
        //            .HasConstraintName("FK_Draw_Ticket");

        //        entity.HasOne(d => d.Winner)
        //            .WithMany(p => p.Draws)
        //            .HasForeignKey(d => d.WinnerId)
        //            .HasConstraintName("FK_Draw_UserInfo");
        //    });

        //    modelBuilder.Entity<Lot>(entity =>
        //    {
        //        entity.ToTable("Lot");

        //        entity.Property(e => e.LotId).ValueGeneratedNever();

        //        entity.Property(e => e.Name).HasMaxLength(100);

        //        entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

        //        entity.Property(e => e.TicketPrice).HasColumnType("decimal(18, 0)");

        //        entity.HasOne(d => d.Photo)
        //            .WithMany(p => p.Lots)
        //            .HasForeignKey(d => d.PhotoId)
        //            .HasConstraintName("FK_Lot_Photo");
        //    });

        //    modelBuilder.Entity<Photo>(entity =>
        //    {
        //        entity.ToTable("Photo");

        //        entity.Property(e => e.PhotoId).ValueGeneratedNever();

        //        entity.Property(e => e.PhotoPath).HasMaxLength(1000);
        //    });

        //    modelBuilder.Entity<Ticket>(entity =>
        //    {
        //        entity.ToTable("Ticket");

        //        entity.Property(e => e.TicketId).ValueGeneratedNever();

        //        entity.Property(e => e.Date).HasColumnType("date");

        //        entity.Property(e => e.UserId).HasMaxLength(450);

        //        entity.HasOne(d => d.Lot)
        //            .WithMany(p => p.Tickets)
        //            .HasForeignKey(d => d.LotId)
        //            .HasConstraintName("FK_Ticket_Lot");

        //        entity.HasOne(d => d.User)
        //            .WithMany(p => p.Tickets)
        //            .HasForeignKey(d => d.UserId)
        //            .HasConstraintName("FK_Ticket_UserInfo");
        //    });

        //    modelBuilder.Entity<UserInfo>(entity =>
        //    {
        //        entity.ToTable("UserInfo");

        //        entity.Property(e => e.Balance).HasColumnType("decimal(18, 0)");

        //        entity.HasOne(d => d.Address)
        //            .WithMany(p => p.UserInfos)
        //            .HasForeignKey(d => d.AddressId)
        //            .HasConstraintName("FK_UserInfo_Address");

        //        entity.HasOne(d => d.Photo)
        //            .WithMany(p => p.UserInfos)
        //            .HasForeignKey(d => d.PhotoId)
        //            .HasConstraintName("FK_UserInfo_Photo");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
