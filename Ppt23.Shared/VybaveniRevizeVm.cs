
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared;

public class VybaveniRevizeVm
{
    public Guid Id { get; set; }

    public Guid RevizeId { get; set; }
    public string? Name { get; set; }

    public List<RevizeViewModel> Revizes { get; set; } = new List<RevizeViewModel>();
    public List<VybaveniUkonVm> Ukons { get; set; } = new List<VybaveniUkonVm>();
}