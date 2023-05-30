using System.ComponentModel.DataAnnotations;
using Ppt23.Shared;
using Mapster;


namespace Ppt23.Api.Data;

public class Pracovnik
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = " ";
    public string Job { get; set; } = "";
    public List<Ukon> Ukons { get; set; } = new();
}
