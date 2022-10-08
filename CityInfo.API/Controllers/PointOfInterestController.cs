using AutoMapper;
using CityInfo.API.Entites;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{


    [Route("api/cities/{cityid}/pointofinterest")]
    [Authorize]
    [ApiController]

    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        private readonly IMailService mailService;
        private readonly IMapper mapper;
        private readonly ICityInfoRepository _cityInfoRepository;


        public PointOfInterestController(
            ILogger<PointOfInterestController> logger,
            IMailService mailService,
            IMapper mapper,
            ICityInfoRepository CityInfoRepository)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mailService = mailService;
            this.mapper = mapper;
            _cityInfoRepository = CityInfoRepository;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(
            int cityid)
        {
            var CityName = User.Claims.FirstOrDefault(c => c.Type == "city")?.Value;
            if(!await _cityInfoRepository.CityNameMatchesCityId(CityName, cityid))
            {
                return Forbid();
            }
           
            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"{cityid} nottttttt");
                return NotFound();
            }
            var pointsOfInterestForCity = await _cityInfoRepository
               .GetPointsOfInterestForCityAsync(cityid);

            return Ok(mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForCity));
        }


        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(
            int cityid, int pointOfInterestId)
        {

            if (!await _cityInfoRepository.CityExistsAsync(cityid))
            {
                _logger.LogInformation($"{cityid} nottttttt");
                return NotFound();
            }

            var pointOfInterest = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityid, pointOfInterestId);
            if (pointOfInterest == null)
            {

                return NotFound();
            }
            return Ok(mapper.Map<PointOfInterestDto>(pointOfInterest));

            //var city = citiesDatastore.Cities.FirstOrDefault(c => c.Id == cityid);
            //if (city == null)
            //{
            //    return NotFound();
            //}
            //// find point of interest 

            //var pointOfInterest =city.PointOfInterest.FirstOrDefault(c =>c.Id== pointOfInterestId);
            //if(pointOfInterest == null)
            //{
            //    return NotFound();
            //}
            //return Ok(pointOfInterest);
        }
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
            int cityId,
            PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var finalPointOfInterest = mapper.Map<Entites.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, finalPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var CreatedPointOfInterestToReturn =
                mapper.Map<Models.PointOfInterestDto>(finalPointOfInterest);
           
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityid = cityId,
                    pointOfInterestId = CreatedPointOfInterestToReturn.Id
                },
                CreatedPointOfInterestToReturn );

            //    if (!ModelState.IsValid) 
            //    {
            //        return BadRequest();
            //    }
            //    var city = citiesDatastore.Cities.FirstOrDefault(c => c.Id == cityId);
            //    if (city == null)
            //    {
            //        return NotFound();
            //    }
            //    var maxPointOfIntererstId = citiesDatastore.Cities.SelectMany(
            //        c => c.PointOfInterest
            //        ).Max(p => p.Id);

            //    var finalPointOfrInterest = new PointOfInterestDto()
            //    {
            //        Id = ++maxPointOfIntererstId,
            //        Name = pointOfInterest.Name,
            //        Description = pointOfInterest.Description
            //    };
            //    city.PointOfInterest.Add(finalPointOfrInterest);

            //    return CreatedAtRoute("GetPointOfInterest",
            //        new
            //        {
            //            CityId = cityId,
            //            pointOfInterest = finalPointOfrInterest.Id
            //        }
            //        );
        }

        [HttpPut("{pointofinterestid}")]
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId, int pointofinterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    return NotFound();
                }
                var pointOfInterestEntity = await _cityInfoRepository
                    .GetPointOfInterestForCityAsync(cityId, pointofinterestId);

                if (pointOfInterestEntity == null)
                {
                    return NotFound();
                }
                mapper.Map(pointOfInterest, pointOfInterestEntity);

                await _cityInfoRepository.SaveChangesAsync();

                return NoContent();
        }

    
            

        [HttpPatch("{pointofinterestid}")]
        public async Task<ActionResult> PartiallyUpdatePointOfInterest(
                int cityId, int pointofinterestId,
                JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument
                )
        {
                if (!await _cityInfoRepository.CityExistsAsync(cityId))
                {
                    return NotFound();
                }
                // find point of interest 
                var pointOfInterestEntity = await _cityInfoRepository
                    .GetPointOfInterestForCityAsync(cityId, pointofinterestId);

                if (pointOfInterestEntity == null)
                {
                    return NotFound();
                }
                var PointOfInteresrToPatch = mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);
                    
                patchDocument.ApplyTo(PointOfInteresrToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (!TryValidateModel(PointOfInteresrToPatch))
                {
                    return BadRequest(ModelState);
                }
                mapper.Map(PointOfInteresrToPatch, pointOfInterestEntity);
                await _cityInfoRepository.SaveChangesAsync();
                return NoContent();
        }

           
        
            [HttpDelete("{pointofinterestId}")]
            public async Task<ActionResult> DeletePointOfInterest(
                int cityId, int pointofinterestId)
            {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            // find point of interest 
            var pointOfInterestEntity = await _cityInfoRepository
                .GetPointOfInterestForCityAsync(cityId, pointofinterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }
            _cityInfoRepository.DeletePointOfInterest(pointOfInterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

                mailService.Send("point of interest delete", $"point of interest {pointOfInterestEntity.Name} " +
                    $"with id {pointOfInterestEntity.Id}");
                return NoContent();
            }
    }
}
