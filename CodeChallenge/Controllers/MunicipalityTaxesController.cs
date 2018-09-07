using System;
using System.Threading.Tasks;
using CodeChallenge.Core.Interfaces.Services;
using CodeChallenge.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{    
    public class MunicipalityTaxesController : Controller
    {
        private readonly IMunicipalityTaxesService _municipalityTaxesService;

        public MunicipalityTaxesController(IMunicipalityTaxesService municipalityTaxesService)
        {
            _municipalityTaxesService = municipalityTaxesService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet]
        [Route("api/MunicipalityTaxes/TaxTypes")]
        public async Task<IActionResult> GetTaxTypesAsync()
        {
            var taxTypes = await _municipalityTaxesService.GetTaxTypesAsync();
            return Ok(taxTypes);
        }

        [HttpGet]
        [Route("api/MunicipalityTaxes/All")]
        public async Task<IActionResult> GetMunicipalityTaxesAsync()
        {
            var municipalitiesWithTaxes = await _municipalityTaxesService.GetMunicipalitiesWithTaxesAsync();
            return Ok(municipalitiesWithTaxes);
        }

        [HttpGet]
        [Route("api/MunicipalityTaxes/Tax")]
        public async Task<IActionResult> GetMunicipalityTaxAsync(string municipalityName, DateTime date)
        {
            var municipalityTax = await _municipalityTaxesService.GetMunicipalityTaxAsync(municipalityName, date);
            return Ok(municipalityTax);
        }
        
        [HttpPost]
        [Route("api/MunicipalityTaxes/Add")]
        public async Task<IActionResult> AddMunicipalityTaxAsync([FromBody] TaxesModel municipalityTaxModel)
        {
            if (!ModelState.IsValid)
            {
                // Handle bad format 
                return StatusCode(400);
            }

            await _municipalityTaxesService.AddMunicipalityTax(municipalityTaxModel);
            return Ok();
        }

        [HttpPut]
        [Route("api/MunicipalityTaxes/Update")]
        public async Task<IActionResult> UpdateMunicipalityTax([FromBody] TaxesModel municipalityTaxModel)
        {
            if (!ModelState.IsValid)
            {
                // Handle bad format 
                return StatusCode(400);
            }

            await _municipalityTaxesService.UpdateMunicipalityTax(municipalityTaxModel);
            return Ok();
        }

        [HttpPut]
        [Route("api/MunicipalityTaxes/Import")]
        public async Task<IActionResult> ImportMunicipalityTaxes()
        {
            await _municipalityTaxesService.ImportDataFromCsv();
            return Ok();
        }
    }
}