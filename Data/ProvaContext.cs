using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProvaDs2.Models;

    public class ProvaContext : DbContext
    {
        public ProvaContext (DbContextOptions<ProvaContext> options)
            : base(options)
        {
        }

        public DbSet<ProvaDs2.Models.Pessoa> Pessoa { get; set; }

        public DbSet<ProvaDs2.Models.Administrador> Administrador { get; set; }

        public DbSet<ProvaDs2.Models.Armario> Armario { get; set; }

        public DbSet<ProvaDs2.Models.Compartimento> Compartimento { get; set; }
    }
