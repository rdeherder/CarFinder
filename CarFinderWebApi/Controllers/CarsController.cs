using AutoMapper;
using CarFinderApi.Library.ExternalDataAccess;
using CarFinderUI.BlazorApp;
using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
using CarFinderWebApi.Models;
using GridMvc.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CarFinderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IExternalCarsData _externalCarsData;
        private readonly ILogger<CarsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICarsEndpoint _carsEndpoint;

        public CarsController(IExternalCarsData externalCarsData, ILogger<CarsController> logger, IMapper mapper, ICarsEndpoint carsEndpoint)
        {
            _externalCarsData = externalCarsData;
            _logger = logger;
            _mapper = mapper;
            _carsEndpoint = carsEndpoint;
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarsGridRows()
        {
            try
            {
                var items = await _carsEndpoint.GetAllAsync();
                var server = new GridServer<CarModel>(items,
                                                      Request.Query,
                                                      true,
                                                      "carsGrid",
                                                      ColumnCollections.CarColumns,
                                                      10)
                                                     .ChangePageSize(enable: true)
                                                     .Filterable()
                                                     .Searchable()
                                                     .Selectable(set: true)
                                                     .Sortable()
                                                     .WithMultipleFilters();

                return Ok(server.ItemsToDisplay);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception at {nameof(GetCarsGridRows)}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var cars = await _externalCarsData.GetCarAsync(id);
                var retVal = _mapper.Map<CarDTO>(cars);

                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception at {nameof(GetCar)}");
                return BadRequest(ex.Message);
            }
        }
    }
}
