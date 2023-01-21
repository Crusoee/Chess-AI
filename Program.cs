using Chess_AI;

Console.WriteLine(" - Would you like to be white or black?");
string color;
string color2;
while (true)
{
    color = Console.ReadLine();

    if (color == "white")
    {
        color = "white";
        color2 = "black";
        break;
    }
    else if (color == "black")
    {
        color = "black";
        color2 = "white";
        break;
    }
    else
    {
        Console.WriteLine(" ! Only options: white / black");
    }
}

Console.WriteLine("GAME START");
Console.WriteLine("______________________________________");

Board board = new Board(color, color2);

while (true) {
    
    board.movePiece();

    board.flipBoard1();

    board.movePiece2();

    board.flipBoard2();
}
