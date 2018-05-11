using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public enum PowerUpKind
    {
        TurboNyan, MilkGlasses, BigNyan, LoveNyan, FloristNyan,
        DoggieNyan, Piano
    }

    public class PowerUp : IGameObject
    {
        public PowerUpKind Kind { get; }
        public Vector2 Velocity { get; private set; }
        public Point LeftTopCorner { get; private set; }
        public int Height { get; }
        public int Width { get; }
        public bool IsAlive { get; private set; }

        public PowerUp(Point leftTopCorner, PowerUpKind kind)
        {
            Kind = kind;
            Velocity = UsualGameObjectProperties.Velocity;
            LeftTopCorner = leftTopCorner;
            Height = UsualGameObjectProperties.Height;
            Width = UsualGameObjectProperties.Width;
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
            game.NyanCat.CurrentPowerUp?.Deactivate(game);
            game.NyanCat.CurrentPowerUp = new PowerUp(LeftTopCorner, Kind);
            game.NyanCat.CurrentPowerUp.Activate(game);
            Kill();
        }

        public void Activate(Game game)
        {
            if (PowerUpActions.Activate.ContainsKey(Kind))
                PowerUpActions.Activate[Kind](game);
        }

        public void Deactivate(Game game)
        {
            if (PowerUpActions.Deactivate.ContainsKey(Kind))
                PowerUpActions.Deactivate[Kind](game);
        }

        public override string ToString()
        {
            return $"{Kind} ({LeftTopCorner.X}, {LeftTopCorner.Y})";
        }
    }
}
