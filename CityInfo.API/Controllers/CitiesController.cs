﻿using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]
    public class CitiesController :ControllerBase //Base
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int  maxCitiesPageSize= 20;

        public CitiesController(ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities(
            string? name ,string?searchQuery,int pageNumber=1, int pageSize=10
            )
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }
            var (cityEntities,paginationMetadata) = await _cityInfoRepository
                .GetCitiesAsync(name,searchQuery,pageNumber,pageSize);
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
        
                
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id,
            bool includePointOfInterest =false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
            if (city == null)
            {
               return NotFound();
            }
            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));
            //FIND CITY
            //var cityToREturn = citiesDatastore.Cities.FirstOrDefault(c => c.Id == id);
            //if (cityToREturn == null)
            //{
            //    return NotFound();
            //}
            //return Ok(cityToREturn);
            //return new JsonResult(CitiesDatastore.Current.Cities.FirstOrDefault(c =>c.Id==id));
        }
    }
}
