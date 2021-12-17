using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day17
    {
        public record Point(int x, int y);

        public Day17()
        {

        }

        public Point ApplyRules(Point inbound, ref Point velocity)
        {
            Point initialVelocity = velocity;
            velocity = new Point(velocity.x > 0 ? velocity.x-1 : velocity.x < 0 ? velocity.x+1 : 0, velocity.y-1);
            return new Point(inbound.x + initialVelocity.x, inbound.y + initialVelocity.y);
        }

        public string SolvePart1()
        {
            var points = data.Replace("target area: x=", "").Replace(" y=", "").Split(',');
            var xpoint1 = int.Parse(points[0].Split('.')[0]);
            var xpoint2 = int.Parse(points[0].Split('.')[2]);
            var ypoint1 = int.Parse(points[1].Split('.')[0]);
            var ypoint2 = int.Parse(points[1].Split('.')[2]);
            var gMaxY = 0;

            int x = 0;
            for (int i = 1; i < Math.Max(xpoint1,xpoint2); i++)
            {
                x += i;
                if (xpoint1 <= x && xpoint2 >= x)
                {
                    x = i;
                    break;
                }
            }
            for (int y = 0; y < 200; y++)
            {
                bool hitTarget = false;
                Point testvelocity = new(x, y);
                Point probe = new(0, 0);
                var MaxY = 0;
                while (probe.y >= Math.Min(ypoint1, ypoint2))
                {
                    probe = ApplyRules(probe, ref testvelocity);
                    MaxY = Math.Max(MaxY, probe.y);
                    if (probe.x >= xpoint1 && probe.x <= xpoint2 && probe.y >= ypoint1 && probe.y <= ypoint2)
                    {
                        hitTarget = true;
                        break;
                    }
                }
                if (hitTarget && gMaxY < MaxY)
                {
                    gMaxY = MaxY;
                }

            }

            return gMaxY.ToString();
        }

        public string SolvePart2()
        {
            var points = data.Replace("target area: x=", "").Replace(" y=", "").Split(',');
            var xpoint1 = int.Parse(points[0].Split('.')[0]);
            var xpoint2 = int.Parse(points[0].Split('.')[2]);
            var ypoint1 = int.Parse(points[1].Split('.')[0]);
            var ypoint2 = int.Parse(points[1].Split('.')[2]);
            var targetsHit = 0;

            for (int x = 0; x <= xpoint2; x++)
            {
                for (int y = Math.Min(ypoint1,ypoint2); y < 200; y++)
                {
                    Point testvelocity = new(x, y);
                    Point probe = new(0, 0);
                    while (probe.x <= Math.Max(xpoint1, xpoint2) && probe.y >= Math.Min(ypoint1, ypoint2))
                    {
                        probe = ApplyRules(probe, ref testvelocity);
                        if (probe.x >= xpoint1 && probe.x <= xpoint2 && probe.y >= ypoint1 && probe.y <= ypoint2)
                        {
                            targetsHit++;
                            break;
                        }
                    }
                }

            }

            return targetsHit.ToString();
        }

        public static string tdata = "target area: x=20..30, y=-10..-5";

        public static string data = "target area: x=155..215, y=-132..-72";
    }
}
