using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Data;
using ServiceDb.Models;

namespace WebServiceGame6813Team6.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class GamePreferencesController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public GamePreferencesController(ServiceDbContext context)
        {
            _context = context;
        }

        // GET: api/GamePreferences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamePreference>>> GetGamePreferences()
        {
          if (_context.GamePreferences == null)
          {
              return NotFound();
          }
            return await _context.GamePreferences.ToListAsync();
        }

        // GET: api/GamePreferences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamePreference>> GetGamePreference(long id)
        {
          if (_context.GamePreferences == null)
          {
              return NotFound();
          }
            var gamePreference = await _context.GamePreferences.FindAsync(id);

            if (gamePreference == null)
            {
                return NotFound();
            }

            return gamePreference;
        }

        [HttpGet("U/{userID}")]
        public async Task<ActionResult<IEnumerable<GamePreference>>> GetUsersGamePreferences(long userID)
        {
            var user = await _context.Users.FindAsync(userID);
            var userProfile = await _context.Profiles.SingleOrDefaultAsync(p => p.UserId == user.Id);

            var preferences = _context.GamePreferences.Where(p => p.ProfileId == userProfile.ProfileId).ToList();

            return preferences;

        }

        // PUT: api/GamePreferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamePreference(long id, GamePreference gamePreference)
        {
            if (id != gamePreference.PrefId)
            {
                return BadRequest();
            }

            _context.Entry(gamePreference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamePreferenceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GamePreferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GamePreference>> PostGamePreference(GamePreference gamePreference)
        {
          if (_context.GamePreferences == null)
          {
              return Problem("Entity set 'ServiceDbContext.GamePreferences'  is null.");
          }
            _context.GamePreferences.Add(gamePreference);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamePreference", new { id = gamePreference.PrefId }, gamePreference);
        }

        [HttpPost("{userID}/{gameID}/{ELO}")]
        public async Task<ActionResult<GamePreference>> CreateGamePreference(long userID, long gameID, long ELO)
        {
            var user = await _context.Users.FindAsync(userID);
            var userProfile = await _context.Profiles.SingleOrDefaultAsync(p => p.UserId == user.Id);

            var game = await _context.Games.FindAsync(gameID);

            var gp = new GamePreference();
            gp.ProfileId = userProfile.ProfileId;
            gp.GameId = game.GameId;
            gp.Elo = ELO;
            gp.Region = userProfile.Region;
            gp.BehaviorIndex = userProfile.BehaviorIndex;

            _context.GamePreferences.Add(gp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamePreference", new { id = gp.PrefId }, gp);

        }

        // DELETE: api/GamePreferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamePreference(long id)
        {
            if (_context.GamePreferences == null)
            {
                return NotFound();
            }
            var gamePreference = await _context.GamePreferences.FindAsync(id);
            if (gamePreference == null)
            {
                return NotFound();
            }

            _context.GamePreferences.Remove(gamePreference);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamePreferenceExists(long id)
        {
            return (_context.GamePreferences?.Any(e => e.PrefId == id)).GetValueOrDefault();
        }
    }
}
