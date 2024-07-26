using System.Security.Cryptography;

namespace ChampionsLeagueLibrary;

#region Třída Player
// TODO: Vytvořte třídu Player

// TODO: Vytvořte vlastnosti
// - string Name
// - FootballClub Club
// - int GoalCount

// TODO: Vytvořte parametrický konstruktor (name, club, goalCount)

public class Player
{
    public string Name { get; set; }
    public FootballClub Club { get; set; }
    public int GoalCount { get; set; }

    public Player(string name, FootballClub club, int goalCount)
    {
        Name = name;
        Club = club;
        GoalCount = goalCount;
    }

    public override bool Equals(object? obj)
    {
        return obj is Player player &&
               Name == player.Name &&
               Club == player.Club &&
               GoalCount == player.GoalCount;
    }

    //GetHashCode vrací libovolné celé číslo pro objekt hráče
    //Pokud pro dva hráče platí pl1.Equals(pl2), pak platí pl1.GetHashCode()==pl2.GetHashCode()
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Club, GoalCount);
    }
}

#endregion
