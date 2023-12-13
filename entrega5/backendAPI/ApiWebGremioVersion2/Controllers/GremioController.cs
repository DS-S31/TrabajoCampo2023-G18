using ApiWebGremioVersion2.Data;
using ApiWebGremioVersion2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWebGremioVersion2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GremioController : ControllerBase
    {

        private readonly GremioBDContext _dbContext;

        public GremioController(GremioBDContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var odontologos = _dbContext.Odontologo.Select(n => new OdontologoDTO()
            {
                apellido=n.apellido,
                dni=n.dni,
                nombre=n.nombre,
                id= n.ID
            });
            return Ok(odontologos);
        }

        [HttpPost("AgregarOdontologo")]
        public ActionResult AgregarOdontologo([FromBody] OdontologoDTO model)
        {
            if(model== null)
            {
                return BadRequest("No hay datos");
            }
            Odontologo odontologo = new Odontologo()

            {
                apellido = model.apellido,
                dni = model.dni,
                nombre = model.nombre,
            };

            _dbContext.Odontologo.Add(odontologo);
            _dbContext.SaveChanges();


            return Ok(odontologo);
        }

        [HttpDelete("EliminarOdontologoPorId")]
        public ActionResult Eliminar(int n1)
        {
            if (n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var odontologo = _dbContext.Odontologo.Where(n => n.ID == n1).FirstOrDefault();
            if (odontologo==null)
            {
                return NotFound("No se encontró el odontologo");
            }

            _dbContext.Odontologo.Remove(odontologo);

            _dbContext.SaveChanges();
            return Ok(odontologo);
        }


        [HttpGet("GetAllProvincia")]
        public ActionResult GetAllProvincia()
        {
            var provincia = _dbContext.Provincia.Select(n => new ProvinciaDTO()
            {
                Id=n.ID,
                nombre=n.nombre
            });
            return Ok(provincia);
        }

        [HttpPost("AgregarProvincia")]
        public ActionResult AgregarProvincia([FromBody] ProvinciaDTO model)
        {
            if (model == null)
            {
                return BadRequest("No hay datos");
            }
            Provincia provincia = new Provincia()

            {
                nombre = model.nombre,
            };

            _dbContext.Provincia.Add(provincia);
            _dbContext.SaveChanges();

            return Ok(provincia);
        }

        [HttpDelete("EliminarProvinciaPorID")]
        public ActionResult EliminarP(int n1)
        {
            if (n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var provincia = _dbContext.Provincia.Where(n => n.ID == n1).FirstOrDefault();
            if (provincia == null)
            {
                return NotFound("No se encontró la provincia");
            }

            _dbContext.Provincia.Remove(provincia);

            _dbContext.SaveChanges();
            return Ok(provincia);
        }

        [HttpGet("GetAllLocalidad")]
        public ActionResult GetAllLocalidad()
        {
            var localidad = _dbContext.Localidad.Select(n => new LocalidadDTO()
            {
                Id=n.ID,
                nombre = n.nombre,
                codigoPostal=n.codigoPostal,
                idProvincia=n.idProvincia

            });
            return Ok(localidad);
        }

        [HttpGet("LocalidadPorID")]
        public ActionResult GetLocalidad(int n1)
        {
            if (n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var localidad = _dbContext.Localidad.Where(n => n.ID == n1).FirstOrDefault();
            if (localidad == null)
            {
                return NotFound("No se encontró la localidad");
            }


            return Ok(localidad);
        }

        [HttpPost("AgregarLocalidad")]
        public ActionResult AgregarLocalidad([FromBody] LocalidadDTO model)
        {
            if (model == null)
            {
                return BadRequest("No hay datos");
            }
            Localidad localidad = new Localidad()

            {
                nombre = model.nombre,
                codigoPostal = model.codigoPostal,
                idProvincia=model.idProvincia
            };

            _dbContext.Localidad.Add(localidad);
            _dbContext.SaveChanges();

            return Ok(localidad);
        }

        [HttpDelete("EliminarLocalidadPorID")]
        public ActionResult EliminarLocalidad(int n1)
        {
            if (n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var localidad = _dbContext.Localidad.Where(n => n.ID == n1).FirstOrDefault();
            if (localidad == null)
            {
                return NotFound("No se encontró la localidad");
            }

            _dbContext.Localidad.Remove(localidad);

            _dbContext.SaveChanges();

            return Ok(localidad);
        }

    


        [HttpGet("GetAllConsultorio")]
        public ActionResult GetAllConsultorio()
        {
            var consultorio = _dbContext.Consultorio.Select(n => new ConsultorioDTO()
            {
                Id=n.ID,
                calle = n.calle,
                numero = n.numero,
                idLocalidad=n.idLocalidad
            });
            return Ok(consultorio);
        }

        [HttpGet("GetConsultoriobyID")]
        public ActionResult GetConsultoriobyID(int n1)
        {
            if(n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var consultorio = _dbContext.Consultorio.Where(n => n.ID == n1).FirstOrDefault();
            if (consultorio == null)
            {
                return NotFound("No se encontró el consultorio");
            }

            return Ok(consultorio);
        }

        [HttpPost("AgregarConsultorio")]
        public ActionResult AgregarConsultorio([FromBody] ConsultorioDTO model)
        {
            if (model == null)
            {
                return BadRequest("No hay datos");
            }
            Consultorio consultorio = new Consultorio()

            {
                calle = model.calle,
                numero = model.numero,
                idLocalidad=model.idLocalidad
            };

            _dbContext.Consultorio.Add(consultorio);
            _dbContext.SaveChanges();

            return Ok(consultorio);
        }

        [HttpDelete("EliminarConsultorioPorID")]
        public ActionResult EliminarConsultorio(int n1)
        {
            if (n1 == 0)
            {
                return BadRequest("No hay ningun id 0");
            }
            var consultorio = _dbContext.Consultorio.Where(n => n.ID == n1).FirstOrDefault();
            if (consultorio == null)
            {
                return NotFound("No se encontró el consultorio");
            }

            _dbContext.Consultorio.Remove(consultorio);

            _dbContext.SaveChanges();
            return Ok(consultorio);
        }

        [HttpGet("GetAllCO")]
        public ActionResult GetAllCO()
        {
            var localidad = _dbContext.ConsultorioOdontologo;
            return Ok(localidad);
        }
        [HttpGet("GetAllCOByO")]
        public ActionResult GetAllCOByO(int n)
        {
            if (n == 0)
            {
                return BadRequest("Debe poner un id distinto a 0");
            }
            var consultoriosDeO = _dbContext.ConsultorioOdontologo.Where(co=>co.idOdontologo==n)
                 .ToList(); ;
            if(consultoriosDeO == null)
            {
                return NotFound($"No se encontró ningun consultorio del odontologo de id {n} ");
            }
            return Ok(consultoriosDeO);
        }

        [HttpPost("CargarCO")]
        public ActionResult CargarCO([FromBody] ConsultorioOdontologoDTO model)
        {
            if (model == null){
                return BadRequest();
            }
            ConsultorioOdontologo consultorioOdontologo = new ConsultorioOdontologo()
            {
                idConsultorio = model.idConsultorio,
                idOdontologo = model.idOdontologo
            };
            _dbContext.ConsultorioOdontologo.Add(consultorioOdontologo);
            _dbContext.SaveChanges();

            return Ok(consultorioOdontologo);
        }

        [HttpDelete("EliminarCO")]
        public ActionResult EliminarCdeO(int id)
        {
            if (id == 0)
            {
                return BadRequest("Debe poner un id distinto a 0");
            }
            var co = _dbContext.ConsultorioOdontologo.Where(a => a.ID == id).FirstOrDefault();
            if (co == null)
            {
                return NotFound($"No se encontró ningun consultorio del odontologo de id {id} ");
            }

            _dbContext.ConsultorioOdontologo.Remove(co);
            return Ok(_dbContext.ConsultorioOdontologo);
        }
    }
}