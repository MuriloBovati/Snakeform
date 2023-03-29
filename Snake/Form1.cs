using Snake.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        Game Game;
        public Form1()
        {
            InitializeComponent();
            Game = new Game(ref frame, ref lbPontos, ref pnTela);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game.startGame();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void iniciarJogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Game.startGame();
        }

        private void frame_Tick(object sender, EventArgs e)
        {
            Game.checkCollision();
            Game.tick();
        }

        private void clicado(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Game.Arrow = e.KeyCode;
            }
        }
    }
}
