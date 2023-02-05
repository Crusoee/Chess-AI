using System;
using System.Security.Cryptography.X509Certificates;
using Chess_AI;

namespace Chess_AI
{
    public class Board
    {
        private string color;
        private string color1;
        private string color2;

        UserInterface userInterface = new UserInterface();

        string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h" };
        string[] numbers = { "1", "2", "3", "4", "5", "6", "7", "8" };

        // the squarePositions dictionary will help me find out which player is on which square
        public Dictionary<string, BoardPiece> squarePositions = new Dictionary<string, BoardPiece>();
        public Dictionary<string, BoardPiece> squarePositionsInverse = new Dictionary<string, BoardPiece>();

        public Dictionary<string, int> allPositions = new Dictionary<string, int>();
        public Dictionary<int, string> inverseAllPositions = new Dictionary<int, string>();
        public Dictionary<string, int> allPositions2 = new Dictionary<string, int>();
        public Dictionary<int, string> inverseAllPositions2 = new Dictionary<int, string>();

        List<string> kingCantMoveHere = new List<string>();
        List<string> kingCantMoveHereTemp = new List<string>();


        public Board(string color1, string color2)
            // setting up the board
        {
            this.color = color1;
            this.color1 = color1;
            this.color2 = color2;

            foreach (string a in alphabet)
            {
                foreach (string n in numbers)
                {
                    squarePositions.Add(a + n, new Empty());
                }
            }

            foreach (string a in alphabet.Reverse())
            {
                foreach (string n in numbers.Reverse())
                {
                    squarePositionsInverse.Add(a + n, new Empty());
                }
            }

            int cntr = -1;
            foreach (string a in alphabet)
            {
                foreach (string n in numbers)
                {
                    cntr += 1;
                    allPositions.Add(a + n, cntr);
                    inverseAllPositions.Add(cntr, a + n);
                }
            }

            cntr = -1;
            foreach (string a in alphabet.Reverse())
            {
                foreach (string n in numbers.Reverse())
                {
                    cntr += 1;
                    allPositions2.Add(a + n, cntr);
                    inverseAllPositions2.Add(cntr, a + n);
                }
            }

            // setting up white board pieces

            squarePositions["a1"] = (new Rook("white", allPositions, inverseAllPositions));
            //squarePositions["h1"] = (new Rook("white", allPositions, inverseAllPositions));
            //squarePositions["b1"] = (new Knight("white", allPositions, inverseAllPositions));
            //squarePositions["g1"] = (new Knight("white", allPositions, inverseAllPositions));
            //squarePositions["c1"] = (new Bishop("white", allPositions, inverseAllPositions));
            squarePositions["f1"] = (new Bishop("white", allPositions, inverseAllPositions));
            squarePositions["e1"] = (new King("white", allPositions, inverseAllPositions));
            squarePositions["d1"] = (new Queen("white", allPositions, inverseAllPositions));
            //squarePositions["a2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["b2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["c2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["d2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["e2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["f2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["g2"] = (new Pawn("white", allPositions, inverseAllPositions));
            //squarePositions["h2"] = (new Pawn("white", allPositions, inverseAllPositions));

            // setting up black board pieces

            squarePositions["a8"] = (new Rook("black", allPositions2, inverseAllPositions2));
            //squarePositions["h8"] = (new Rook("black", allPositions2, inverseAllPositions2));
            //squarePositions["b8"] = (new Knight("black", allPositions2, inverseAllPositions2));
            //squarePositions["g8"] = (new Knight("black", allPositions2, inverseAllPositions2));
            squarePositions["c8"] = (new Bishop("black", allPositions2, inverseAllPositions2));
            //squarePositions["f8"] = (new Bishop("black", allPositions2, inverseAllPositions2));
            squarePositions["e8"] = (new King("black", allPositions2, inverseAllPositions2));
            squarePositions["d8"] = (new Queen("black", allPositions2, inverseAllPositions2));
            //squarePositions["a7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["b7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["c7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["d7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["e7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["f7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["g7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
            //squarePositions["h7"] = (new Pawn("black", allPositions2, inverseAllPositions2));
        }

