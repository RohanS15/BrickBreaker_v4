using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace brick_breaker_v5_roh
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isgameOver;

        int score;
        int ballx;
        int bally;
        int playerspeed;

        Random rnd = new Random();


        public Form1()
        {
            InitializeComponent();

            setupgame();

        }

        private void setupgame()
        {
            score = 0;
            ballx = 5;
            bally = 5;
            playerspeed = 12;

            txtscore.Text = "score: " + score;

            gametimer.Start();


            foreach(Control x in this.Controls)
            {
                if ( x is PictureBox && (string)x.Tag == "blocks" )
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                }
            }
        }



        private void gameOver(string message)
        {

            isgameOver = true;
            gametimer.Stop();

            txtscore.Text = "Score: " + score + " " + message;



        }





        private void maingametime(object sender, EventArgs e)
        {

            txtscore.Text = "Score: " + score;


            if(goLeft == true && player.Left > 0)
            {
                player.Left -= playerspeed;
            }

            if(goRight == true && player.Left < 700)
            {
                player.Left += playerspeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 773)
            {
                ballx = -ballx;

            }

            if (ball.Top < 0)
            {
                bally = -bally;

            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }

            }



            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {

                    if(ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;

                        this.Controls.Remove(x);

                    }

                }

            }


            if( score == 18)
            {
                gameOver("YOU WIN!!");
            }

            if(ball.Top > 820)
            {
                gameOver("YOU LOOSE!!");
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }

             if(e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

           

        }

        private void keyisup(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }


        }
    }
}
