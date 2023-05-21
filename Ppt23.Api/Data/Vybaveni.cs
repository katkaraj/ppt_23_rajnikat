using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Ppt23.Shared;
using Mapster;

namespace Ppt23.Api.Data
{
	public class Vybaveni
	{
		[Key]
        public Guid Id { get; set; }

        [Required, MinLength(5, ErrorMessage = "Název musí být alespoň 5 písmen!")]
        public string Name { get; set; } = "";
        public DateTime BoughtDateTime { get; set; }
        public DateTime LastRevisionDateTime { get; set; }
        public bool IsRevisionNeeded { get => LastRevisionDateTime < DateTime.Now.AddYears(-2); }
        public int Price { get; set; }
        public List<Revize> Revizes { get; set; } = new();

        public void pridejRevizis(PptDbContext db)
        {
            Random rnd = new Random();

            for (int i=0; i<rnd.Next(0,3); i++)
            {
                Revize rev = new Revize()
                {
                    Name = $"Revize {Name} #{i + 1}",
                    Id = Guid.Empty,
                    Vybaveni = this,
                    VybaveniId = Id
                };
                

                rev.randDate(this.Adapt<VybaveniVm>(), this.BoughtDateTime, DateTime.Today);

                Revizes.Add(rev);
                db.Revizes.Add(rev);
                
            }
        }
    }

}

