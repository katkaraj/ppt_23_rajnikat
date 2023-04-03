
int cislo = NahodneCislo();
while (true)
{
    Console.WriteLine("Hádej číslo");
    //hadanecislo = Int32.Parse(Console.ReadLine());
    string? vstup = Console.ReadLine();
    if (vstup == null) return;

    if(!int.TryParse(vstup, out int hadanecislo))
    {
        Console.WriteLine("Zadejte číslo");
        continue;
    }

    if (cislo == hadanecislo)
    {
        Console.WriteLine("Uhodl jste!");
        Console.WriteLine($"Chcete hrát znovu? (Y/N)");
        string? hratZnovu = Console.ReadLine();
        if (hratZnovu != null && hratZnovu.ToLower() == "y")
        {
            Console.Clear();
            cislo = NahodneCislo();
        }
        else
            break;

    }
    //if (cislo > hadanecislo)
    //{
    //    Console.WriteLine("Číslo je větší");
    //}
    //if (cislo < hadanecislo)
    //{
    //    Console.WriteLine("Cislo je menší");
    //}
    string mensiVetsi = cislo < hadanecislo ? "MENŠÍ" : "VĚTŠÍ";
    Console.WriteLine($"Tvoje číslo je {mensiVetsi} než myšlené číslo. Zkus to znovu");
}

Console.WriteLine("Díky za hru");
Console.ReadLine();

int NahodneCislo() => Random.Shared.Next(1, 101);

