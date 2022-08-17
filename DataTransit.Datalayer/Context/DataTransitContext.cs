using DataTransit.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataTransit.Datalayer.Context
{
    public class DataTransitContext : DbContext
    {
        public DataTransitContext(DbContextOptions<DataTransitContext> options)
            : base(options)
        {
        }
        public DbSet<ExcelModel> ExelModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
    }
}
