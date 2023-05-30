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
        public List<Ukon> Ukons { get; set; } = new();
        public List<Pracovnik> Pracovniks { get; set; } = new();

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

        public void pridejUkons(PptDbContext db)
        {
            Random rnd = new Random();
            string[] ukony = {"CT scan", "MRI", "Endoskopie", "UZ" };

            for (int j = 0; j < rnd.Next(0, 2); j++)
            {
                Ukon ukon = new Ukon()
                {
                    Id = Guid.Empty,
                    Name = ukony[rnd.Next(ukony.Length)],
                    vybaveni = this,
                    VybaveniId = Id

                };
            ukon.randDate(this.Adapt<VybaveniVm>(), this.BoughtDateTime, DateTime.Today);

            Ukons.Add(ukon);
            db.Ukons.Add(ukon);
            }

        }

        public void pridejPracovniks(PptDbContext db)
        {
            Random rnd = new Random();
            string[] jobs = { "RA asistent", "Lékař", "Sestra" };
            for(int k=0; k<2; k++)
            {
                Pracovnik pracovnik = new Pracovnik()
                {
                    Id = Guid.Empty,
                    Name = RandomString(10),
                    Job = jobs[rnd.Next(jobs.Length)],
                };

                Pracovniks.Add(pracovnik);
                db.Pracovniks.Add(pracovnik);
            }
        }

        public string RandomString(int length) =>
           new(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('a', 'z')).ToArray());
    }

}

