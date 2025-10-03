using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        enum enTurn {player1, player2};
        enTurn PlayerTurn = enTurn.player1;

        enum enwinner {GameInProgress, player1, player2, Draw};
        

        Image image_X = Properties.Resources.X;
        Image image_O = Properties.Resources.O;
        Image image_Question = Properties.Resources.question_mark_96;

        struct stGameStatus
        {
            public enwinner winner;
            public bool GameOver;
            public short PlayCount;
        }
        stGameStatus GameStatus;

        void ShowGameOverMessage()
        {
              MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void EndGame()
        {
            lblTurn.Text = "Game Over";

            switch(GameStatus.winner)
            {
                case enwinner.player1:
                {
                    lblWinner.Text = "Player 1";
                    break;
                }
                case enwinner.player2:
                {
                    lblWinner.Text = "Player 2";
                    break;
                }

                default:

                    lblWinner.Text = "Draw";
                    break;
            }

            ShowGameOverMessage();

        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if(btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if(btn1.Tag.ToString() == "X")
                {
                    GameStatus.winner = enwinner.player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.winner = enwinner.player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }
        
            GameStatus.GameOver = false;
            return false;
        }

        public void CheckWinner()
        {
            //Rows
            if(CheckValues(button1, button2, button3))
                return;
           if( CheckValues(button4, button5, button6))
                return;
           if( CheckValues(button7, button8, button9))
                return;

            //Columns
            if(CheckValues(button1, button4, button7))
                return;
            if(CheckValues(button2, button5, button8))
                return;
            if(CheckValues(button3, button6, button9))
                return;

            //DiaDonals
            if(CheckValues(button1, button5, button9))
               return;
            if(CheckValues(button3, button5, button7))
               return;
        }
        public void ChangeImage(Button btn)
        {
            if(GameStatus.GameOver)
            {
                return;
            }
            else
            { 
               if(btn.Tag.ToString() == "?")
               {
                   switch(PlayerTurn)
                   {
                       case enTurn.player1:
                       {
                        btn.Image = image_X;
                        PlayerTurn = enTurn.player2;
                        lblTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                           break;
                       }
               
                       case enTurn.player2:
                       {
                         btn.Image = image_O;
                         PlayerTurn = enTurn.player1;
                         lblTurn.Text = "Player 1";
                         GameStatus.PlayCount++;
                         btn.Tag = "O";
                         CheckWinner();
                         break;
                       }
                   }
               }
            }

            if(GameStatus.PlayCount == 9 )
            {
                GameStatus.GameOver = true;
                GameStatus.winner = enwinner.Draw;
                EndGame();
            }
        }
   
        void ResetButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = image_Question;
            btn.BackColor = Color.Transparent;
        }

        void RestartGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn = enTurn.player1;
            lblTurn.Text = "Player 1";
            lblWinner.Text = "In Progress";
            GameStatus.GameOver = false;
            GameStatus.winner = enwinner.GameInProgress;
            GameStatus.PlayCount = 0;
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }



        //Buttons

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 255, 255, 255));
            pen.Width = 7;

            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(pen, 350 , 230,  850, 230);
            e.Graphics.DrawLine(pen, 350 , 380,  850, 380);
            e.Graphics.DrawLine(pen, 500, 100,  500, 500);
            e.Graphics.DrawLine(pen, 700, 100, 700, 500);
        }

    }
}
