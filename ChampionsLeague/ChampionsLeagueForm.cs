using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChampionsLeagueLibrary;

namespace ChampionsLeague
{
    public partial class ChampionsLeagueForm : Form
    {

        private PlayersRecords playersRecords = new PlayersRecords();
        private int change = 1;


        public ChampionsLeagueForm()
        {
            InitializeComponent();
            playersRecords.PlayersCountChanged += Handler;
            this.Text = "Liga mistrů";
        }

        private void Handler(object sender, PlayersCountChangedEventArgs e)
        {
            if (change == 1)
            {
                listBoxLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + " | Změna počtu hráčů z " + e.OldCount + " na " + e.NewCount);
            }
            change = 1;
        }

        private void BtnUpravitHrace(object sender, EventArgs e)
        {
            if (listBoxMain.SelectedIndex > -1)

            {
                int selected = listBoxMain.SelectedIndex;
                Player player = null;


                AddEditPlayer form = new AddEditPlayer(player);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    change = 0;
                    playersRecords.Delete(listBoxMain.SelectedIndex);
                    listBoxMain.Items.RemoveAt(listBoxMain.SelectedIndex);
                    change = 0;
                    playersRecords.Add(form.Player);
                    listBoxMain.Items.Add(form.Player.Name + "\t\t" + FootballClubInfo.GetName(form.Player.Club) + "\t\t" + form.Player.GoalCount);
                }
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný hráč. ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPridatHrace(object sender, EventArgs e)
        {
            AddEditPlayer form = new AddEditPlayer(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                playersRecords.Add(form.Player);
                listBoxMain.Items.Add(form.Player.Name + "\t\t" + FootballClubInfo.GetName(form.Player.Club) + "\t\t" + form.Player.GoalCount);
            }
        }

        private void BtnOdebratHrace(object sender, EventArgs e)
        {
            if (listBoxMain.SelectedIndex > -1)
            {
                playersRecords.Delete(listBoxMain.SelectedIndex);
                listBoxMain.Items.RemoveAt(listBoxMain.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Nebyl vybrán žádný hráč. ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNejlepsiKluby(object sender, EventArgs e)
        {
            if (listBoxMain.Items.Count > 0)
            {
                bool count = playersRecords.FindBestClubs(out FootballClub[] clubs, out int goalCount);

                if (count)
                {
                    TopClubsForm form = new TopClubsForm(clubs, goalCount);
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Seznam klubů nelze zobrazit, chybí data.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "(*.players)|*.players";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PlayersFileSerializerDeserializer serializer = new PlayersFileSerializerDeserializer(playersRecords, dialog.FileName);
                serializer.Save();
            }
        }

        private void BtnLoad(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(*.players)|*.players";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                PlayersFileSerializerDeserializer serializer = new PlayersFileSerializerDeserializer(playersRecords, dialog.FileName);
                serializer.Load();

                listBoxMain.Items.Clear();

                for (int i = 0; i < playersRecords.Count; i++)
                {
                    Player p = playersRecords[i];
                    listBoxMain.Items.Add(p.Name + "\t\t" + FootballClubInfo.GetName(p.Club) + "\t\t" + p.GoalCount);
                }

            }
        }
    }
}
