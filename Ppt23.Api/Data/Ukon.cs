using System.ComponentModel.DataAnnotations;
using Ppt23.Shared;
using Mapster;


namespace Ppt23.Api.Data;

public class Ukon
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = " ";
    public Guid VybaveniId { get; set; }
    public Vybaveni vybaveni { get; set; } = null!;
    public DateTime DateTime { get; set; }

    public void randDate(VybaveniVm vyb, DateTime startDate, DateTime endDate)
    {
        Random rnd = new Random();
        var range = endDate - startDate;

        var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

        DateTime = startDate + randTimeSpan;
    }
}
