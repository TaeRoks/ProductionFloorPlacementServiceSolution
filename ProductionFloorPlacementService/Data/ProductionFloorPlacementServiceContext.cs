using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Data.Models;

namespace ProductionFloorPlacementService.Data
{
    public class ProductionFloorPlacementServiceContext : DbContext
    {
        public ProductionFloorPlacementServiceContext (DbContextOptions<ProductionFloorPlacementServiceContext> options)
            : base(options)
        {
        }

        public DbSet<ProductionFloorModel> ProductionFloorModel { get; set; } = default!;

        public DbSet<TechnologicalEquipmentModel> TechnologicalEquipmentModel { get; set; } = default!;

        public DbSet<PlacementContractModel> PlacementContractModel { get; set; } = default!;
    }
}
