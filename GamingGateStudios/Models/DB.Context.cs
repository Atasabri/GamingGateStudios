﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamingGateStudios.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AD> ADS { get; set; }
        public virtual DbSet<Ads_Clients> Ads_Clients { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Deal_Gamers> Deal_Gamers { get; set; }
        public virtual DbSet<Deal_Member> Deal_Member { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Gamer> Gamers { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<MemberShip> MemberShips { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Portfolio> Portfolios { get; set; }
        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<Splash> Splashes { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Third_Partner> Third_Partner { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Winner> Winners { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<GGBDeal> GGBDeals { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Tags_Videos> Tags_Videos { get; set; }
        public virtual DbSet<Users_GGBDeals> Users_GGBDeals { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
    
        public virtual int Rest_Users()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Rest_Users");
        }
    
        public virtual int GameWatch(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GameWatch", iDParameter);
        }
    
        public virtual int ADClick(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ADClick", iDParameter);
        }
    
        public virtual int ADWatch(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ADWatch", iDParameter);
        }
    
        public virtual int GamerActive(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GamerActive", idParameter);
        }
    
        public virtual int GamerNotActive(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GamerNotActive", idParameter);
        }
    
        public virtual int GamerActive1(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GamerActive1", idParameter);
        }
    
        public virtual int GamerNotActive1(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GamerNotActive1", idParameter);
        }
    }
}