using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSuperHero(int id)
        {
            var objHero = await _context.SuperHeroes.FindAsync(id);

            if (objHero == null) return BadRequest("Hero not found");

            return Ok(objHero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> AddHero(SuperHero objSuperHero)
        {
            try
            {
                _context.SuperHeroes.Add(objSuperHero);
                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero(SuperHero objSuperHero)
        {
            try
            {
                var objHero = await _context.SuperHeroes.FindAsync(objSuperHero.Id);

                if (objHero == null) return BadRequest("Hero not found");

                objHero.Name = objSuperHero.Name;
                objHero.FirstName = objSuperHero.FirstName;
                objHero.LastName = objSuperHero.LastName;
                objHero.Place = objSuperHero.Place;

                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            try
            {
                var objHero = await _context.SuperHeroes.FindAsync(id);

                if (objHero == null) return BadRequest("Hero not found");

                _context.SuperHeroes.Remove(objHero);
                await _context.SaveChangesAsync();

                return Ok(await _context.SuperHeroes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
