using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductionFloorPlacementService.Data;
using ProductionFloorPlacementService.Models;
using ProductionFloorPlacementService.Services;

namespace ProductionFloorPlacementService.ServiceTests
{
    [TestClass]
    public class ContractServiceTests
    {
        private ProductionFloorPlacementServiceContext _context;
        private ContractService _contractService;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ProductionFloorPlacementServiceContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _context = new ProductionFloorPlacementServiceContext(options);
            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated();
            _contractService = new ContractService(_context);

            _context.ProductionFloorModel.Add(new ProductionFloorModel
            {
                Area = 100,
                Name = "floor 2",
            });
            _context.ProductionFloorModel.Add(new ProductionFloorModel
            {
                Area = 1200,
                Name = "floor 3",
            });
            _context.ProductionFloorModel.Add(new ProductionFloorModel
            {
                Area = 103120,
                Name = "floor 4",
            });
            _context.TechnologicalEquipmentModel.Add(new TechnologicalEquipmentModel
            {
                Area = 10,
                Name = "toster",
            });
            _context.TechnologicalEquipmentModel.Add(new TechnologicalEquipmentModel
            {
                Area = 3,
                Name = "chair",
            });
            _context.TechnologicalEquipmentModel.Add(new TechnologicalEquipmentModel
            {
                Area = 5,
                Name = "table",
            });
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task CreateContract_ShouldAddPlacementContractModelToContext()
        {
            // Arrange
            var placementContractDto = new PlacementContractDto
            {
                ProductionFloorModelId = 1,
                TechnologicalEquipmentModelId = 2,
                TechnologicalEquipmentQuantity = 3
            };

            // Act
            await _contractService.CreateContract(placementContractDto);

            // Assert
            Assert.AreEqual(1, _context.PlacementContractModel.Count());
        }

        [TestMethod]
        public async Task CreateContract_ShouldSaveChanges()
        {
            // Arrange
            var placementContractDto = new PlacementContractDto
            {
                ProductionFloorModelId = 1,
                TechnologicalEquipmentModelId = 2,
                TechnologicalEquipmentQuantity = 3
            };

            // Act
            var contract1 = await _contractService.CreateContract(placementContractDto);
            var save = await _context.SaveChangesAsync();
            // Assert
            Assert.AreEqual(0, save);
        }

        [TestMethod]
        public async Task GetContract_WithValidId_ShouldReturnContract()
        {
            // Arrange
            var contractId = 1;
            var placementContractModel = new PlacementContractModel
            {
                Id = contractId,
                ProductionFloorModelId = 1,
                TechnologicalEquipmentModelId = 2,
                TechnologicalEquipmentQuantity = 3
            };
            _context.PlacementContractModel.Add(placementContractModel);
            await _context.SaveChangesAsync();

            // Act
            var result = await _contractService.GetContract(contractId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(contractId, result.Id);
        }

        [TestMethod]
        public async Task GetContract_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var contractId = 999;

            // Act and Assert
            Exception exception = null;
            try
            {
                await _contractService.GetContract(contractId);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
            Assert.AreEqual("Not found", exception.Message);
        }

        [TestMethod]
        public async Task GetContracts_ShouldReturnListOfContracts()
        {
            // Arrange
            var placementContractModels = new List<PlacementContractModel>
        {
            new PlacementContractModel { ProductionFloorModelId = 1, TechnologicalEquipmentModelId = 2, TechnologicalEquipmentQuantity = 3 },
            new PlacementContractModel { ProductionFloorModelId = 3, TechnologicalEquipmentModelId = 3, TechnologicalEquipmentQuantity = 6 }
        };
            _context.PlacementContractModel.AddRange(placementContractModels);
            await _context.SaveChangesAsync();

            // Act
            var result = await _contractService.GetContracts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(placementContractModels.Count, result.Count);
        }
    }
}