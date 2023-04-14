using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker2023
{
    public partial class GameScreen : UserControl
    {
        int score = 0;
        public static int lives = 3;
        int time = 0;

        Ball car = new ball(10, 10, 10, 10);
        List<Rectangle> bricks = new List<Rectangle>();

        Ball ball = new Ball(10, 10, 10, 10);
        List<Ball> balls = new List<Ball>();

        Player hero;
        Boolean leftArrowDown, rightArrowDown, leftArrowUp, rightArrowUp, upArrowDown, downArrowDown;

        SolidBrush greenBrush = new SolidBrush(Color.Green);

        SolidBrush redBrush = new SolidBrush(Color.Red);

        SolidBrush whiteBrush = new SolidBrush(Color.White);

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }
        }

        private void gameEngine_Tick(object sender, EventArgs e)
        {

            if (rightArrowDown && hero.x > 0)
            {
                hero.Move("right");
            }

            if (leftArrowDown && hero.x < this.Width - hero.width)
            {
                hero.Move("left");
            }

            ball.Move(this.Width, this.Height);

            foreach (Ball b in balls)
            {
                b.Move(this.Width, this.Height);
            }

            foreach (Ball b in balls)
            {
                ball.Collision(b);
            }

            foreach (Ball b in balls)
            {
                if (b.Collision(hero))
                {
                    lives++;
                    break;
                }
            }

            if (lives == 0)
            {
                gameEngine.Stop();
            }

            Refresh();
        }

        public GameScreen()
        {
            InitializeComponent();
            InitializeGame();
        }
        public void InitializeGame()
        {
            hero = new Player(300, 550);

            int x = this.Width - 30;
            int y = this.Height - 30;
            ball = new Ball(x, y, 10, 10);

            bricks.Clear();
            bricks.Add(new Rectangle(270, 270, 30, 10));
            bricks.Add(new Rectangle(250, 250, 30, 10));
            bricks.Add(new Rectangle(290, 250, 30, 10));
            bricks.Add(new Rectangle(230, 230, 30, 10));
            bricks.Add(new Rectangle(270, 230, 30, 10));
            bricks.Add(new Rectangle(310, 230, 30, 10));
            bricks.Add(new Rectangle(210, 210, 30, 10));
            bricks.Add(new Rectangle(250, 210, 30, 10));
            bricks.Add(new Rectangle(290, 210, 30, 10));
            bricks.Add(new Rectangle(330, 210, 30, 10));
            
            if (ball.Y > this.Height)
            {
                ball.X = 280;
                ball.Y = 300;

                hero.X = 280;
                hero.Y = 450;

                lives -= 1;
                livesLabel.Text = $"x{lives}";
            }
            
            for (int i = 0; i < bricks.Count(); i++)
            {
                if (ball.IntersectsWith(bricks[i]))
                {
                    bricks.RemoveAt(i);
                    ballYspeed *= -1;
                }
            }
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, hero.x, hero.y, hero.width, hero.height);
            e.Graphics.FillRectangle(redBrush, ball.x, ball.y, ball.size, ball.size);

            for (int i = 0; i < bricks.Count(); i++)
            {
                e.Graphics.FillRectangle(greenBrush, bricks[i]);
            }

            foreach (Ball b in balls)
            {
                e.Graphics.FillRectangle(redBrush, b.x, b.y, b.size, b.size);
            }
        }
    }
}
