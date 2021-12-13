using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day12
    {
        public class Entry
        {
            public string Name { get; set; }
            public bool MultipleVisitsAllowed { get; set; }

            public Dictionary<string,Entry> Connections { get; set; }

        }

        public void FindPaths(string beginning, string Paths, string smallCave = "")
        {
            foreach (var e in list[beginning].Connections)
            {
                if (e.Key == "end")
                {
                    paths[$"start{Paths},end"] = true;
                }
                else if(Paths.Contains("," + e.Key + ",") && e.Key==smallCave)
                {
                    FindPaths(e.Key, Paths + "," + e.Key, "");
                }
                else if (!Paths.Contains("," + e.Key + ",") || e.Value.MultipleVisitsAllowed!=false)
                {
                    FindPaths(e.Key, Paths + "," + e.Key, smallCave);
                }
            }

        }

        public Day12()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            foreach (var line in lines)
            {
                var sFrom = line.Split('-')[0];
                var sTo = line.Split('-')[1];
                Entry eFrom;
                Entry eTo;
                if (list.ContainsKey(sFrom))
                {
                    eFrom = list[sFrom];
                }
                else
                {
                    eFrom = new Entry() { Name = sFrom, MultipleVisitsAllowed = sFrom.ToUpper()==sFrom, Connections = new Dictionary<string, Entry>() };
                    list[sFrom] = eFrom;
                }
                if (list.ContainsKey(sTo))
                {
                    eTo = list[sTo];
                }
                else
                {
                    eTo = new Entry() { Name = sTo, MultipleVisitsAllowed = sTo.ToUpper()==sTo, Connections = new Dictionary<string, Entry>() };
                    list[sTo] = eTo;
                }
                if (sTo!="start") eFrom.Connections[sTo] = eTo;
                if (sFrom!="start") eTo.Connections[sFrom] = eFrom;
            }
        }

        readonly Dictionary<string, Entry> list = new();
        readonly Dictionary<string,bool> paths = new();

        public string SolvePart1()
        {

            FindPaths("start", "");

            return paths.Count.ToString();
        }

        public string SolvePart2()
        {
            list.Where(k => k.Value.MultipleVisitsAllowed==false).ToList().ForEach( smallCave => FindPaths("start", "", smallCave.Key));

            return paths.Count.ToString();
        }

        public static string tdata = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

        public static string data = @"bm-XY
ol-JS
bm-im
RD-ol
bm-QI
JS-ja
im-gq
end-im
ja-ol
JS-gq
bm-AF
RD-start
RD-ja
start-ol
cj-bm
start-JS
AF-ol
end-QI
QI-gq
ja-gq
end-AF
im-QI
bm-gq
ja-QI
gq-RD";
    }
}
