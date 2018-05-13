using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nyan_cat
{
    public partial class MyForm : Form
    {
        private Game game;

        public MyForm(Game game)
        {
            this.game = game;
            DoubleBuffered = true;
            var time = 0;
            var timer = new Timer
            {
                Interval = 1
            };
            timer.Tick += (sender, args) =>
            {
                time++;
                Invalidate();
            };
            timer.Start();

            var scoreLabel = new Label
            {
                Text = "SCORE: ",
                Font = new Font("Times New Roman", 19),
                Size = new Size(105, 25),
                Location = new Point(0, 700),
                ForeColor = Color.Black
            };
            var score = new Label
            {
                Font = new Font("Times New Roman", 19),
                AutoSize = true,
                Location = new Point(scoreLabel.Right, 700),
                ForeColor = Color.Black
            };
            var comboLabel = new Label
            {
                Text = "COMBO:",
                Font = new Font("Times New Roman", 19),
                Size = new Size(125, 30),
                Location = new Point(score.Right + 25, 700),
                ForeColor = Color.Black
            };
            var combo = new Label
            {
                Font = new Font("Times New Roman", 19),
                AutoSize = true,
                Location = new Point(comboLabel.Right, 700),
                ForeColor = Color.Black
            };
            var gameOver = new Label
            {
                Font = new Font("Times New Roman", 42),
                AutoSize = true,
                Location = new Point(300, 350),
                ForeColor = Color.Red
            };
            var powerUpLabel = new Label
            {
                Text = "POWER UP:",
                Font = new Font("Times New Roman", 19),
                Size = new Size(160, 30),
                Location = new Point(combo.Right + 25, 700),
                ForeColor = Color.Black
            };
            var powerUp = new PictureBox
            {
                Image = Image.FromFile("not_exist.png"),
                Location = new Point(powerUpLabel.Right, 690)
            };
            var gemLabel = new Label
            {
                Text = "GEM:",
                Font = new Font("Times New Roman", 19),
                Size = new Size(80, 30),
                Location = new Point(powerUp.Right + 25, 700),
                ForeColor = Color.Black
            };
            var gem = new PictureBox
            {
                Image = Image.FromFile("not_exist.png"),
                Location = new Point(gemLabel.Right, 690)
            };
            Controls.Add(scoreLabel);
            Controls.Add(score);
            Controls.Add(comboLabel);
            Controls.Add(combo);
            Controls.Add(gameOver);
            Controls.Add(powerUpLabel);
            Controls.Add(powerUp);
            Controls.Add(gemLabel);
            Controls.Add(gem);

            Paint += (sender, args) =>
            {
                score.Text = game.Score.ToString();
                combo.Text = game.Combo.ToString();
                if (game.IsOver)
                {
                    BackColor = Color.Black;
                    gameOver.Text = "GAME OVER";
                    scoreLabel.Location = new Point(380, gameOver.Bottom + 20);
                    scoreLabel.ForeColor = Color.Red;
                    score.Location = new Point(scoreLabel.Right + 20, scoreLabel.Top);
                    score.ForeColor = Color.Red;
                    gem.Image = null;
                    powerUp.Image = null;
                }
                else
                {
                    powerUp.Image = Image.FromFile(game.NyanCat.CurrentPowerUp != null
                        ? GameObjectExtensions.PowerUpImages[game.NyanCat.CurrentPowerUp.Kind]
                        : "not_exist.png");
                    gem.Image = Image.FromFile(game.NyanCat.CurrentGem != null
                        ? GameObjectExtensions.GemImages[game.NyanCat.CurrentGem.Kind]
                        : "not_exist.png");
                    game.NyanCat.Draw(args.Graphics);
                    foreach (var gameObject in game.GameObjects)
                        gameObject.Draw(args.Graphics);
                    game.Update();
                }
            };

            KeyDown += (sender, ev) =>
            {
                var jumpKeys = new Keys[3]
                {
                    Keys.Up, Keys.W, Keys.Space
                };
                if (jumpKeys.Contains(ev.KeyCode))
                    game.NyanCat.Jump();
            };

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
        }
    }
}
