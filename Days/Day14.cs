using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day14
    {
        public Day14()
        {

        }



        public string SolvePart1()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            string startingPoint = data.Split('\r')[0];
            Dictionary<string,string> map = new Dictionary<string,string>();
            foreach (var item in data.Replace("\r\n","\n").Split('\n'))
            {
                if (item.Trim() == startingPoint)
                    continue;
                if (item.Trim().Length==0)
                    continue;
                var mapItems = item.Replace(" -> ", "~").Split('~');
                map[mapItems[0]] = mapItems[0].Insert(1,mapItems[1]).Substring(1);
            }
            for (int i = 0; i < 10; i++)
            {
                stringBuilder.Append(startingPoint[0]);
                for (int j = 0; j < startingPoint.Length-1; j++)
                {
                    var reference = startingPoint.Substring(j, 2);
                    var insert = map[reference];
                    stringBuilder.Append(insert);
                }
                startingPoint = stringBuilder.ToString();
                stringBuilder = new System.Text.StringBuilder();
                Console.WriteLine(startingPoint);
            }
            var lens = startingPoint.GroupBy(s => s).Select(g => (g.Key, g.Count())).OrderBy(x => x.Item2);
            int least = lens.First().Item2;
            int most = lens.Last().Item2;

            return (most-least).ToString();
        }

        void TryAdd(Dictionary<string,long> g, string key, long count)
        {
            if (!g.ContainsKey(key))
            {
                g[key] = 0;
            }
            g[key] += count;
        }

        public string SolvePart2()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            string startingPoint = data.Split('\r')[0];
            Dictionary<string, string> map = new Dictionary<string, string>();
            Dictionary<string,long> genome = new Dictionary<string, long>();
            Dictionary<string, long> genome2 = new Dictionary<string, long>();
            foreach (var item in data.Replace("\r\n", "\n").Split('\n'))
            {
                if (item.Trim() == startingPoint)
                    continue;
                if (item.Trim().Length==0)
                    continue;
                var mapItems = item.Replace(" -> ", "~").Split('~');
                map[mapItems[0]] = mapItems[0].Insert(1, mapItems[1]);
            }
            for (int j = 0; j < startingPoint.Length-1; j++)
            {
                var reference = startingPoint.Substring(j, 2);
                if (!genome.ContainsKey(reference))
                {
                    genome[reference] = 0;
                }
                genome[reference] += 1;
            }

            for (int i = 0; i < 40; i++)
            {
                foreach (var item in genome)
                {
                    TryAdd(genome2, map[item.Key].Substring(0, 2), genome[item.Key]);
                    TryAdd(genome2, map[item.Key].Substring(1, 2), genome[item.Key]);
                }
                genome = genome2;
                genome2 = new Dictionary<string, long>();
            }
            Dictionary<char, long> values = new Dictionary<char, long>();
            foreach (var item in genome)
            {
                if (!values.ContainsKey(item.Key[1]))
                {
                    values[item.Key[1]] = 0;
                }
                values[item.Key[1]] += item.Value;
            }
            values[startingPoint[0]] += 1;
            var lens = values.OrderBy(x => x.Value);
            long least = lens.First().Value;
            long most = lens.Last().Value;

            return (most-least).ToString();

        }

        public static string tdata = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

        public static string data = @"38519085727749079372

19 -> 0
08 -> 7
57 -> 7
95 -> 8
38 -> 2
05 -> 7
52 -> 8
49 -> 3
42 -> 5
73 -> 6
65 -> 3
82 -> 9
20 -> 5
70 -> 3
31 -> 7
26 -> 5
53 -> 6
64 -> 8
83 -> 7
47 -> 7
67 -> 3
09 -> 0
07 -> 7
44 -> 2
37 -> 7
28 -> 0
85 -> 0
68 -> 3
86 -> 8
87 -> 4
14 -> 1
60 -> 0
21 -> 6
30 -> 6
62 -> 2
93 -> 6
25 -> 2
72 -> 4
17 -> 1
63 -> 0
11 -> 2
96 -> 5
04 -> 5
24 -> 3
91 -> 6
40 -> 7
34 -> 5
75 -> 4
22 -> 1
94 -> 1
13 -> 6
58 -> 9
79 -> 1
27 -> 6
71 -> 8
97 -> 9
10 -> 1
48 -> 4
18 -> 3
06 -> 1
41 -> 5
54 -> 1
77 -> 4
16 -> 2
12 -> 3
59 -> 5
90 -> 7
80 -> 9
03 -> 0
15 -> 8
74 -> 9
89 -> 3
29 -> 4
01 -> 6
88 -> 4
99 -> 1
00 -> 9
23 -> 4
51 -> 3
69 -> 1
39 -> 9
45 -> 7
98 -> 7
33 -> 7
46 -> 6
36 -> 3
02 -> 5
84 -> 9
50 -> 2
43 -> 1
76 -> 6
81 -> 3
92 -> 1
35 -> 8
55 -> 6
32 -> 3
66 -> 7
61 -> 9
78 -> 8
56 -> 8";
    }
}
