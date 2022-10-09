using Microsoft.AspNetCore.Mvc;
using FormulaOneApp.Models;
using System.Diagnostics.Metrics;

namespace FormulaOneApp.Controllers;

[Route(template:"api/[controller]")] // api/teams
[ApiController]
public class TeamsController : ControllerBase
{
    private static List<Team> teams = new List<Team>()
    {
        new Team()
        {
            Country = "Germany",
            Id = 1,
            Name = "Mercedes AMG F1",
            TeamPrincipal = "Toto Wolf"
        },
        new Team()
        {
            Country = "Italy",
            Id = 2,
            Name = "Ferrari",
            TeamPrincipal = "Mattia Binotto"
        },
        new Team()
        {
            Country = "Swiss",
            Id = 3,
            Name = "Alfa Romeo",
            TeamPrincipal = "Frederic Vasseur"
        },
    };

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(teams);
    }

    [HttpGet(template:"{id:int}")]
    public IActionResult Get(int id)
    {
        var team = teams.FirstOrDefault(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        return Ok(team);
    }

    [HttpPost]
    public IActionResult Post(Team team)
    {
        teams.Add(team);

        return CreatedAtAction("Get", team.Id, team);
    }

    [HttpPatch]
    public IActionResult Patch(int id, string country)
    {
        var team = teams.FirstOrDefault(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        team.Country = country;

        return NoContent();
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var team = teams.FirstOrDefault(x => x.Id == id);

        if (team == null)
            return BadRequest(error: "Invalid Id");

        teams.Remove(team);

        return NoContent();
    }
}
