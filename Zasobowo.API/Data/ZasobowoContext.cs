using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Zasobowo.API.Models;

namespace Zasobowo.API.Data
{
    public class ZasobowoContext : DbContext
    {
        public ZasobowoContext(DbContextOptions<ZasobowoContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
    }
}
