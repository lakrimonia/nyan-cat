using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace nyan_cat
{
    public static class ControlElements
    {
        public static Control scoreLabel = new Label
        {
            Text = "SCORE: ",
            Font = new Font("Times New Roman", 19),
            Size = new Size(105, 25),
            Location = new Point(0, 700),
            ForeColor = Color.Black
        };
        public static Control score = new Label
        {
            Font = new Font("Times New Roman", 19),
            AutoSize = true,
            Location = new Point(scoreLabel.Right, 700),
            ForeColor = Color.Black
        };
        public static Control comboLabel = new Label
        {
            Text = "COMBO:",
            Font = new Font("Times New Roman", 19),
            Size = new Size(125, 30),
            Location = new Point(score.Right + 25, 700),
            ForeColor = Color.Black
        };
        public static Control combo = new Label
        {
            Font = new Font("Times New Roman", 19),
            AutoSize = true,
            Location = new Point(comboLabel.Right, 700),
            ForeColor = Color.Black
        };
        public static Control gameOver = new Label
        {
            Font = new Font("Times New Roman", 42),
            AutoSize = true,
            Location = new Point(300, 350),
            ForeColor = Color.Red
        };
        public static Control powerUpLabel = new Label
        {
            Text = "POWER UP:",
            Font = new Font("Times New Roman", 19),
            Size = new Size(160, 30),
            Location = new Point(combo.Right + 25, 700),
            ForeColor = Color.Black
        };
        public static PictureBox powerUp = new PictureBox
        {
            Image = Image.FromFile("not_exist.png"),
            Location = new Point(powerUpLabel.Right, 690)
        };
        public static Control gemLabel = new Label
        {
            Text = "GEM:",
            Font = new Font("Times New Roman", 19),
            Size = new Size(80, 30),
            Location = new Point(powerUp.Right + 25, 700),
            ForeColor = Color.Black
        };
        public static PictureBox gem = new PictureBox
        {
            Image = Image.FromFile("not_exist.png"),
            Location = new Point(gemLabel.Right, 690)
        };
        public static List<Control> Controls = new List<Control>
        {
            scoreLabel, score, comboLabel, combo, gameOver,
            powerUpLabel, powerUp, gemLabel, gem
        };
    }
}
