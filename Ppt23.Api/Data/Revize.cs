using System.ComponentModel.DataAnnotations;
using Ppt23.Shared;
using Mapster;


namespace Ppt23.Api.Data;

public class Revize
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = "";
    public DateTime DateTime { get; set; }
    public Guid VybaveniId { get; set; }
    public Vybaveni Vybaveni { get; set; } = null!;

    public void randDate(VybaveniVm vyb, DateTime startDate, DateTime endDate)
    {
        Random rnd = new Random();
        var range = endDate - startDate;

        var randTimeSpan = new TimeSpan((long)(rnd.NextDouble() * range.Ticks));

        DateTime = startDate + randTimeSpan;
        
    }
}
