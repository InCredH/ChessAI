// See https://aka.ms/new-console-template for more information
// #define Name "VICE 1.0"

// using long U64;

class Program
{
    public static void Main(string[] args)
    {
        uint playBitboard = (uint)0;
        Console.WriteLine("Start");
        Board.bitboard.printBitboard(playBitboard);

        playBitboard |= ((uint)1 << Board.SQ64((int)Board.board.A1));
        Console.WriteLine("Value" + playBitboard);;
        Console.WriteLine("D2 added: ");
        Board.bitboard.printBitboard(playBitboard);
    }
}