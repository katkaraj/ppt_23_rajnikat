
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Ppt23.Shared;

public class VybaveniVm
{
    [Required, MinLength(5, ErrorMessage = "Zadejte název který má alespoň {1} znaků")]
    [Display(Name = "Název")]
    public string Name { get; set; } = "";
    
    public DateTime BoughtDateTime { get; set; }
    public DateTime LastRevisionDateTime { get; set; }
    public bool IsRevisionNeeded { get => LastRevisionDateTime < DateTime.Now.AddYears(-2); }
    //public bool IsInEditMode { get; set; }
    public Guid Id { get; set; }

    [Required, Range(0, 10000000, ErrorMessage = "Zadejte cenu mezi 0 a 10 000 000")]
    public int Price { get; set; }

    public static List<VybaveniVm> GetTestList(int poc)
    {
        List<VybaveniVm> list = new();
        for (int i=0; i<poc; i++)
        {
            VybaveniVm model = new()
            {
                Name = RandomString(10),
                BoughtDateTime = RandomDateTime(),
                LastRevisionDateTime = DateTime.Now.AddDays(-Random.Shared.Next(0,3*365)),
                Id = Guid.NewGuid(),

            };
            list.Add(model);
        }
        return list;
    }

    public VybaveniVm()
    {
        this.Name = RandomString(10);
        this.BoughtDateTime = RandomDateTime();
        this.LastRevisionDateTime = RevisionRandomDateTime(BoughtDateTime, DateTime.Today);
        //this.IsInEditMode = false;
        this.Price = Random.Shared.Next(10000000);
        this.Id = Guid.NewGuid();
    }

    public VybaveniVm Copy()
    {
        VybaveniVm to = new();
        to.BoughtDateTime = BoughtDateTime;
        to.LastRevisionDateTime = LastRevisionDateTime;
        //to.IsInEditMode = IsInEditMode;
        to.Name = Name;
        to.Price = Price;
        to.Id = Id;
        return to;
    }

    public void MapTo(VybaveniVm? to)
    {
        if (to == null) return;
        to.BoughtDateTime = BoughtDateTime;
        to.LastRevisionDateTime = LastRevisionDateTime;
        to.Name = Name;
        to.Price = Price;
    }
    private static Random rnd = new Random();
    public static string RandomString(int lenght)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, lenght).Select(s => s[rnd.Next(s.Length)]).ToArray());
    }

    public static DateTime RandomDateTime()
    {
        DateTime start = new DateTime(2018, 1, 1);
        int range = (DateTime.Today - start).Days;
        return start.AddDays(rnd.Next(range));
    }

    public static DateTime RevisionRandomDateTime(DateTime from, DateTime to)
    {
        DateTime start = from;
        int range = (to - from).Days;
        return start.AddDays(rnd.Next(range));
    }

    public bool MakeRevision()
    {
        if(((DateTime.Today - LastRevisionDateTime).TotalDays) > 365)
        {
             return true;
        }
        else
        {
            return false;
        }
    }

}
public class RevizeViewModel
{
    public string? Nazev;
    public Guid Id { get; set; }
    public DateTime DateTime { get; set; }
    //Random rnd = new Random();

    public static List<RevizeViewModel> GetRevizeList(int poc)
    {
        List<RevizeViewModel> list = new();
        for (int i = 0; i < poc; i++)
        {
            RevizeViewModel model = new()
            {
                Nazev = RandomString(Random.Shared.Next(5, 25)),
                Id = Guid.NewGuid()

            };
            list.Add(model);
        }
        return list;
    }

    public static string RandomString(int length)=>
    
        new(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('a', 'z')).ToArray()); 
}

public class VybaveniRevizeVm
{
    public Guid Id { get; set; }

    public Guid RevizeId { get; set; }
    public string? Name { get; set; }
    public List<RevizeViewModel> Revizes { get; set; } = new List<RevizeViewModel>();
}
