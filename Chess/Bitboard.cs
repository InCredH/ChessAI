public class Bitboard {
    uint shiftMe = (uint)1;
    
    int rank = 0;
    int file = 0;
    int sq = 0;
    int sq64 = 0;

    public void printBitboard(uint bb) {
        Console.WriteLine();
        for(int rank = 7; rank >= 0 ; rank--) {
            for(int file  = 0 ; file <= 7 ; file++) {
                sq = Board.FR2SQ(file, rank); // 120 based
                sq64 = Board.SQ64(sq); // 64 based
                if(((shiftMe << sq64) & bb) != 0) 
				    Console.Write("X");
			    else 
				    Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}   