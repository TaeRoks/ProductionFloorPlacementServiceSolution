using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductionFloorPlacementService.Models;

namespace ProductionFloorPlacementService.Data
{
    public class ProductionFloorPlacementServiceContext : DbContext
    {
        public ProductionFloorPlacementServiceContext (DbContextOptions<ProductionFloorPlacementServiceContext> options)
            : base(options)
        {
        }

        public DbSet<ProductionFloorPlacementService.Models.ProductionFloorModel> ProductionFloorModel { get; set; } = default!;

        public DbSet<ProductionFloorPlacementService.Models.TechnologicalEquipmentModel> TechnologicalEquipmentModel { get; set; } = default!;

        public DbSet<ProductionFloorPlacementService.Models.PlacementContractModel> PlacementContractModel { get; set; } = default!;
    }
}
