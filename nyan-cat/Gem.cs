using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public enum GemKind
    {
        DoubleCombo,
        Invulnerable,
        MilkLongLife
    }

    public class Gem : IGameObject
    {
        public GemKind Kind { get; }
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public Gem(Point leftTopCorner, GemKind kind)
        {
            Kind = kind;
            Velocity = new Vector2(-1, 0);
            LeftTopCorner = leftTopCorner;
            Height = 50;
            Width = 50;
            IsAlive = true;
        }

        public void Move()
        {
            var dx = (int)Velocity.X;
            var dy = (int)Velocity.Y;
            LeftTopCorner = new Point(LeftTopCorner.X + dx,
                LeftTopCorner.Y + dy);
            IsAlive = IsAlive && LeftTopCorner.X > 0;
        }

        public void Kill() => IsAlive = false;

        public void Accelerate(Vector2 acceleration)
        {
            Velocity = new Vector2(Velocity.X + acceleration.X,
                Velocity.Y + acceleration.Y);
        }

        public void Use(Game game)
        {
            game.Score += 10000;
            game.NyanCat.CurrentGem?.Deactivate(game);
            game.NyanCat.CurrentGem = new Gem(LeftTopCorner, Kind);
            game.NyanCat.CurrentGem.Activate(game);
            Kill();
        }

        public void Activate(Game game) => GemActions.Activate[Kind](game);

        public void Deactivate(Game game) => GemActions.Deactivate[Kind](game);

        public override string ToString()
        {
            return $"{Kind} ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
