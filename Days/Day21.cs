using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day21
    {
        public Day21()
        {

        }

        int die = 0;
        int rolls = 0;

        int RollDice()
        {
            rolls++;
            die %= 100;
            die += 1;
            return die;
        }

        int Spot(int starting, int roll1, int roll2, int roll3)
        {
            var temp = (starting-1) + roll1 + roll2 + roll3;
            temp %= 10;
            temp += 1;
            return temp;
        }

        void dfs(int player1, int player2, int p1Score, int p2Score, bool p1turn, long pathReached, ref long player1Wins, ref long player2Wins)
        {
            if (p1turn)
            {
                for (int i = 3; i <= 9; i++)
                {
                    var iPlayer1 = Spot(player1, i, 0, 0);
                    var ip1Score = p1Score + iPlayer1;
                    if (ip1Score >= 21) player1Wins += pathReached * reference[i];
                    else dfs(iPlayer1, player2, ip1Score, p2Score, !p1turn, pathReached*reference[i], ref player1Wins, ref player2Wins);
                }
            }
            else
            {
                for (int i = 3; i <= 9; i++)
                {
                    var iPlayer2 = Spot(player2, i, 0, 0);
                    var ip2Score = p2Score + iPlayer2;
                    if (ip2Score >= 21) player2Wins += pathReached * reference[i];
                    else dfs(player1, iPlayer2, p1Score, ip2Score, !p1turn, pathReached*reference[i], ref player1Wins, ref player2Wins);
                }

            }
        }

        public string SolvePart1()
        {
            int player1 = 8;
            int player2 = 5;
            int p1Score = 0;
            int p2Score = 0;
            for (int i = 0; i < 1000; i++)
            {
                player1 = Spot(player1, RollDice(), RollDice(), RollDice());
                p1Score += player1;
                if (p1Score >= 1000)
                {
                    //game over
                    return (rolls*p2Score).ToString();
                }
                player2 = Spot(player2, RollDice(), RollDice(), RollDice());
                p2Score += player2;
                if (p2Score >= 1000)
                {
                    //game over
                    return (rolls*p1Score).ToString();
                }
            }
            return "";
        }


        Dictionary<(int Spot, int Count), int> P1Scores = new();
        Dictionary<(int Spot, int Count), int> P2Scores = new();

        Dictionary<(int starting, int roll), (int Spot, int Count)> Destination = new();
        long[] reference = new long[] { 0, 0, 0, 1, 3, 6, 7, 6, 3, 1 };
        public string SolvePart2()
        {
            //1: 3(1)
            //1: 4(3)
            //1: 5(6)
            //1: 6(7)
            //1: 7(6)
            //1: 8(3)
            //1: 9(1)
            int player1 = 8;
            int player2 = 5;
            long player1Wins = 0;
            long player2Wins = 0;
            dfs(player1, player2, 0, 0, true, 1, ref player1Wins, ref player2Wins);

            return $"{player1Wins} vs {player2Wins}";
        }

        public static string tdata = @"Player 1 starting position: 4
Player 2 starting position: 8";

        public static string data = @"Player 1 starting position: 8
Player 2 starting position: 5";
    }
}
