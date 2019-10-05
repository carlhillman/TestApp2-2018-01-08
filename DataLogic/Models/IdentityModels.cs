using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestApp2.Repositories;
using System.Collections.Generic;
using System;
namespace TestApp2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser:IdentityUser, IEntity<string>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public int Alder  { get; set; }
        //enum för sex appeal 
        public Kön Kön { get; set; }
        public string Stad { get; set; }
        //bilden 
        public byte[] Bild { get; set; }
        //typen på filen av bild
        public  string  ContentType { get; set; }
        //enum för synlighet      
        public Synlighet Synlighet { get; set;}
        public virtual ICollection<FriendRequest> FriendRequest { get; set; }
        public virtual ICollection<Friends>Friends { get; set; }
        public virtual ICollection<Post>Post { get; set;  }
   
    }

    //sambandstabell
    public class FriendRequest : IEntity<int>
    {
       public int Id { get; set; }
       public virtual ApplicationUser From { get; set; }
       public virtual ApplicationUser To { get; set; }

    }
    public class Friends : IEntity<int>
    {
        public int Id { get; set;}
        public virtual ApplicationUser Friend1 { get; set; }
        public virtual ApplicationUser Friend2 { get; set; }
    }



    //model tabell 
    public class Post : IEntity<int>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public virtual ApplicationUser From { get; set; }
        public virtual ApplicationUser To { get; set; }

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(): base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //johans tillagda kod
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.FriendRequest).WithRequired(x => x.To);
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Friends).WithRequired(x => x.Friend1);
            modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Post).WithRequired(x => x.To);

            base.OnModelCreating(modelBuilder);

            //ska ta bort onödig skit data från aspusers/applicationUser
            modelBuilder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.LockoutEnabled)
                                               .Ignore(c => c.LockoutEndDateUtc)                                              
                                               .Ignore(c => c.TwoFactorEnabled)
                                               .Ignore(c=>c.PhoneNumber)
                                               .Ignore(c=>c.PhoneNumberConfirmed);
          //ska ta bort onödig skit data från aspnetusers/applicationUser
        }

    }
  //enum 
    public enum Kön {Man , Kvinna }
    public enum Synlighet { Synlig, Gömd}

}