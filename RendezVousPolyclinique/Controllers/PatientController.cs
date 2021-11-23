using MappersTool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PolyDB.DAL.Entities;
using PolyDB.DAL.Repositories.Interfaces;
using RendezVousPloyclinique.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RendezVousPolyclinique.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<PatientEntity, int> _repo;
        public PatientController(IRepository<PatientEntity, int> repo)
        {
            this._repo = repo;
        }

     

        [HttpGet]
        public  IActionResult Get()
        {
            return new OkObjectResult(_repo.Get().Select(p=> PatientMapper.MapToModel(p)));
        }
        [HttpPost]
        public IActionResult Post(PatientModel patient)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(patient);
            }
            try
            {
                if(_repo.Insert(PatientMapper.MapToEntity(patient)))
                {
                    return NoContent();
                }
                else
                {
                    throw new InvalidOperationException("Erreur d'insertion DB");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
