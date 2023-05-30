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
                    VybaveniVm vyb = new VybaveniVm();
                    var en = vyb.Adapt<Vybaveni>();
                    
                    en.pridejRevizis(db);
                    en.pridejUkons(db);
                    en.pridejPracovniks(db);
                    db.Vybavenis.Add(en);
                    
                }
            }
            await db.SaveChangesAsync();
        }
    }
}

