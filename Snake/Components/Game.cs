using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake.Components
{
    internal class Game
    {
        public Keys Direction { get; set; }

        public Keys Arrow { get; set; }

        private Timer Frame { get; set; }

        private Label LbPontuacao { get; set; }

        private Panel PnTela { get; set; }

        private int pontos = 0;

        private Food Food;

        private SnakePlayer SnakePlayer;

        private Bitmap offScreenBitmap;

        private Graphics bitmapGraph;

        private Graphics screenpGraph;

        public Game(ref Timer timer, ref Label label, ref Panel panel)
        {
            PnTela = panel;
            Frame = timer;
            LbPontuacao = label;
            offScreenBitmap = new Bitmap(428, 428);
            SnakePlayer = new SnakePlayer();
            Food = new Food();
            Direction = Keys.Left;
            Arrow = Direction;
        }

        public void startGame()
        {
            SnakePlayer.Reset();
            Food.CreateFood();
            Direction = Keys.Left;
            bitmapGraph = Graphics.FromImage(offScreenBitmap);
            screenpGraph = PnTela.CreateGraphics();
            Frame.Enabled = true;
        }

        public void tick()
        {
            if (((Arrow == Keys.Left) && (Direction != Keys.Right)) ||
                ((Arrow == Keys.Right) && (Direction != Keys.Left)) ||
                ((Arrow == Keys.Up) && (Direction != Keys.Down)) ||
                ((Arrow == Keys.Down) && (Direction != Keys.Up)))
            {
                Direction = Arrow;
            }

            switch (Direction)
            {
                case Keys.Left:
                    SnakePlayer.Left();
                    break;

                case Keys.Right:
                    SnakePlayer.Right();
                    break;

                case Keys.Up:
                    SnakePlayer.Up();
                    break;

                case Keys.Down:
                    SnakePlayer.Down();
                    break;
            }
            bitmapGraph.Clear(Color.White);

            bitmapGraph.DrawImage(Properties.Resources.food, (Food.Location.X * 15), (Food.Location.Y * 15), 15, 15);

            bool gameOverStats = false;

            for (int i = 0; i < SnakePlayer.Length; i++)
            {
                if (i == 0)
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#000000")), SnakePlayer.Location[i].X * 15, SnakePlayer.Location[i].Y * 15, 15, 15);
                }
                else
                {
                    bitmapGraph.FillEllipse(new SolidBrush(ColorTranslator.FromHtml("#4f4f4f")), SnakePlayer.Location[i].X * 15, SnakePlayer.Location[i].Y * 15, 15, 15);
                }

                if ((SnakePlayer.Location[i] == SnakePlayer.Location[0]) && (i > 0))
                {
                    gameOverStats = true;
                }
            }


            screenpGraph.DrawImage(offScreenBitmap, 0,0);
            if (gameOverStats)
            {
                gameOver();
            }



        }

        public void gameOver()
        {
            Frame.Enabled = false;
            bitmapGraph.Dispose();
            screenpGraph.Dispose();
            LbPontuacao.Text = "PONTOS: 0";
            MessageBox.Show("Game Over");
        }

        public void checkCollision()
        {
            if (SnakePlayer.Location[0] == Food.Location)
            {
                SnakePlayer.eat();
                Food.CreateFood();
                pontos += 1;
                LbPontuacao.Text = "PONTOS: " + pontos;
            }
        }
    }
}
