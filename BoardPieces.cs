using System;
using Chess_AI;

namespace Chess_AI
{
    public abstract class BoardPiece
    {
        string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h" };
        string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8" };

        protected Dictionary<string, int> allPositions;
        protected Dictionary<int, string> inverseAllPositions;

        protected UserInterface userInterface = new UserInterface();
        public BoardPiece()
        {

        }

        protected string pieceType;
        public string PieceType
        {
            get { return pieceType; }
        }

        protected string color;

        public string Color
        {
            get => color;
        }

        public virtual string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {
            return "Cant";
        }
    }

    public class Empty : BoardPiece
    {
        public Empty()
        {
            this.pieceType = "Empty";
        }
    }

    public class Pawn : BoardPiece
    {
        public bool firstMove = true;
        public Pawn(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions) {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "pawn";
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {

            if (firstMove && (allPositions[moveTo] - allPositions[moveFrom] == 1 ||
                allPositions[moveTo] - allPositions[moveFrom] == 2) && 
                squarePositions[moveTo].PieceType == "Empty")
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                if (!allMoves)
                {
                    firstMove = false;
                }
                return "Moving";
            }
            else if (allPositions[moveTo] - allPositions[moveFrom] == 1 &&
                squarePositions[moveTo].PieceType == "Empty")
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                return "Moving";
            }
            else if ((allPositions[moveTo] - allPositions[moveFrom] == 9 ||
                allPositions[moveFrom] - allPositions[moveTo] == 7) &&
                squarePositions[moveTo].Color != this.color &&
                squarePositions[moveTo].PieceType != "Empty")
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                return "Attacking";
            }
            else
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, cantMoveThere: true, allMoves: allMoves, check: check);
                //Console.WriteLine($"Invalid move for {this.pieceType} at {moveFrom}");
                return "Cant";
            }
        }
    }

    public class Rook : BoardPiece
    {
        public Rook(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions)
        {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "rook";
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {
            if (squarePositions[moveTo].Color != this.color && 
                allPositions[moveFrom] % 8 + allPositions[moveTo] - allPositions[moveFrom] < 8 &&
                allPositions[moveFrom] % 8 + allPositions[moveTo] - allPositions[moveFrom] > -1)
            {
                if (allPositions[moveTo] > allPositions[moveFrom])
                {
                    for (int i = 1; i < allPositions[moveTo] % 8 - allPositions[moveFrom] % 8; i++)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, i: i, inTheWay: true);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                else if (allPositions[moveTo] < allPositions[moveFrom])
                {
                    for (int i = -1; i > -(allPositions[moveFrom] % 8 - allPositions[moveTo] % 8); i--)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                if (squarePositions[moveTo].PieceType != "Empty" )
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (squarePositions[moveTo].Color != this.color &&
                (allPositions[moveTo] % 8) - (allPositions[moveFrom] % 8) == 0)
            {
                if (allPositions[moveTo] > allPositions[moveFrom])
                {
                    for (int i = 8; i < allPositions[moveTo] - allPositions[moveFrom]; i += 8)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, inTheWay: true, allMoves: allMoves, check: check);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                else if (allPositions[moveTo] < allPositions[moveFrom])
                {
                    for (int i = -8; i > -(allPositions[moveFrom] - allPositions[moveTo]); i -= 8)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, inTheWay: true, allMoves: allMoves, check: check);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, cantMoveThere: true, allMoves: allMoves, check: check);
                //Console.WriteLine($"Invalid move for {this.pieceType} at {moveFrom}");
                return "Cant";
            }
        }
    }

    public class Knight : BoardPiece
    {
        public Knight(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions)
        {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "knight";
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {

            if (((allPositions[moveTo] - allPositions[moveFrom] == 10 &&
                allPositions[moveFrom] % 8 < 6 &&
                allPositions[moveFrom] < 55) ||
                (allPositions[moveTo] - allPositions[moveFrom] == -6 &&
                allPositions[moveFrom] % 8 < 6 &&
                allPositions[moveFrom] > 7))
                &&
                squarePositions[moveTo].Color != this.color)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (((allPositions[moveTo] - allPositions[moveFrom] == 17 &&
                allPositions[moveFrom] % 8 < 7 &&
                allPositions[moveFrom] < 47) ||
                (allPositions[moveTo] - allPositions[moveFrom] == -15 &&
                allPositions[moveFrom] % 8 < 7 &&
                allPositions[moveFrom] > 15))
                &&
                squarePositions[moveTo].Color != this.color)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (((allPositions[moveTo] - allPositions[moveFrom] == 6 &&
                allPositions[moveFrom] % 8 > 1 &&
                allPositions[moveFrom] < 55) ||
                (allPositions[moveTo] - allPositions[moveFrom] == -10 &&
                allPositions[moveFrom] % 8 > 1 &&
                allPositions[moveFrom] > 7))
                &&
                squarePositions[moveTo].Color != this.color)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (((allPositions[moveTo] - allPositions[moveFrom] == 15 &&
                allPositions[moveFrom] % 8 > 2 &&
                allPositions[moveFrom] < 47) ||
                (allPositions[moveTo] - allPositions[moveFrom] == -17 &&
                allPositions[moveFrom] % 8 > 2 &&
                allPositions[moveFrom] > 15))
                &&
                squarePositions[moveTo].Color != this.color)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, cantMoveThere: true, allMoves: allMoves, check: check);
                //Console.WriteLine($"Invalid move for {this.pieceType} at {moveFrom}");
                return "Cant";
            }
        }

    }

    public class Bishop : BoardPiece
    {
        public Bishop(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions)
        {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "bishop";
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {
            // moving backwards
            if (allPositions[moveFrom] > allPositions[moveTo])
            {
                // moving downwards
                if ((allPositions[moveFrom] - allPositions[moveTo]) % 9 == 0 && allPositions[moveFrom] % 8 > allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = -9; i < allPositions[moveTo] - allPositions[moveFrom]; i -= 9)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                // moving upwards
                else if ((allPositions[moveFrom] - allPositions[moveTo]) % 7 == 0 && allPositions[moveFrom] % 8 < allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = -7; i > allPositions[moveTo] - allPositions[moveFrom]; i -= 7)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                else
                {
                    return "Cant";
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // moving forwards
            else if (allPositions[moveFrom] < allPositions[moveTo])
            {
                // moving downwards
                if ((allPositions[moveTo] - allPositions[moveFrom]) % 7 == 0 && allPositions[moveFrom] % 8 > allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = 7; i < allPositions[moveTo] - allPositions[moveFrom]; i += 7)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                // moving upwards
                else if ((allPositions[moveTo] - allPositions[moveFrom]) % 9 == 0 && allPositions[moveFrom] % 8 < allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = 9; i < allPositions[moveTo] - allPositions[moveFrom]; i += 9)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                else
                {
                    return "Cant";
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }

            }
            else
            {
                return "Cant";
            }
        }
    }
    public class King : BoardPiece
    {
        public King(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions)
        {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "king";
        }

        private bool avoidCheck(Dictionary<string, BoardPiece> squarePositions, string moveTo, string moveFrom)
        {
            if (squarePositions[moveTo].PieceType == "Empty")
            {
                squarePositions[moveTo] = new King(this.color, this.allPositions, this.inverseAllPositions);
                foreach (string square1 in squarePositions.Keys)
                {
                    if (squarePositions[square1].Color != this.color && squarePositions[square1].PieceType != "Empty")
                    {
                        if (squarePositions[square1].move(square1, moveTo, squarePositions: squarePositions, allMoves: true, check: true) == "Attacking")
                        {
                            squarePositions[moveTo] = new Empty();
                            return false;
                        }
                    }
                }

            }
            squarePositions[moveTo] = new Empty();
            return true;
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {
            if (check == false)
            {
                if (avoidCheck(squarePositions, moveTo, moveFrom) == false)
                {
                    return "Check";
                }
            }
            // up
            if (allPositions[moveTo] - allPositions[moveFrom] == 1 && squarePositions[moveTo].Color != this.color &&
                allPositions[moveTo] % 8 != 0)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // right
            else if ((allPositions[moveTo] - allPositions[moveFrom] == 8) && squarePositions[moveTo].Color != this.color &&
                allPositions[moveFrom] < 56)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // right up
            else if ((allPositions[moveTo] - allPositions[moveFrom] == 9) && squarePositions[moveTo].Color != this.color &&
                allPositions[moveTo] % 8 != 0 &&
                allPositions[moveFrom] < 56)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // right down
            else if ((allPositions[moveTo] - allPositions[moveFrom] == 7) && squarePositions[moveTo].Color != this.color &&
                allPositions[moveTo] % 8 != 7 &&
                allPositions[moveFrom] < 56)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // left
            else if ((allPositions[moveTo] - allPositions[moveFrom] == -8) && squarePositions[moveTo].Color != this.color
                &&
                allPositions[moveFrom] > 7)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                } 
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // left up
            else if ((allPositions[moveTo] - allPositions[moveFrom] == -7) && squarePositions[moveTo].Color != this.color
                &&
                allPositions[moveFrom] > 7 &&
                allPositions[moveTo] % 8 != 0)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // left down
            else if ((allPositions[moveTo] - allPositions[moveFrom] == -9) && squarePositions[moveTo].Color != this.color
                &&
                allPositions[moveFrom] > 7 &&
                allPositions[moveTo] % 8 != 7)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // down
            else if ((allPositions[moveTo] - allPositions[moveFrom] == -1) && squarePositions[moveTo].Color != this.color &&
                allPositions[moveTo] % 8 != 7)
            {
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            else
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, cantMoveThere: true, allMoves: allMoves, check: check);
                return "Cant";
            }
        }
    }
    public class Queen : BoardPiece
    {
        public Queen(string color, Dictionary<string, int> allPositions, Dictionary<int, string> inverseAllPositions)
        {
            this.allPositions = allPositions;
            this.inverseAllPositions = inverseAllPositions;
            this.color = color;
            this.pieceType = "queen";
        }

        public override string move(string moveFrom, string moveTo, Dictionary<string, BoardPiece> squarePositions, bool allMoves = false, bool check = false)
        {
            if (squarePositions[moveTo].Color != this.color &&
                allPositions[moveFrom] % 8 + allPositions[moveTo] - allPositions[moveFrom] < 8 &&
                allPositions[moveFrom] % 8 + allPositions[moveTo] - allPositions[moveFrom] > -1)
            {
                if (allPositions[moveTo] > allPositions[moveFrom])
                {
                    for (int i = 1; i < allPositions[moveTo] % 8 - allPositions[moveFrom] % 8; i++)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, i: i, inTheWay: true);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                else if (allPositions[moveTo] < allPositions[moveFrom])
                {
                    for (int i = -1; i > -(allPositions[moveFrom] % 8 - allPositions[moveTo] % 8); i--)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (squarePositions[moveTo].Color != this.color &&
                (allPositions[moveTo] % 8) - (allPositions[moveFrom] % 8) == 0)
            {
                if (allPositions[moveTo] > allPositions[moveFrom])
                {
                    for (int i = 8; i < allPositions[moveTo] - allPositions[moveFrom]; i += 8)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, inTheWay: true, allMoves: allMoves, check: check);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }
                else if (allPositions[moveTo] < allPositions[moveFrom])
                {
                    for (int i = -8; i > -(allPositions[moveFrom] - allPositions[moveTo]); i -= 8)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, inTheWay: true, allMoves: allMoves, check: check);
                            //Console.WriteLine($"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].Color} " +
                            //    $"{squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType} in the way"
                            //    + $" at {inverseAllPositions[allPositions[moveFrom] + i]}");
                            return "InTheWay";
                        }
                    }
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {squarePositions[moveTo].Color} {squarePositions[moveTo].PieceType}: {moveTo}");
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    //Console.WriteLine($"{squarePositions[moveFrom].Color} {squarePositions[moveFrom].PieceType}: {moveFrom} -> {moveTo}");
                    return "Moving";
                }
            }
            else if (allPositions[moveFrom] > allPositions[moveTo])
            {
                // moving downwards
                if ((allPositions[moveFrom] - allPositions[moveTo]) % 9 == 0 && allPositions[moveFrom] % 8 > allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = -9; i < allPositions[moveTo] - allPositions[moveFrom]; i -= 9)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                // moving upwards
                else if ((allPositions[moveFrom] - allPositions[moveTo]) % 7 == 0 && allPositions[moveFrom] % 8 < allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = -7; i > allPositions[moveTo] - allPositions[moveFrom]; i -= 7)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                else
                {
                    return "Cant";
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            // moving forwards
            else if (allPositions[moveFrom] < allPositions[moveTo])
            {
                // moving downwards
                if ((allPositions[moveTo] - allPositions[moveFrom]) % 7 == 0 && allPositions[moveFrom] % 8 > allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = 7; i < allPositions[moveTo] - allPositions[moveFrom]; i += 7)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                // moving upwards
                else if ((allPositions[moveTo] - allPositions[moveFrom]) % 9 == 0 && allPositions[moveFrom] % 8 < allPositions[moveTo] % 8 && squarePositions[moveTo].Color != this.color)
                {
                    for (int i = 9; i < allPositions[moveTo] - allPositions[moveFrom]; i += 9)
                    {
                        if (squarePositions[inverseAllPositions[allPositions[moveFrom] + i]].PieceType != "Empty")
                        {
                            userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check, inTheWay: true);
                            return "InTheWay";
                        }
                    }
                }
                else
                {
                    return "Cant";
                }

                if (squarePositions[moveTo].PieceType != "Empty")
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, attacking: true, allMoves: allMoves, check: check);
                    return "Attacking";
                }
                else
                {
                    userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, allMoves: allMoves, check: check);
                    return "Moving";
                }
            }
            else
            {
                userInterface.writeToConsole(squarePositions, allPositions, inverseAllPositions, moveFrom, moveTo, cantMoveThere: true, allMoves: allMoves, check: check);
                //Console.WriteLine($"Invalid move for {this.pieceType} at {moveFrom}");
                return "Cant";
            } 
        }
    }

}
