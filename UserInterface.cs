using System;
using Chess_AI;

namespace Chess_AI
{
    public class UserInterface
    {
        private string userInput;

        public string getUserInput()
        {
            userInput = Console.ReadLine();
            return userInput;
        }

        public void writeToConsole(Dictionary<string, BoardPiece> squarePositions, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions,
            string moveFrom, string moveTo, int i = 0, bool allMoves = false, bool attacking = false, bool cantMoveThere = false, bool inTheWay = false, bool check = false)
        {
            if (allMoves)
            {
                if (check)
                {
                    Console.WriteLine($"! Cannot enter into check at {moveTo}");
                }
                else if (!cantMoveThere)
                {
                    if (attacking)
                    {
                        Console.WriteLine($" * {moveTo} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}");
                    }
                    else if (inTheWay)
                    {

                    }
                    else
                    {
                        Console.WriteLine($" * {moveTo}");
                    }

                }
            }
            else if (attacking)
            {
                Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
            }
            else if (cantMoveThere)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    Console.WriteLine($" ! Invalid move for {squarePositions[moveFrom].PieceType} at {moveFrom} to {moveTo}\nReason: {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType} is occupying square");
                }
                else
                {
                    Console.WriteLine($" ! Invalid move for {squarePositions[moveFrom].PieceType} at {moveFrom} to {moveTo}");
                }
            }
            else if (inTheWay)
            {
                Console.WriteLine($" ! {squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
            }
            else
            {
                Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
            }
        }

    }
}
