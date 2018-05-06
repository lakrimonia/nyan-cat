using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nyan_cat
{
    public class DogPathfinder
    {

        private class DijkstraData
        {
            public Platform Previous { get; set; }
            public double Price { get; set; }
        }

        private static readonly Platform platformDefault =
            new Platform(new Point(0, 0), 0);

        private Platform end;

        public List<Platform> FindPath(Game game, Platform start)
        {
            var finish = GetPlatformUnderCat(game);
            var notVisited = new List<Platform>(GetPlatforms(game));
            var platforms = GetPlatforms(game);
            var track = new Dictionary<Platform, DijkstraData>
            {
                [start] = new DijkstraData { Price = 0, Previous = platformDefault }
            };

            while (true)
            {
                var toOpen = ChooseToOpen(notVisited, track);
                if (toOpen == platformDefault)
                    return new List<Platform>();
                if (toOpen == finish)
                {
                    end = toOpen;
                    break;
                }
                var directions = GetDirections(game, toOpen);
                UpdateTrack(track, toOpen, directions);
                notVisited.Remove(toOpen);
            }
            return GetResult(track);
        }

        private static void UpdateTrack(Dictionary<Platform, DijkstraData> track,
            Platform toOpen, List<Platform> directions)
        {
            foreach (var point in directions)
            {
                var currentPrice = track[toOpen].Price + GetPrice(toOpen, point);
                var nextNode = point;
                if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
                {
                    track[nextNode] = new DijkstraData { Previous = toOpen,
                        Price = currentPrice };
                }
            }
        }

        static private int GetPrice(Platform start, Platform end)
        {
            return 0;
        }

        private List<Platform> GetResult(Dictionary<Platform, DijkstraData> track)
        {
            var result = new List<Platform>();
            while (end != platformDefault)
            {
                result.Add(end);
                end = track[end].Previous;
            }
            result.Reverse();
            return result.Skip(1).ToList();
        }

        private static Platform ChooseToOpen(List<Platform> notVisited,
            Dictionary<Platform, DijkstraData> track)
        {
            var toOpen = platformDefault;
            var bestPrice = double.PositiveInfinity;
            foreach (var e in notVisited)
            {
                if (track.ContainsKey(e) && track[e].Price < bestPrice)
                {
                    bestPrice = track[e].Price;
                    toOpen = e;
                }
            }
            return toOpen;
        }

        public Platform GetPlatformUnderCat(Game game)
        {
            return null;
        }

        public List<Platform> GetPlatforms(Game game)
        {
            return game.GameObjects
                .Where(gObj => gObj is Platform)
                .Select(p => (Platform)p)
                .ToList();
        }

        public List<Platform> GetDirections(Game game, Platform platform)
        {

            return null;
        }
    }
}
