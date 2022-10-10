using Microsoft.AspNetCore.Mvc;
using FormulaOneApp.Models;
using FormulaOneApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneApp.Controllers;

[Route(template:"api/[controller]")] // api/teams
[ApiController]
public class TeamsController : ControllerBase
{
    private static AppDbContext _context;

    public TeamsController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var teams = await _context.Teams.ToListAsync();
        return Ok(teams);
    }

    [HttpGet(template:"{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        return Ok(team);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Team team)
    {
        await _context.Teams.AddAsync(team);
        await _context.SaveChangesAsync();

        return CreatedAtAction("Get", team.Id, team);
    }

    [HttpPatch]
    public async Task<IActionResult> Patch(int id, string country)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        team.Country = country;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
