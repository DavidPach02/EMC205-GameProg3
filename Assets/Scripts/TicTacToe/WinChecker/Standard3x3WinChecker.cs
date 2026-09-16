using UnityEngine;

public class Standard3x3WinChecker : IWinChecker {
    public GameOutcome CheckOutcome(IBoardReader board, int lastRow, int lastCol) {
        // Check if the move resulted in a WIN
        if (IsWin(board, lastRow, lastCol)) {
            return GameOutcome.Win;
        }

        // Check if the board is completely full (DRAW)
        if (IsBoardFull(board)) {
            return GameOutcome.Draw;
        }

        // 3. Otherwise, the game continues
        return GameOutcome.InProgress;
    }

    private bool IsWin(IBoardReader board, int lastRow, int lastCol) {
        string symbol = board.GetCell(lastRow, lastCol);
        if (string.IsNullOrEmpty(symbol)) return false;

        // Row check
        if (board.GetCell(lastRow, 0) == symbol &&
            board.GetCell(lastRow, 1) == symbol &&
            board.GetCell(lastRow, 2) == symbol) return true;

        // Column check
        if (board.GetCell(0, lastCol) == symbol &&
            board.GetCell(1, lastCol) == symbol &&
            board.GetCell(2, lastCol) == symbol) return true;

        // Diagonal checks
        if (lastRow == lastCol &&
            board.GetCell(0, 0) == symbol &&
            board.GetCell(1, 1) == symbol &&
            board.GetCell(2, 2) == symbol) return true;

        if (lastRow + lastCol == 2 &&
            board.GetCell(0, 2) == symbol &&
            board.GetCell(1, 1) == symbol &&
            board.GetCell(2, 0) == symbol) return true;

        return false;
    }

    private bool IsBoardFull(IBoardReader board) {
        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                if (board.IsCellEmpty(r, c)) {
                    // Found an open space
                    return false; 
                }
            }
        }
        // No empty cells left
        return true; 
    }
}
