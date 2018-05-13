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
            foreach (var control in ControlElements.Controls)
                Controls.Add(control);
            Paint += (sender, args) =>
            {
                ControlElements.score.Text = game.Score.ToString();
                ControlElements.combo.Text = game.Combo.ToString();
                if (game.IsOver)
                {
                    BackColor = Color.Black;
                    ControlElements.gameOver.Text = "GAME OVER";
                    ControlElements.scoreLabel.Location = new Point(380, ControlElements.gameOver.Bottom + 20);
                    ControlElements.scoreLabel.ForeColor = Color.Red;
                    ControlElements.score.Location = new Point(ControlElements.scoreLabel.Right + 20, ControlElements.scoreLabel.Top);
                    ControlElements.score.ForeColor = Color.Red;
                    ControlElements.gem.Image = null;
                    ControlElements.powerUp.Image = null;
                }
                else
                {
                    ControlElements.powerUp.Image = Image.FromFile(game.NyanCat.CurrentPowerUp != null
                        ? GameObjectExtensions.PowerUpImages[game.NyanCat.CurrentPowerUp.Kind]
                        : "not_exist.png");
                    ControlElements.gem.Image = Image.FromFile(game.NyanCat.CurrentGem != null
                        ? GameObjectExtensions.GemImages[game.NyanCat.CurrentGem.Kind]
                        : "not_exist.png");
                    game.NyanCat.Draw(game, args.Graphics);
                    foreach (var gameObject in game.GameObjects)
                        gameObject.Draw(game, args.Graphics);
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
