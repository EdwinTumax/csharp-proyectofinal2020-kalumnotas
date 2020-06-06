using System.Collections.Generic;
using KalumNotas.Entities;
using KalumNotas.KalumDBContext;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KalumNotas.Controllers
{
    [Route("/KalumNotas/v1/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly KalumNotasDBContext dBContext;
        public AlumnosController(KalumNotasDBContext dBContext)
        {
            this.dBContext = dBContext;
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get(string nombre)
        {
            List<Alumno> alumnos = null;
            if(nombre == null){
                alumnos =  await dBContext.Alumnos.Include(x => x.Religion).ToListAsync();
            }else {
                alumnos = await dBContext.Alumnos.Where(a => a.Nombres.StartsWith($"{nombre}")).ToListAsync();
            }
            if(alumnos == null)
            {
                return NoContent();
            }
            else 
            {
                return alumnos;
            }
        }
        [HttpGet("{carne}", Name = "GetAlumno")]
        public async Task<ActionResult<Alumno>> Get(string carne, string seccion)
        {
            var alumno = await dBContext.Alumnos.FirstOrDefaultAsync(x => x.Carne == carne);
            if(alumno == null)
            {
                return NotFound();
            }
            else 
            {
                return alumno;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Alumno>> Post([FromBody] Alumno value)
        {
            await dBContext.Alumnos.AddAsync(value);
            await dBContext.SaveChangesAsync();            
            return new CreatedAtRouteResult("GetAlumno", new { carne = value.Carne }, value);
        }

        [HttpPut("{carne}")]
        public async Task<ActionResult> Put(string carne, [FromBody] Alumno value)
        {
            if(carne != value.Carne){
                return BadRequest();
            }
            dBContext.Entry(value).State = EntityState.Modified;
            await dBContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{carne}")]
        public async Task<ActionResult<Alumno>> Delete(string carne)
        {
            var alumno = await dBContext.Alumnos.FirstAsync(x => x.Carne == carne);
            if(alumno == null){
                return NotFound();
            }
            dBContext.Alumnos.Remove(alumno);
            await dBContext.SaveChangesAsync();
            return alumno;
        }

    }
}