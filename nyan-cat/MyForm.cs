using System;
using System.Collections.Generic;
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

            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            //graphics.DrawImage(Image.FromFile("nyan-cat.png"), new Point(0, 0));
            foreach (var obj in game.GameObjects)
            {
                if (obj is Platform)
                {
                    var platform = obj as Platform;
                    var left = platform.LeftTopCorner;
                    for (var i = left.X; i < left.X + platform.Width + 1; i++)
                        graphics.DrawImage(Image.FromFile("platform.png"), new Point(i, left.Y));
                }
                var leftCorner = obj.LeftTopCorner;
                if (obj is Cow)
                    graphics.DrawImage(Image.FromFile("cow.png"), leftCorner);
                else if (obj is Milk)
                {
                    graphics.DrawImage(Image.FromFile("milk.png"), leftCorner);
                }
                else if (obj is Gem)
                {
                    var gem = obj as Gem;
                    string file = null;
                    if (gem.Kind == GemKind.DoubleCombo)
                        file = "red-gem.png";
                    if (gem.Kind == GemKind.Invulnerable)
                        file = "green-gem.png";
                    if (gem.Kind == GemKind.MilkLongLife)
                        file = "grey-gem.png";
                    graphics.DrawImage(Image.FromFile(file), leftCorner);
                }
                else if (obj is Food)
                    graphics.DrawImage(Image.FromFile("cake.png"), leftCorner);
            }
        }
    }
}
