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
            var map = MapCreator.CreateRandomMap(false ,true);
            foreach (var gameObject in map)
            {
                var rect = new Rectangle(gameObject.LeftTopCorner.X,
                    gameObject.LeftTopCorner.Y, gameObject.Width,
                    gameObject.Height);
                switch (gameObject)
                {
                    case Animal _:
                        graphics.DrawImage(Image.FromFile("tac-nyan.png"), rect);
                        break;
                    case UFO _:
                        graphics.DrawImage(Image.FromFile("ufo.png"), rect);
                        break;
                    case Food _:
                        graphics.DrawImage(Image.FromFile("cake.png"), rect);
                        break;
                    case Cow _:
                        graphics.DrawImage(Image.FromFile("cow.png"), rect);
                        break;
                    case Milk _:
                        graphics.DrawImage(Image.FromFile("milk.png"), rect);
                        break;
                    case Bomb _:
                        graphics.FillRectangle(Brushes.DarkRed, rect);
                        break;
                    case Platform _:
                        var file = $"p{gameObject.Width}.png";
                        graphics.DrawImage(Image.FromFile(file), rect);
                        break;
                }
            }
        }
    }
}
