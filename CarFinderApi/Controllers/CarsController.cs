using AutoMapper;
using CarFinderApi.Library.ExternalDataAccess;
using CarFinderApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IExternalCarsData _externalCarsData;
        private readonly ILogger<CarsController> _logger;
        private readonly IMapper _mapper;

        public CarsController(IExternalCarsData externalCarsData, ILogger<CarsController> logger, IMapper mapper)
        {
            _externalCarsData = externalCarsData;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await _externalCarsData.GetCarsAsync();
                var retVal = _mapper.Map<List<CarDTO>>(cars);

                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception at {nameof(GetCars)}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCar(int id)
        {
            try
            {
                var car = await _externalCarsData.GetCarAsync(id);
                var retVal = _mapper.Map<CarDTO>(car);

                return Ok(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception at {nameof(GetCar)}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
