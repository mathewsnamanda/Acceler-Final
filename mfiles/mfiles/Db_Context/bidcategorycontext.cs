using mfiles.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mfiles.Db_Context
{
    public class bidcategorycontext:DbContext
    {
        public bidcategorycontext(DbContextOptions<bidcategorycontext> options):
            base(options)
        {

        }
        public DbSet<core_category> core_category { get; set; }
        public DbSet<core_bid> core_bid { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<core_bid>().HasData(new core_bid()
            {
                id = 1,
                applicant_id = 2,
                category_id = 1
            },
            new core_bid()
            {
                id = 2,
                applicant_id = 2,
                category_id = 2
            },
             new core_bid()
             {
                 id = 3,
                 applicant_id = 3,
                 category_id = 1
             },
              new core_bid()
              {
                  id = 4,
                  applicant_id = 3,
                  category_id = 2
              },
               new core_bid()
               {
                   id = 5,
                   applicant_id = 2,
                   category_id = 3
               },
                new core_bid()
                {
                    id = 6,
                    applicant_id = 8,
                    category_id = 1
                },
                 new core_bid()
                 {
                     id = 7,
                     applicant_id = 8,
                     category_id = 2
                 },
                  new core_bid()
                  {
                      id = 8,
                      applicant_id = 8,
                      category_id = 3
                  });

            modelBuilder.Entity<core_category>().HasData(new core_category()
            {
                id=1,
                name="supply of boots"
            },
            new core_category()
            {
                id=2,
                name="repair of forklifts"
            },
            new core_category()
            {
                id=3,
                name="containers"
            }


            );
        }
    }
}
