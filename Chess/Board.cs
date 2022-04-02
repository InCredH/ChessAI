using System.Collections;

// namespace Chess.Game {
public static class Board {
    const int BOARD = 120;
    const string NAME = "YASH 1.0";
    enum pieces_acr {
        EMPTY,
        wP,
        wN,
        wB,
        wR,
        wQ,
        wK,
        bP,
        bN,
        bB,
        bR,
        bQ,
        bK
    };
    enum file {
        FILE_A,
        FILE_B,
        FILE_C,
        FILE_D,
        FILE_E,
        FILE_F,
        FILE_G,
        FILE_H,
        FILE_NONE
    };
    public enum rank {
        RANK_1,
        RANK_2,
        RANK_3,
        RANK_4,
        RANK_5,
        RANK_6,
        RANK_7,
        RANK_8,
        RANK_NONE
    };
    public enum color {
        WHITE,
        BLACK,
        BOTH
    };
    public enum board {
        A1 = 21, B1, C1, D1, E1, F1, G1, H1,
            A2 = 31, B2, C2, D2, E2, F2, G2, H2,
            A3 = 41, B3, C3, D3, E3, F3, G3, H3,
            A4 = 51, B4, C4, D4, E4, F4, G4, H4,
            A5 = 61, B5, C5, D5, E5, F5, G5, H5,
            A6 = 71, B6, C6, D6, E6, F6, G6, H6,
            A7 = 81, B7, C7, D7, E7, F7, G7, H7,
            A8 = 91, B8, C8, D8, E8, F8, G8, H8, NO_SQ
    };

    // to store the history of the game so that we can undo
    const int MAXNUMBEROFMOVES = 2048;

    internal class Undo {
        int move;
        int castlePerm;
        // En Passant move 
        int enp;
        // Fifty Move rule
        int fiftyMoves;
    }
    /* 
        @W = white
        @K = king 
        @Q = Queen
        @CA = castling
        @B = black 
        castling will be checked by a 4 bit number
        if any of the 4 bits is 0 then the permission is allowed
    */
    enum castling {
        WKCA = 1, WQCA = 2, BKCA = 4, BQCA = 8
    };
    //to store the current postions of the pieces
    static int[] pieces = new int[BOARD];
    // To store the position of the pawn
    static uint[] pawns = new uint[3];

    // To determine the postition of the king
    static int[] kingSQ = new int[2];

    static int side;
    //to store if there is any enpass square available
    static int enPass;
    // Fifty Move check
    static int fiftyMoves;

    // Number of plays played by both the players till now
    static int ply;
    // how many total half moves have been played
    static int hisPlay;

    static int[] pceNum = new int[13];
    // Pieces which is not a pawn
    static int[] bigPce = new int[3];
    // Rook and Queen
    static int[] majPce = new int[3];
    // Bishops and Knight
    static int[] minPce = new int[3];

    // 4 bit integer to check the casling permissions 
    static int castlePerm;

    static Undo[] history = new Undo[MAXNUMBEROFMOVES];

    public static int[] Sq120ToSq64 = new int[BOARD];
    public static int[] Sq64ToSq120 = new int[64];

    // how to represent pieces :
    // pList[wN][0] = E1
    static int[,] pList = new int[13,10];
    //this way of representation makes the code 20% faster

    public static Bitboard bitboard = new Bitboard();

    static Board() {
        int index = 0;
        int column = (int) file.FILE_A;
        int row = (int) rank.RANK_1;
        int sq = (int) board.A1;
        int sq64 = 0;
        for (index = 0; index < BOARD; ++index) {
            Sq120ToSq64[index] = 65;
        }

        for (index = 0; index < 64; ++index) {
            Sq64ToSq120[index] = 120;
        }
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                sq = FR2SQ(j, i);
                Sq64ToSq120[sq64] = sq;
                Sq120ToSq64[sq] = sq64;
                sq64++;
            }
        }

    }

    public static int FR2SQ(int f, int r) {
        return ((21 + (f)) + ((r) * 10));
    }

    public static int SQ64(int x) {
        return Sq120ToSq64[x];
    }

    public static int SQ120(int x) {
        return Sq64ToSq120[x];
    }
}
// }