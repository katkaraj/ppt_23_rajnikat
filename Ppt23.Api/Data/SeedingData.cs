using Ppt23.Shared;
using Mapster;


namespace Ppt23.Api.Data
{
    public class SeedingData
    {
        readonly PptDbContext db;
        public SeedingData(PptDbContext dbContext)
        {
            db = dbContext;
        }

        public async Task SeedData()
        {
            if (!db.Vybavenis.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var en = new VybaveniVm();
                    en.Id = Guid.Empty;
                    db.Vybavenis.Add(en.Adapt<Vybaveni>());
                }
            }

            await db.SaveChangesAsync();
        }
    }
}

