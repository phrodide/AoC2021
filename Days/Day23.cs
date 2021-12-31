using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day23
    {
        public class ML
        {
            public static Dictionary<string, int> Cache = new Dictionary<string, int>();
            public int Cost { get; set; }
            public string State { get; set; }

            public bool Solved
            {
                get
                {
                    if (State==solved) return true;
                    return false;
                }
            }
            public IEnumerable<ML> PossibleMoves
            {
                get
                {
                    if (!Solved)
                    {
                        int index1 = 0;
                        foreach (var item in State)
                        {
                            if (item=='A' || item=='B' || item=='C' || item=='D')
                            {
                                //found source
                                int index1Line = index1 / 15;
                                int index1Pos = index1 % 15;
                                if ((index1Pos-3)/2 + 'A'==item)
                                {
                                    if (index1Line==5)
                                    {
                                        //GOAL! Don't move it.
                                        index1++;
                                        continue;
                                    }
                                    if (index1Line==4 && State[index1+15]==item)
                                    {
                                        //also GOAL! don't move it.
                                        index1++;
                                        continue;
                                    }
                                    if (index1Line==3 && State[index1+15]==item && State[index1+30]==item)
                                    {
                                        //also GOAL! don't move it.
                                        index1++;
                                        continue;
                                    }
                                    if (index1Line==2 && State[index1+15]==item && State[index1+30]==item && State[index1+45]==item)
                                    {
                                        //also GOAL! don't move it.
                                        index1++;
                                        continue;
                                    }
                                }
                                int index2 = 0;
                                foreach (var destination in State)
                                {
                                    if (destination=='.')
                                    {
                                        bool found = false;

                                        char[] newState = State.ToCharArray();
                                        newState[index1] = destination;
                                        newState[index2] = item;
                                        string debugState = new string(newState);
                                        int index2Line = index2 / 15;
                                        int index2Pos = index2 % 15;
                                        //destination cannot be in the hallway if the source is in the hallway.
                                        if (index1Line == index2Line && index1Line==1)
                                        {
                                            index2++;
                                            continue;
                                        }
                                        //destination cannot be to a pit not its own
                                        if (index2Line > 1 &&
                                            (((index2Pos-3)/2)+'A'!= item))
                                        {
                                            index2++;
                                            continue;
                                        }
                                        //destination cannot be mid pit
                                        if (index2Line > 1)// && newState[index2+15]!=item && newState[index2+15]!='#')
                                        {
                                            //fourth spot only valid if three,two,and one are good.
                                            if (index2Line==2 && newState[index2+15]!=item && newState[index2+30]!=item && newState[index2+45]!=item)
                                            {
                                                index2++;
                                                continue;
                                            }
                                            //third spot only valid if two and one are good.
                                            if (index2Line==3 && newState[index2+15]!=item && newState[index2+30]!=item)
                                            {
                                                index2++;
                                                continue;
                                            }
                                            //second spot only valid if one is good.
                                            if (index2Line==4 && newState[index2+15]!=item)
                                            {
                                                index2++;
                                                continue;
                                            }
                                        }
                                        //it cannot be blocked along the way.
                                        if (index2Line > 2)
                                        {
                                            int offset = 1;
                                            found = false;
                                            do
                                            {
                                                if (newState[((index2Line-offset)*15)+index2Pos] != '.')
                                                {
                                                    found = true;
                                                    break;
                                                }
                                                offset++;
                                            } while (index2Line-offset > 1);
                                            if (found)
                                            {
                                                index2++;
                                                continue;
                                            }
                                        }
                                        if (index1Line > 2)
                                        {
                                            int offset = 1;
                                            found = false;
                                            do
                                            {
                                                if (newState[((index1Line-offset)*15)+index1Pos] != '.')
                                                {
                                                    found = true;
                                                    break;
                                                }
                                                offset++;
                                            } while (index1Line-offset > 1);
                                            if (found)
                                            {
                                                index2++;

                                                continue;
                                            }
                                        }
                                        //now check the hallway
                                        int minPos = Math.Min(index1Pos, index2Pos);
                                        int maxPos = Math.Max(index1Pos, index2Pos);
                                        for (int i = minPos; i < maxPos; i++)
                                        {
                                            if (newState[15 + i] != '.' && newState[15 + i] != ',')
                                            {
                                                if (index1Line==1 && i==index1Pos)
                                                {
                                                    continue;

                                                }
                                                if (index2Line==1 && i==index2Pos)
                                                {
                                                    continue;

                                                }
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (found)
                                        {
                                            index2++;
                                            continue;

                                        }

                                        int cost = (Math.Abs(index1Pos - index2Pos) + Math.Abs((index1Line-1) + (index2Line-1))) * (item=='A' ? 1 : (item=='B' ? 10 : (item=='C' ? 100 : 1000)));
                                        if (Cost+cost > 54000)
                                        {
                                            index2++;
                                            continue;

                                        }

                                        if (Cache.ContainsKey(debugState) && Cache[debugState] < (Cost+cost))
                                        {
                                            index2++;
                                            continue;//if we've gone this path before, don't do the more expensive paths.
                                        }
                                        Cache[debugState] = Cost + cost;
                                        if (Cache.Count % 5000 == 0)
                                            Console.Write(".");
                                        //Console.WriteLine(debugState);
                                        //System.Threading.Thread.Sleep(1000);
                                        yield return new ML { State = new string(newState), Cost = Cost + cost };
                                    }
                                    index2++;
                                }
                            }
                            index1++;
                        }

                    }
                    else
                    {
                        yield return this;
                    }
                    yield break;
                }
            }
        }

        public Day23()
        {

        }

        IEnumerable<ML> PossibleMoves8Deep(ML possibleMoves, int level)
        {
            if (level <= 8) {
                return possibleMoves.PossibleMoves;
            }
            else
            {
                return new List<ML>();
            }
        }

        public string SolvePart1()
        {
            var ml = new ML() { State = data2 };
            var moves = from l1 in ml.PossibleMoves
                        from l2 in l1.PossibleMoves
                        from l3 in l2.PossibleMoves
                        from l4 in l3.PossibleMoves
                        from l5 in l4.PossibleMoves
                        from l6 in l5.PossibleMoves
                        from l7 in l6.PossibleMoves
                        from l8 in l7.PossibleMoves
                        from l9 in l8.PossibleMoves
                        from l10 in l9.PossibleMoves
                        from l11 in l10.PossibleMoves
                        from l12 in l11.PossibleMoves
                        from l13 in l12.PossibleMoves
                        from l14 in l13.PossibleMoves
                        from l15 in l14.PossibleMoves
                        from l16 in l15.PossibleMoves
                        from l17 in l16.PossibleMoves
                        from l18 in l17.PossibleMoves
                        from l19 in l18.PossibleMoves
                        from l20 in l19.PossibleMoves
                        from l21 in l20.PossibleMoves
                        from l22 in l21.PossibleMoves
                        from l23 in l22.PossibleMoves
                        from l24 in l23.PossibleMoves
                        from l25 in l24.PossibleMoves
                        from l26 in l25.PossibleMoves
                        from l27 in l26.PossibleMoves
                        from l28 in l27.PossibleMoves
                        from l29 in l28.PossibleMoves
                        from l30 in l29.PossibleMoves
                        from l31 in l30.PossibleMoves
                        where l31.Solved==true
                        orderby l31.Cost
                        select l31;
            var selected = moves.First();
            Console.WriteLine($"Cost: {selected.Cost}\r\n{selected.State}");
            return "";
        }

        public string SolvePart2()
        {
            return "";
        }

        public static string tdata = @"#############
#..,.,.,.,..#
###B#C#B#D###
  #A#D#C#A#  
  #########  ";

        public static string solved1 = @"#############
#..,.,.,.,..#
###A#B#C#D###
  #A#B#C#D#  
  #########  ";

        public static string solved = @"#############
#..,.,.,.,..#
###A#B#C#D###
  #A#B#C#D#  
  #A#B#C#D#  
  #A#B#C#D#  
  #########  ";

        public static string data = @"#############
#..,.,.,.,..#
###A#D#B#C###
  #B#C#D#A#  
  #########  ";

        public static string data2 = @"#############
#..,.,.,.,..#
###A#D#B#C###
  #D#C#B#A#  
  #D#B#A#C#  
  #B#C#D#A#  
  #########  ";
    }
}
