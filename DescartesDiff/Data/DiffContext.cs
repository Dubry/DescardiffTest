using DescartesDiff.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DescartesDiff.Data
{
    public class DiffContext : DbContext
    {
        public DiffContext(DbContextOptions<DiffContext> options) : base(options) { }

        public DbSet<DataModel> DiffResults { get; set; }
    }
}
