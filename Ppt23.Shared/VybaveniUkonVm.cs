
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared;

public class VybaveniUkonVm
{
    public Guid Id { get; set; }

    public Guid UkonId { get; set; }
    public string Name { get; set; } = null;
    public DateTime Date { get; set; }
    
}