        public void commands(string selectedPiece)
        {
            if (selectedPiece == "/all my pieces")
            {
                foreach (string key in squarePositions.Keys)
                {
                    if (squarePositions[key].Color == color)
                    {
                        Console.WriteLine($"{squarePositions[key].PieceType} at {key}");
                    }
                }
            }
            else if (selectedPiece == "/help")
            {
                Console.WriteLine("/all my pieces -> shows squares your uncaptured pieces currently occupy\n" +
                    "/all their pieces -> shows squares the enemy's uncaptured pieces currently occupy\n" +
                    "/all pieces -> shows squares all uncaptured pieces currently occupy\n");
            }
            else if (selectedPiece == "/all their pieces")
            {
                foreach (string key in squarePositions.Keys)
                {
                    if (squarePositions[key].Color != color && squarePositions[key].PieceType != "Empty")
                    {
                        Console.WriteLine($"{squarePositions[key].Color} {squarePositions[key].PieceType} at {key}");
                    }
                }
            }
            else if (selectedPiece == "/all pieces")
            {
                foreach (string key in squarePositions.Keys)
                {
                    if (squarePositions[key].Color == color)
                    {
                        Console.WriteLine($"{squarePositions[key].Color} {squarePositions[key].PieceType} at {key}");
                    }
                }
                foreach (string key in squarePositions.Keys)
                {
                    if (squarePositions[key].Color != color && squarePositions[key].PieceType != "Empty")
                    {
                        Console.WriteLine($"{squarePositions[key].Color} {squarePositions[key].PieceType} at {key}");
                    }
                }
            }
            else
            {
                Console.WriteLine(" ! Invalid command");
            }
        }

        public void commands2(string moveTo, string selectedPiece)
        {
            if (moveTo == "/all moves")
            {
                Console.WriteLine($"All possible moves for {squarePositions[selectedPiece].PieceType}: ");
                allPossibleMoves(selectedPiece, selectedPiece);
            }
            else if (moveTo == "/captures")
            {
                Console.WriteLine($"Has {squarePositions[selectedPiece].captureCount} captures!");
            }
            else if (moveTo == "/moves")
            {
                Console.WriteLine($"Has {squarePositions[selectedPiece].movementCount} moves! ");
            }
            else if (moveTo == "/help")
            {
                Console.WriteLine("/all moves -> shows squares selected board piece may advance to");
                Console.WriteLine("/captures -> shows all captures for selected piece");
                Console.WriteLine("/moves -> shows all moves for selected piece");
            }
            else
            {
                Console.WriteLine(" ! Invalid command");
            }
        }

