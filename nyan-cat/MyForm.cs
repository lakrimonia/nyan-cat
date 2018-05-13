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
                Text = "SCORE:",
                Font = new Font("Times New Roman", 24),
                AutoSize = true,
                Location = new Point(0, 700),
                ForeColor = Color.Black
            };
            var score = new Label
            {
                Font = new Font("Times New Roman", 24),
                AutoSize = true,
                Location = new Point(scoreLabel.Right + 20, 700),
                ForeColor = Color.Black
            };
            var comboLabel = new Label
            {
                Text = "COMBO:",
                Font = new Font("Times New Roman", 24),
                AutoSize = true,
                Location = new Point(score.Right + 100, 700),
                ForeColor = Color.Black
            };
            var combo = new Label
            {
                Font = new Font("Times New Roman", 24),
                AutoSize = true,
                Location = new Point(comboLabel.Right + 35, 700),
                ForeColor = Color.Black
            };
            var gameOver = new Label
            {
                Font = new Font("Times New Roman", 42),
                AutoSize = true,
                Location = new Point(300, 350),
                ForeColor = Color.Red
            };
            Controls.Add(scoreLabel);
            Controls.Add(score);
            Controls.Add(comboLabel);
            Controls.Add(combo);
            Controls.Add(gameOver);

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
                }
                else
                {
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
