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

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var map = MapCreator.CreateRandomMap(false, true);
            foreach (var gameObject in map)
                gameObject.Draw(graphics);
        }
    }
}