        public void movePiece()
        {
            bool inCheck = false;
            string kingsSquare = "";
            foreach (string key in squarePositions.Keys)
            {
                if (squarePositions[key].PieceType == "king" && squarePositions[key].Color == this.color1 && kingCantMoveHere.Contains(key))
                {
                    kingsSquare = key;
                    Console.WriteLine($" ! Your king is in check at {key}!");
                    inCheck = true;
                    break;
                }
            }
            bool finishedTurn = false;
            Console.WriteLine($"{this.color}'s turn");
            while (!finishedTurn)
            {
                string selectedPiece;

                Console.WriteLine(" - Select a board piece:");

                selectedPiece = userInterface.getUserInput();

                if (selectedPiece.Contains('/'))
                {
                    commands(selectedPiece);
                }
                else if (squarePositions.ContainsKey(selectedPiece))
                {
                    if (squarePositions[selectedPiece].PieceType == "Empty")
                    {
                        Console.WriteLine($"Empty square at {selectedPiece}");
                    }
                    else if (squarePositions[selectedPiece].Color != color)
                    {
                        Console.WriteLine($"{squarePositions[selectedPiece].Color} {squarePositions[selectedPiece].PieceType} at {selectedPiece}");
                    }
                    else
                    {
                        Console.WriteLine($"{squarePositions[selectedPiece].Color} {squarePositions[selectedPiece].PieceType} selected");

                        Console.WriteLine(" - Enter valid move:");
                        while (true)
                        {
                            string moveTo = userInterface.getUserInput();
                            if (moveTo.Contains('/'))
                            {
                                commands2(moveTo, selectedPiece);
                            }
                            else if (squarePositions[selectedPiece].PieceType == "king" && kingCantMoveHere.Contains(moveTo))
                            {
                                Console.WriteLine($" ! Invalid move for {squarePositions[selectedPiece].PieceType}: {selectedPiece} -> {moveTo}\n"
                                    + "Reason: In check!");
                                break;
                            }
                            else if (squarePositions.ContainsKey(moveTo))
                            {
                                if (!inCheck)
                                {
                                    string mayMove = squarePositions[selectedPiece].move(selectedPiece, moveTo, squarePositions);
                                    if (mayMove == "Attacking" || mayMove == "Moving")
                                    {
                                        squarePositions[moveTo] = squarePositions[selectedPiece];
                                        squarePositions[selectedPiece] = new Empty();
                                        finishedTurn = true;
                                    }
                                    break;
                                }
                                else {
                                    var newBoard = squarePositions.ToDictionary(entry => entry.Key, entry => entry.Value);
                                    string mayMove = squarePositions[selectedPiece].move(selectedPiece, moveTo, newBoard);
                                    if (mayMove == "Attacking" || mayMove == "Moving")
                                    {
                                        Console.WriteLine(moveTo);
                                        newBoard[moveTo] = newBoard[selectedPiece];
                                        newBoard[selectedPiece] = new Empty();
                                    }
                                    else {
                                        break;
                                    }
                                    kingCantMoveHereTemp = avoidCheck(newBoard, color1, color2, allPositions, inverseAllPositions);
                                    if (kingCantMoveHereTemp.Contains(kingsSquare)) {
                                        Console.WriteLine($"King is still in check at {kingsSquare}");
                                        break;
                                    }
                                    else {
                                        squarePositions[moveTo] = squarePositions[selectedPiece];
                                        squarePositions[selectedPiece] = new Empty();
                                        finishedTurn = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(" ! Deselected");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" ! Invalid square");
                }
            }

            kingCantMoveHere = avoidCheck(squarePositions, color2, color1, allPositions, inverseAllPositions);

            if (this.color == color1)
            {
                this.color = color2;
            }
            else if (this.color == color2)
            {
                this.color = color1;
            }
            else
            {
                Console.WriteLine(" ! Error? Color Mismatch? ");
            }


            Console.WriteLine("______________________________________");
        }

        public void movePiece2()
        {
            bool inCheck = false;
            string kingsSquare = "";
            foreach (string key in squarePositionsInverse.Keys)
            {
                if (squarePositionsInverse[key].PieceType == "king" && squarePositionsInverse[key].Color == this.color2 && kingCantMoveHere.Contains(key))
                {
                    kingsSquare = key;
                    Console.WriteLine($" ! Your king is in check at {key}!");
                    inCheck = true;
                    break;
                }
            }
            bool finishedTurn = false;
            Console.WriteLine($"{this.color}'s turn");
            while (!finishedTurn)
            {
                string selectedPiece;

                Console.WriteLine(" - Select a board piece:");

                selectedPiece = userInterface.getUserInput();

                if (selectedPiece.Contains('/'))
                {
                    commands(selectedPiece);
                }
                else if (squarePositionsInverse.ContainsKey(selectedPiece))
                {
                    if (squarePositionsInverse[selectedPiece].PieceType == "Empty")
                    {
                        Console.WriteLine($"Empty square at {selectedPiece}");
                    }
                    else if (squarePositionsInverse[selectedPiece].Color != color)
                    {
                        Console.WriteLine($"{squarePositionsInverse[selectedPiece].Color} {squarePositionsInverse[selectedPiece].PieceType} at {selectedPiece}");
                    }
                    else
                    {
                        Console.WriteLine($"{squarePositionsInverse[selectedPiece].Color} {squarePositionsInverse[selectedPiece].PieceType} selected");

                        Console.WriteLine(" - Enter valid move:");
                        while (true)
                        {
                            string moveTo = userInterface.getUserInput();
                            if (moveTo.Contains('/'))
                            {
                                commands2(moveTo, selectedPiece);
                            }
                            else if (squarePositionsInverse[selectedPiece].PieceType == "king" && kingCantMoveHere.Contains(moveTo))
                            {
                                Console.WriteLine($" ! Invalid move for {squarePositionsInverse[selectedPiece].PieceType}: {selectedPiece} -> {moveTo}\n"
                                    + "Reason: In check!");
                                break;
                            }
                            else if (squarePositionsInverse.ContainsKey(moveTo))
                            {
                                if (!inCheck)
                                {
                                    string mayMove = squarePositionsInverse[selectedPiece].move(selectedPiece, moveTo, squarePositionsInverse);
                                    if (mayMove == "Attacking" || mayMove == "Moving")
                                    {
                                        squarePositionsInverse[moveTo] = squarePositionsInverse[selectedPiece];
                                        squarePositionsInverse[selectedPiece] = new Empty();
                                        finishedTurn = true;
                                    }
                                    break;
                                }
                                else
                                {
                                    var newBoard = squarePositionsInverse.ToDictionary(entry => entry.Key, entry => entry.Value);
                                    string mayMove = squarePositionsInverse[selectedPiece].move(selectedPiece, moveTo, newBoard);
                                    if (mayMove == "Attacking" || mayMove == "Moving")
                                    {
                                        Console.WriteLine(moveTo);
                                        newBoard[moveTo] = newBoard[selectedPiece];
                                        newBoard[selectedPiece] = new Empty();
                                    }
                                    else {
                                        break;
                                    }
                                    kingCantMoveHereTemp = avoidCheck(newBoard, color2, color1, allPositions2, inverseAllPositions2);
                                    if (kingCantMoveHereTemp.Contains(kingsSquare)) {
                                        Console.WriteLine($"King is still in check at {kingsSquare}");
                                        break;
                                    }
                                    else {
                                        squarePositionsInverse[moveTo] = squarePositionsInverse[selectedPiece];
                                        squarePositionsInverse[selectedPiece] = new Empty();
                                        finishedTurn = true;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine(" ! Deselected");
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" ! Invalid square");
                }
            }

            kingCantMoveHere = avoidCheck(squarePositionsInverse, color1, color2, allPositions2, inverseAllPositions2);

            if (this.color == color1)
            {
                this.color = color2;
            }
            else if (this.color == color2)
            {
                this.color = color1;
            }
            else
            {
                Console.WriteLine(" ! Error? Color Mismatch? ");
            }

            Console.WriteLine("______________________________________");
        }

        public void flipBoard1()
        {
            foreach (string square in squarePositions.Keys)
            {
                squarePositionsInverse[square] = squarePositions[square];
            }
        }
        public void flipBoard2()
        {
            foreach (string square in squarePositions.Keys)
            {
                squarePositions[square] = squarePositionsInverse[square];
            }
        }

        public List<string> avoidCheck(Dictionary<string, BoardPiece> board, string oppositeColor, string color, Dictionary<string, int> allPositions, Dictionary<int, string> allPositionsInverse)
        {
            List<string> checkSquares = new List<string>();

            foreach (string key in board.Keys)
            {
                var newBoard = board.ToDictionary(entry => entry.Key, entry => entry.Value);

                foreach (string key2 in newBoard.Keys)
                {
                    if (newBoard[key2].PieceType == "king" && newBoard[key2].Color == oppositeColor)
                    {
                        newBoard[key2] = new Empty();
                    }
                }

                newBoard[key] = new King(oppositeColor, allPositions, allPositionsInverse);

                foreach (string key2 in newBoard.Keys)
                {
                    if (newBoard[key2].Color == color && newBoard[key2].move(key2, key, newBoard, allMoves: true, check: true) == "Attacking")
                    {
                        checkSquares.Add(key);
                    }
                }
            }
            // Console.WriteLine(string.Join(", ", checkSquares));
            return checkSquares;
        }

        private void allPossibleMoves(string selectedPiece, string squarePosition)
        {
            foreach (string key in squarePositions.Keys)
            {
                squarePositions[selectedPiece].move(squarePosition, key, squarePositions, allMoves: true);
            }
        }
    }
}
