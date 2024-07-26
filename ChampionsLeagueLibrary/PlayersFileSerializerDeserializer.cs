using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChampionsLeagueLibrary
{
    public class PlayersFileSerializerDeserializer
    {

        readonly IPlayersSaveableLoadable players;
        readonly string file;

        public PlayersFileSerializerDeserializer(IPlayersSaveableLoadable players, string file)
        {
            this.players = players;
            this.file = file;
        }

        //metoda otevře soubor file pro zápis
        //každý hráč v players(získano voláním players.Save()) je zapsán na nový řádek do file
        //pro převod hráče na řetězec je využita metoda Serialize
        //soubor file je uzavřen
        public void Save()
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                foreach (Player player in players.Save())
                {
                    writer.WriteLine(Serialize(player));
                }
                writer.Close();
            }
        }
        /*metoda otevře soubor file pro čtení
        jsou načteny všechny řádky textu a převedeny do pole hráčů(s využitím metody Deserialize)
        soubor file je uzavřen
        hráči jsou nahráni do objektu players(players.Load(...))*/
        public void Load()
        {
            List<Player> list = new List<Player>();
            using (StreamReader reader = new StreamReader(file))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(Deserialize(line));
                }
            }
            players.Load(list.ToArray());
        }

        /*privátní statické metody
        string Serialize(Player p)
        Player Deserialize(string s)
        metody slouží pro převod objektu hráče na string a naopak
        pro metody platí:
        pro každého hráče - Player a
        po provedení - Player b = Deserialize(Serialize(a))
        musí platit - a.Name==b.Name && a.Club==b.Club && a.GoalCount==b.GoalCount*/

        private static string Serialize(Player p)
        {
            string jsonString = JsonSerializer.Serialize(p);
            return jsonString;
        }

        private static Player Deserialize(string json)
        {
            Player jsonPlayer = JsonSerializer.Deserialize<Player>(json);
            return JsonSerializer.Deserialize<Player>(json);
        }

    }
}
