using System.Collections;

namespace ChampionsLeagueLibrary;

public enum FootballClub
{
    None,
    FCPorto,
    Arsenal,
    RealMadrid,
    Chelsea,
    Barcelona
}

#region Třída FootballClubInfo
// TODO: Vytvořte statickou třídu FootballClubInfo


// TODO: Vytvořte veřejnou konstantu int Count, která vrací počet hodnot (položek) ve výčtovém typu FootballClub

// TODO: Vytvořte veřejnou statickou vlastnost IEnumerable Items, která disponuje pouze operací get
// - pomocí enumerátoru vraťte všechny položky ve výčtovém typu FootballClub (od None až po Barcelona)

// TODO: Vytvořte veřejnou statickou metodu string GetName
// - parametr FootballClub footballClub
// - metoda vrací řetězcové vyjádření pro každou položku výčtového typu
// None -> "", FCPorto -> "FC Porto", Arsenal -> "Arsenal", RealMadrid -> "Real Madrid"
// Chelsea -> "Chelsea", Barcelona -> "Barcelona"
// - jinak je vrácen prázdný řetězec

public static class FootballClubInfo
{
    public const int Count = 6;

    public static IEnumerable Items
    {
        get
        {
            List<FootballClub> list = new List<FootballClub>();
            list.Add(FootballClub.None);
            list.Add(FootballClub.FCPorto);
            list.Add(FootballClub.Arsenal);
            list.Add(FootballClub.RealMadrid);
            list.Add(FootballClub.Chelsea);
            list.Add(FootballClub.Barcelona);
            return list;
        }
    }

    public static string GetName(FootballClub footballClub)
    {
        if (footballClub == FootballClub.FCPorto)
        {
            return "FC Porto";
        }
        else if (footballClub == FootballClub.Arsenal)
        {
            return "Arsenal";
        }
        else if (footballClub == FootballClub.RealMadrid)
        {
            return "Real Madrid";
        }
        else if (footballClub == FootballClub.Chelsea)
        {
            return "Chelsea";
        }
        else if (footballClub == FootballClub.Barcelona)
        {
            return "Barcelona";
        }
        else
        {
            return "";
        }
    }

}

#endregion
