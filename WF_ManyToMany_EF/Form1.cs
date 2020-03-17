using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WF_ManyToMany_EF.Models;

namespace WF_ManyToMany_EF
{
    public partial class Form1 : Form
    {
        SoccerContext db;
        public Form1()
        {
            InitializeComponent();

            db = new SoccerContext();
            db.Players.Load();
            dataGridView1.DataSource = db.Players.Local.ToBindingList();

        }


        #region Добавить

        private void button1_Click(object sender, EventArgs e)
        {
            PlayerForm playerForm = new PlayerForm();

            List<Team> teams = db.Teams.ToList();

            playerForm.listBox1.DataSource = teams;
            playerForm.listBox1.ValueMember = "Id";
            playerForm.listBox1.DisplayMember = "Name";

            DialogResult result = playerForm.ShowDialog(this);

            // отмена
            if (result == DialogResult.Cancel)
            {
                return;
            }

            Player player = new Player();
            // добавляем имя
            player.Name = playerForm.textBox1.Text;
            // добавляем возраст через поле в форме PlayerForm
            player.Age = (int)playerForm.numericUpDown1.Value;
            // очистить список и заново заполнить
            teams.Clear();
            foreach (var item in playerForm.listBox1.SelectedItems)
            {
                teams.Add((Team)item);
            }
            // присваиваем значение 
            player.Teams = teams;
            // добавляем игрока в БД
            db.Players.Add(player);
            // сохраняем изменения
            db.SaveChanges();

            MessageBox.Show("Игрок добавлен");
        }

        #endregion

        #region Изменить

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count<1)
            {
                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;

                int id = 0;

                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                {
                    return;
                }
                Player player = db.Players.Find(id);
                PlayerForm playerForm = new PlayerForm();

                playerForm.textBox1.Text = player.Name;
                playerForm.numericUpDown1.Value = player.Age;


                // получаем список команд
                List<Team> teams = db.Teams.ToList();
                playerForm.listBox1.DataSource = teams;
                playerForm.listBox1.ValueMember = "Id";
                playerForm.listBox1.DisplayMember = "Name";

                foreach (Team item in player.Teams)
                {
                    playerForm.listBox1.SelectedItem = item;
                }

                DialogResult dialogResult = playerForm.ShowDialog(this);
                // отмена
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

                player.Age = (int)playerForm.numericUpDown1.Value;

                playerForm.Name = playerForm.textBox1.Text;

                // проверить наличие команд у игрока
                foreach (var team in teams)
                {
                    if (playerForm.listBox1.SelectedItems.Contains(team))
                    {
                        // если не содержит то добавляем
                        if (!player.Teams.Contains(team))
                        {
                            player.Teams.Add(team);
                        }
                        else
                        {
                            if (player.Teams.Contains(team))
                            {
                                player.Teams.Remove(team);
                            }
                        }
                    }
                }

                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Запись была изменена");


            }
        }

        #endregion

        #region Удалить

        private void button3_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Добавить команду
        private void button4_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
