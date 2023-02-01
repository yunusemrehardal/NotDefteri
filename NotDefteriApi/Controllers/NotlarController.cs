using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotDefteriApi.Data;
using NotDefteriApi.Dtos;

namespace NotDefteriApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotlarController : ControllerBase
    {
        private readonly UygulamaDbContext _db;

        public NotlarController(UygulamaDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<List<Not>> GetNotlar()
        {
            return await _db.Notlar.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Not>> GetNot(int id)
        {
            Not? not = await _db.Notlar.FindAsync(id);

            if (not == null)
                return NotFound();

            return not;
        }

        [HttpPost]
        public async Task<ActionResult<Not>> PostNot(YeniNotDto dto)
        {
            if (ModelState.IsValid)
            {
                Not not = new Not()
                {
                    Baslik = dto.Baslik,
                    Icerik = dto.Icerik
                };

                _db.Notlar.Add(not);
                await _db.SaveChangesAsync();
                return not;
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Not>> PutNot(int id, GuncelleNotDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                Not? not = await _db.Notlar.FindAsync(id);

                if (not == null)
                    return NotFound();

                not.Baslik = dto.Baslik;
                not.Icerik = dto.Icerik;
                not.DegistirmeZamani = DateTime.Now;

                await _db.SaveChangesAsync();
                return not;
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNot(int id)
        {
            var not = await _db.Notlar.FindAsync(id);

            if (not == null)
                return NotFound();

            _db.Notlar.Remove(not);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
