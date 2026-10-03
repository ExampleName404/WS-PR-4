using Microsoft.EntityFrameworkCore;
using TeamApi.Data;
using TeamApi.Models;

namespace TeamApi.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _db;

    public TeamRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _db.Teams.AsNoTracking().ToListAsync();
    }

    public async Task<Team?> GetAsync(int id)
    {
        return await _db.Teams.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateAsync(Team team)
    {
        await _db.Teams.AddAsync(team);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Team team)
    {
        _db.Teams.Update(team);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var team = await _db.Teams.FindAsync(id);
        if (team != null)
        {
            _db.Teams.Remove(team);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Team>> GetByCityAsync(string city)
    {
        var name = city.Trim();
        return await _db.Teams
            .AsNoTracking()
            .Where(t => t.City == name)
            .ToListAsync();
    }
}
