﻿using System;
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