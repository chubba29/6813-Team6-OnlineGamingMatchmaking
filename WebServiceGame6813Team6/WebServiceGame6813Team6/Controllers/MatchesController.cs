using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServiceDb.Data;
using ServiceDb.Models;
using WebServiceGame6813Team6.Authorization;

namespace WebServiceGame6813Team6.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ServiceDbContext _context;

        public MatchesController(ServiceDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreatedAsync();
        }

        //test comment
        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            return await _context.Matches.ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(long id)
        {
          if (_context.Matches == null)
          {
              return NotFound();
          }
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        //QUESTIONS: could we theoretically pass in the GamePreference ID instead? Not sure how to handle this otherwise
        //Could pass in game ID and profile/user ID and probably get away with it.
        [HttpGet("{userID}/{gameID}")]
        public async Task<ActionResult<Profile>> GetProfileMatch(long userID, long gameID)
        {
            var ELODeviation = 11;
            //find user object
            var user = await _context.Users.FindAsync(userID);
            //grab user profile
            var userProfile = await _context.Profiles.SingleOrDefaultAsync(p => p.UserId == user.Id);

            //get specific game
            var game = await _context.Games.FindAsync(gameID);

            //find gamePreference object based on profile and game
            var gamePreference = await _context.GamePreferences.SingleOrDefaultAsync(g => g.ProfileId == userProfile.ProfileId && g.GameId == game.GameId);

            //find list of possible matches to return to user front end
            var possibleMatches = _context.GamePreferences.Where(g => g.ProfileId != userProfile.ProfileId &&
                    g.GameId == gamePreference.GameId &&
                    g.Region == gamePreference.Region &&
                    g.BehaviorIndex == gamePreference.BehaviorIndex &&
                    g.Elo <= (gamePreference.Elo + ELODeviation) &&
                    g.Elo >= (gamePreference.Elo - ELODeviation)).ToList();

            if (possibleMatches.Count() > 0)
            {
                var random = new Random();
                var randomIndex = random.Next(possibleMatches.Count());

                var match = possibleMatches[randomIndex];

                var matchedProfile = await _context.Profiles.FindAsync(match.ProfileId);

                return matchedProfile;
            }
            else
            {
                return null;
            }
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(long id, Match match)
        {
            if (id != match.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
          if (_context.Matches == null)
          {
              return Problem("Entity set 'ServiceDbContext.Matches'  is null.");
          }
            _context.Matches.Add(match);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MatchExists(match.MatchId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return CreatedAtAction("GetMatch", new { id = match.MatchId }, match);
        }  


        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(long id)
        {
            if (_context.Matches == null)
            {
                return NotFound();
            }
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(long id)
        {
            return (_context.Matches?.Any(e => e.MatchId == id)).GetValueOrDefault();
        }
    }
}
