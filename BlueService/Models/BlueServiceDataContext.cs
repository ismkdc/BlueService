﻿using BlueService.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlueService.Models
{
    public class BlueServiceDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserFavoriteProduct> UserFavoriteProducts { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public BlueServiceDataContext()
        {
            Database.SetInitializer(new Configuration());
        }
    }
}