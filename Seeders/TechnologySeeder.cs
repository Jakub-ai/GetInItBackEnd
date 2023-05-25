using GetInItBackEnd.Entities;

namespace GetInItBackEnd.Seeders;

public class TechnologySeeder
{
    private readonly GetInItDbContext _dbContext;

    public TechnologySeeder(GetInItDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Seed()
    {
        if (await _dbContext.Database.CanConnectAsync())
        {
            if (!_dbContext.Technologies.Any())
            {
                var skillJs = new Technology
                {
                    Skill = "JavaScript"
                };
                var skillC = new Technology
                {
                    Skill = "C"
                };
                var skillJava = new Technology
                {
                    Skill = "Java"
                };
                var skillCSharp = new Technology
                {
                    Skill = "C#"
                };
                _dbContext.Technologies.Add(skillJs);
                _dbContext.Technologies.Add(skillC);
                _dbContext.Technologies.Add(skillJava);
                _dbContext.Technologies.Add(skillCSharp);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}