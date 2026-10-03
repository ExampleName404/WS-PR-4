using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamApi.Dtos;
using TeamApi.Models;
using TeamApi.Repositories;

namespace TeamApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamRepository _repository;
    private readonly IMapper _mapper;

    public TeamController(ITeamRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ReturnResult>> GetAll()
    {
        var teams = await _repository.GetAllAsync();
        return Ok(ReturnResult.Success(_mapper.Map<IEnumerable<TeamDto>>(teams)));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReturnResult>> Get(int id)
    {
        var team = await _repository.GetAsync(id);
        if (team == null)
            return NotFound(ReturnResult.Fail($"Команда с id={id} не найдена"));

        return Ok(ReturnResult.Success(_mapper.Map<TeamDto>(team)));
    }

    [HttpPost]
    public async Task<ActionResult<ReturnResult>> Create(TeamCreateDto dto)
    {
        var team = _mapper.Map<Team>(dto);
        await _repository.CreateAsync(team);

        return CreatedAtAction(nameof(Get), new { id = team.Id },
            ReturnResult.Success(_mapper.Map<TeamDto>(team)));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ReturnResult>> Update(int id, TeamCreateDto dto)
    {
        var team = await _repository.GetAsync(id);
        if (team == null)
            return NotFound(ReturnResult.Fail($"Команда с id={id} не найдена"));

        _mapper.Map(dto, team);
        await _repository.UpdateAsync(team);

        return Ok(ReturnResult.Success(_mapper.Map<TeamDto>(team)));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ReturnResult>> Delete(int id)
    {
        var team = await _repository.GetAsync(id);
        if (team == null)
            return NotFound(ReturnResult.Fail($"Команда с id={id} не найдена"));

        await _repository.DeleteAsync(id);
        return Ok(ReturnResult.Success());
    }

    [HttpGet("city/{city}")]
    public async Task<ActionResult<ReturnResult>> GetByCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            return BadRequest(ReturnResult.Fail("Город не указан"));

        var teams = await _repository.GetByCityAsync(city);
        return Ok(ReturnResult.Success(_mapper.Map<IEnumerable<TeamDto>>(teams)));
    }
}
