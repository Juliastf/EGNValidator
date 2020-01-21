using EGNValidator.Data.Configuration;
using EGNValidator.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EGNValidator.Data
{
    public class EGNValidatorContext : DbContext
    {

        public DbSet<Request> Requests { get; set; }

        public EGNValidatorContext(DbContextOptions<EGNValidatorContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RequestConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
