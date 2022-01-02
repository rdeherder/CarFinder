using AutoMapper;
using CarFinderApi.Configurations;
using CarFinderApi.Controllers;
using CarFinderApi.Library.ExternalDataAccess;
using CarFinderApi.Library.Models;
using CarFinderApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderApi.Test
{
    public class IntegrationTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperInitializer>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public async Task GetCars_ReturnsListOfCars()
        {
            // Arrange
            var controller = GetCarsController();

            // Act
            var result = await controller.GetCars();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;

            Assert.IsInstanceOf<IEnumerable<CarDTO>>(okObjectResult.Value);
            var cars = (IEnumerable<CarDTO>)okObjectResult.Value;

            int expected = FamousCars.Count();
            int actual = cars.Count();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetExistingCar_ReturnsCar()
        {
            // Arrange
            var controller = GetCarsController();
            int existingId = 1;

            // Act
            var result = await controller.GetCar(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;

            Assert.IsInstanceOf<CarDTO>(okObjectResult.Value);
            var car = (CarDTO)okObjectResult.Value;

            Assert.IsNotNull(car);
        }

        [Test]
        public async Task GetNonExistingCar_ReturnsNotFound()
        {
            // Arrange
            var controller = GetCarsController();
            int nonExistingId = 99;

            // Act
            var result = await controller.GetCar(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        private CarsController GetCarsController()
        {
            var externalCarsData = new Mock<IExternalCarsData>();
            var logger = new Mock<ILogger<CarsController>>();

            externalCarsData.Setup(sp => sp.GetCarsAsync())
                                         .Returns(() => Task.FromResult(FamousCars));

            externalCarsData.Setup(sp => sp.GetCarAsync(It.IsAny<int>()))
                                         .Returns((int id) => Task.FromResult(FamousCars.FirstOrDefault(x => x.Id == id)));

            var result = new CarsController(externalCarsData.Object,
                                            logger.Object,
                                            _mapper);

            return result;
        }

        private static IEnumerable<ExternalCarModel> FamousCars
        {
            get
            {
                return new List<ExternalCarModel>
                {
                    Kitt,
                    GeneralLee,
                    DeLorean
                };
            }
        }

        private static ExternalCarModel Kitt = new ExternalCarModel
        {
            Id = 1,
            HorsePower = 200,
            Img_Url = String.Empty,
            Make = "pontiac",
            Model = "tramsam",
            Price = 10000,
            Year = 1984
        };

        private static ExternalCarModel GeneralLee = new ExternalCarModel
        {
            Id = 2,
            HorsePower = 250,
            Img_Url = String.Empty,
            Make = "dodge",
            Model = "charger",
            Price = 15000,
            Year = 1969
        };

        private static ExternalCarModel DeLorean = new ExternalCarModel
        {
            Id = 3,
            HorsePower = 150,
            Img_Url = String.Empty,
            Make = "delorean",
            Model = "dmc-12",
            Price = 18000,
            Year = 1986
        };
    }
}
