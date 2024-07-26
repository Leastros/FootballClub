using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampionsLeague
{
    public partial class AddEditPlayer : Form
    {

        public Player Player { get; set; }

        public AddEditPlayer()
        {
            //InitializeComponent();
        }

        public AddEditPlayer(Player player)
        {
            InitializeComponent();
            this.Text = "Přidání/úprava hráče";
            foreach (FootballClub club in FootballClubInfo.Items)
            {
                cbKlub.Items.Add(FootballClubInfo.GetName(club));
            }
            if (player != null)
            {
                Player = player;
                tbJmeno.Text = Player.Name;
                cbKlub.SelectedItem = FootballClubInfo.GetName(Player.Club);
                tbPocetGolu.Text = Player.GoalCount.ToString();
            }
            else
            {
                cbKlub.SelectedIndex = 0;
                tbPocetGolu.Text = "0";
                Player = new Player("", FootballClub.None, 0);
            }

            
        }

        private void BtnStornoAction(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOkAction(object sender, EventArgs e)
        {
            Player.Name = tbJmeno.Text;

            if (tbJmeno.Text.Equals(""))
            {
                MessageBox.Show("Nebylo zadáno jméno hráče", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (cbKlub.SelectedItem.ToString().Equals(FootballClubInfo.GetName(FootballClub.FCPorto)))
                {
                    Player.Club = FootballClub.FCPorto;
                }
                else if (cbKlub.SelectedItem.ToString().Equals(FootballClubInfo.GetName(FootballClub.Arsenal)))
                {
                    Player.Club = FootballClub.Arsenal;
                }
                else if (cbKlub.SelectedItem.ToString().Equals(FootballClubInfo.GetName(FootballClub.RealMadrid)))
                {
                    Player.Club = FootballClub.RealMadrid;
                }
                else if (cbKlub.SelectedItem.ToString().Equals(FootballClubInfo.GetName(FootballClub.Chelsea)))
                {
                    Player.Club = FootballClub.Chelsea;
                }
                else if (cbKlub.SelectedItem.ToString().Equals(FootballClubInfo.GetName(FootballClub.Barcelona)))
                {
                    Player.Club = FootballClub.Barcelona;
                }
                else
                {
                    Player.Club = FootballClub.None;
                }

                if (int.TryParse(tbPocetGolu.Text, out int goals))
                {
                    Player.GoalCount = goals;
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Počet gólů musí být číslo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
