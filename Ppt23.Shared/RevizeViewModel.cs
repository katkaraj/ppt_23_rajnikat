
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ppt23.Shared;


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

