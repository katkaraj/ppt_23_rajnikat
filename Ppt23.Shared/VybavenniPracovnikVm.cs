
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared;

public class VybaveniPracovnikVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Job { get; set; } = String.Empty;
    public VybaveniUkonVm ukon { get; set; } = null!;
    
}