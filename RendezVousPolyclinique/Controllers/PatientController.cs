using ApiTools.Logging.Interfaces;
using MappersTool;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<PatientEntity, int> _repo;
        private readonly ILoggerManager _log;
        public PatientController(IRepository<PatientEntity, int> repo, ILoggerManager logger)
        {
            this._repo = repo;
            _log = logger;
        }

     

        [HttpGet]
        public  IActionResult Get()        {
            throw new Exception("Test extensions");
            return new OkObjectResult(_repo.Get().Select(p=> PatientMapper.MapToModel(p)));
        }

       
        [HttpPost]
        public IActionResult Post(PatientModel patient)
        { 
            try
            {
                _log.LogDebug($"[PatienController][Post] -Insertion");
                if (_repo.Insert(PatientMapper.MapToEntity(patient)))
                {
                    return NoContent();
                }
                else
                {
                    _log.LogWarning($"[PatienController][Post] - Erreur d'insertion DB");
                    throw new InvalidOperationException("Erreur d'insertion DB");
                }
                
            }
            catch (Exception ex)
            {
                _log.LogError($"[PatienController][Post] - Exception : {ex.Message} - StackTrace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }

        }
    }
}
