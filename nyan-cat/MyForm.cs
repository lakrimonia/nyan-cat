using System.ComponentModel;
using System.Data;
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
            var timer = new Timer();
            timer.Interval = 1;
            timer.Tick += (sender, args) =>
            {
                time++;
                Invalidate();
            };
            timer.Start();

            Paint += (sender, args) =>
            {
                game.NyanCat.Draw(args.Graphics);
                foreach (var gameObject in game.GameObjects)
                    gameObject.Draw(args.Graphics);
                game.Update();
            };

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
        }
    }
}
