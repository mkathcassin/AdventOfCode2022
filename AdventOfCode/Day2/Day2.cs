using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    // Scissors Beats Paper
    // Paper Beats Rock
    // Rock Beats Scissors
    public enum RockPaperSciss
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
        NoValue = 0
    }

    public enum GameState
    {
        Win = 6,
        Draw = 3,
        Lose = 0
    }
    public class GameComparer : Comparer<RockPaperSciss>
    {
        public override int Compare(RockPaperSciss opHand, RockPaperSciss myHand)
        {
            //Null Value
            if (opHand == RockPaperSciss.NoValue || myHand == RockPaperSciss.NoValue)
            {
                return 0;
            }
            // Ties are 3 points
            if (opHand == RockPaperSciss.Rock && myHand == RockPaperSciss.Rock)
            {
                return 3;
            }
            if (opHand == RockPaperSciss.Paper && myHand == RockPaperSciss.Paper)
            {
                return 3;
            }
            if (opHand == RockPaperSciss.Scissors && myHand == RockPaperSciss.Scissors)
            {
                return 3;
            }
            // Wins are 6
            if (opHand == RockPaperSciss.Scissors && myHand == RockPaperSciss.Rock)
            {
                return 6;
            }
            if (opHand == RockPaperSciss.Paper && myHand == RockPaperSciss.Scissors)
            {
                return 6;
            }
            if (opHand == RockPaperSciss.Rock && myHand == RockPaperSciss.Paper)
            {
                return 6;
            }
            return 0;
        }

    }
    public class Day2
    {
        private List<(RockPaperSciss, RockPaperSciss)> parseDataPart1(string Path)
        {
            var RawData = File.ReadAllLines(Path).ToList();
            List<(RockPaperSciss, RockPaperSciss)> Game = new List<(RockPaperSciss, RockPaperSciss)>();
            foreach (var point in RawData)
            {
                var game = point.Split(" ").Select(char.Parse).ToList();
                RockPaperSciss opHand = RockPaperSciss.NoValue;
                RockPaperSciss myHand = RockPaperSciss.NoValue;
                if (game[0] == 'A')
                {
                    opHand = RockPaperSciss.Rock;
                }
                if (game[0] == 'B')
                {
                    opHand = RockPaperSciss.Paper;
                }
                if (game[0] == 'C')
                {
                    opHand = RockPaperSciss.Scissors;
                }
                if (game[1] == 'X')
                {
                    myHand = RockPaperSciss.Rock;
                }
                if (game[1] == 'Y')
                {
                    myHand = RockPaperSciss.Paper;
                }
                if (game[1] == 'Z')
                {
                    myHand = RockPaperSciss.Scissors;
                }
                Game.Add((opHand, myHand));
            }
            return Game;
        }
        private int GameResultPart1(RockPaperSciss opHand, RockPaperSciss myHand)
        {
            //Null Value
            if (opHand == RockPaperSciss.NoValue || myHand == RockPaperSciss.NoValue)
            {
                return 0;
            }
            // Ties are 3 points
            if (opHand == RockPaperSciss.Rock && myHand == RockPaperSciss.Rock)
            {
                return 3;
            }
            if (opHand == RockPaperSciss.Paper && myHand == RockPaperSciss.Paper)
            {
                return 3;
            }
            if (opHand == RockPaperSciss.Scissors && myHand == RockPaperSciss.Scissors)
            {
                return 3;
            }
            // Wins are 6
            if (opHand == RockPaperSciss.Scissors && myHand == RockPaperSciss.Rock)
            {
                return 6;
            }
            if (opHand == RockPaperSciss.Paper && myHand == RockPaperSciss.Scissors)
            {
                return 6;
            }
            if (opHand == RockPaperSciss.Rock && myHand == RockPaperSciss.Paper)
            {
                return 6;
            }
            return 0;
        }
        public void TotalPoints()
        {
            var Games = parseDataPart1("./RockPaperScissorsInput.txt");
            var totalPoints = 0;
            foreach (var Game in Games)
            {
                totalPoints = totalPoints + (int)Game.Item2;
                totalPoints = totalPoints + GameResultPart1(Game.Item1, Game.Item2);
            }
            Console.WriteLine("Total Points:" + totalPoints);
        }

        private List<(RockPaperSciss, GameState)> parseDataPart2(string Path)
        {
            var RawData = File.ReadAllLines(Path).ToList();
            List<(RockPaperSciss, GameState)> Game = new List<(RockPaperSciss, GameState)>();
            foreach (var point in RawData)
            {
                var game = point.Split(" ").Select(char.Parse).ToList();
                RockPaperSciss opHand = RockPaperSciss.NoValue;
                GameState winState = GameState.Lose;
                if (game[0] == 'A')
                {
                    opHand = RockPaperSciss.Rock;
                }
                if (game[0] == 'B')
                {
                    opHand = RockPaperSciss.Paper;
                }
                if (game[0] == 'C')
                {
                    opHand = RockPaperSciss.Scissors;
                }
                if (game[1] == 'X')
                {
                    winState = GameState.Lose;
                }
                if (game[1] == 'Y')
                {
                    winState = GameState.Draw;
                }
                if (game[1] == 'Z')
                {
                    winState = GameState.Win;
                }
                Game.Add((opHand, winState));
            }
            return Game;
        }
        public void TotalPointsPart2()
        {
            var Games = parseDataPart2("./RockPaperScissorsInput.txt");
            var totalPoints = 0;
            foreach (var game in Games)
            {
                totalPoints = totalPoints + MyHandValue(game.Item1, game.Item2) + (int)game.Item2;
            }
            Console.WriteLine(totalPoints);
        }

        private int MyHandValue(RockPaperSciss opHand, GameState result)
        {
            switch (result)
            {
                case GameState.Lose:
                    switch (opHand)
                    {
                        case RockPaperSciss.Rock:
                            return (int)RockPaperSciss.Scissors;
                        case RockPaperSciss.Scissors:
                            return (int)RockPaperSciss.Paper;
                        case RockPaperSciss.Paper:
                            return (int)RockPaperSciss.Rock;
                        default:
                            return 0;
                    }
                case GameState.Win:
                    switch (opHand)
                    {
                        case RockPaperSciss.Rock:
                            return (int)RockPaperSciss.Paper;
                        case RockPaperSciss.Scissors:
                            return (int)RockPaperSciss.Rock;
                        case RockPaperSciss.Paper:
                            return (int)RockPaperSciss.Scissors;
                        default:
                            return 0;
                    }
                case GameState.Draw:
                    return (int)opHand;
            }
            return 0;
        }
    }


}
