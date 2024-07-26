using System.Collections;

namespace ChampionsLeagueLibrary;

#region Třída PlayersCountChangedEventArgs
// TODO: Vytvořte třídu PlayersCountChangedEventArgs (dědící z EventArgs)
// - vlastnost int OldCount
// - vlastnost int NewCount

public class PlayersCountChangedEventArgs : EventArgs
{
    public int OldCount { get; set; }
    public int NewCount { get; set; }

    public PlayersCountChangedEventArgs(int oldCount, int newCount)
    {
        OldCount = oldCount;
        NewCount = newCount;
    }
}

#endregion

// TODO: Vytvořte delegát pro událost PlayersCountChangedEventHandler (využijte výše definované event args)
public delegate void PlayersCountChangedEventHandler(object sender, PlayersCountChangedEventArgs e);

#region Třída PlayersRecords
// TODO: Vytvořte třídu PlayersRecords

// TODO: Vytvořte atribut players typu Player[]

// TODO: Vytvořte vlastnost Count (pouze pro čtení), která vždy vrací aktuální velikost pole players

// TODO: Vytvořte událost PlayersCountChanged (PlayersCountChangedEventHandler)

// TODO: vytvořte indexer [int index], který umožňuje získat Player? z pole (pouze operace get)
// - get - pokud je index neplatný, je vráceno null; jinak je vrácen objekt z pole

// TODO: Vytvořte bezparametrický konstruktor
// - inicializujte pole players na pole délky 0

// TODO: Vytvořte metodu void Add(Player player)
// - zvětšete velikost pole o jeden prvek
// - na poslední index v poli je vložen nový hráč (player)
// - vyvolejte událost PlayersCountChanged s příslušnými argumenty

// TODO: Vytvořte metodu void Delete(int index)
// - pokud je index neplatný - metoda nedělá nic
// - odeberte vybraného hráče z pole, pole setřeste (posuňte hráče z vyšších indexů, tak aby byla zaplněna (null) mezera; zachovejte pořadí hráčů)
// - zmenšete velikost pole o 1 prvek
// - vyvolejte událost PlayersCountChanged s příslušnými argumenty

// TODO: Vytvořte metodu bool FindBestClubs(...)
// - výstupní parametr FootballClub[] clubs
// - výstupní parametr int goalCount
// - pokud je pole players prázdné - pak - clubs: prázdné pole, goalCount: 0, metoda vrací false
// - sečtěte počty gólů podle klubů
// - najděte maximální počet gólů a uložte jej do goalCount
// - najděte všechny kluby, které mají celkově goalCount gólů a uložte je do clubs
// - vraťte true

public class PlayersRecords : IEnumerable, IPlayersSaveableLoadable
{
    private ObjectLinkedList players;

    public int Count
    {
        get
        {
            return players.Count;
        }
    }

    public event PlayersCountChangedEventHandler? PlayersCountChanged;

    public Player? this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                return null;
            }
            else
            {
                return (Player?)players[index];
            }
        }
    }

    public PlayersRecords()
    {
        players = new ObjectLinkedList();
    }

    public void Add(Player player)
    {
        players.Add(player);

        PlayersCountChanged?.Invoke(this, new PlayersCountChangedEventArgs(players.Count - 1, players.Count)
        {

        });
    }

    public void Delete(int index)
    {
        if (index < Count)
        {
            players.RemoveAt(index);

            PlayersCountChanged?.Invoke(this, new PlayersCountChangedEventArgs(players.Count + 1, players.Count)
            {

            });
        }

    }

    public bool FindBestClubs(out FootballClub[] clubs, out int goalCount)
    {
        goalCount = 0;
        clubs = new FootballClub[0];
        if (Count == 0)
        {
            return false;
        }

        int[] goalsTotal = new int[FootballClubInfo.Count - 1];
        FootballClub[] clubsList = new FootballClub[FootballClubInfo.Count - 1];

        int count = 0;
        foreach (FootballClub club in FootballClubInfo.Items)
        {
            if (club != FootballClub.None)
            {
                clubsList[count++] = club;
            }
        }

        for (int i = 0; i < Count; i++)
        {
            for (int j = 0; j < clubsList.Length; j++)
            {
                Player pl = (Player)players[i];

                if (pl.Club == clubsList[j])
                {
                    goalsTotal[j] = goalsTotal[j] + pl.GoalCount;
                }
            }
        }

        goalCount = goalsTotal.Max();

        int size = 0;
        for (int i = 0; i < goalsTotal.Length; i++)
        {
            if (goalsTotal[i] == goalCount)
            {
                Array.Resize(ref clubs, clubs.Length + 1);
                size++;
                clubs[clubs.Length - 1] = clubsList[i];
            }
        }
        return true;
    }

    public IEnumerator GetEnumerator()
    {
        return players.GetEnumerator();
    }

    //metoda Save vrátí pole všech hráčů (velikost pole dle počtu hráčů)
    public Player[] Save()
    {
        Player[] array = new Player[players.Count];
        for (int i = 0; i < players.Count; i++)
        {
            array[i] = (Player)players[i];
        }

        return array;
    }

    //metoda Load smaže všechny stávající záznamy hráčů a přidá záznamy z pole loadedPlayers
    public void Load(Player[] loadedPlayers)
    {
        players.Clear();
        for (int i = 0; i < loadedPlayers.Length; i++)
        {
            players.Add(loadedPlayers[i]);
        }
    }
}

#endregion