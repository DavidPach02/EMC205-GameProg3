using UnityEngine;

public class DynamicWinChecker : IWinChecker {
    private readonly int winSequenceLength;

    public DynamicWinChecker(int winSequenceLength) {
        this.winSequenceLength = winSequenceLength;
    }

    public GameOutcome CheckOutcome(IBoardReader board, int lastRow, int lastCol)
    {
        // Check for Win condition along 4 directional axes
        if (IsWinningMove(board, lastRow, lastCol))
        {
            return GameOutcome.Win;
        }

        // 2. Check for Draw condition
        if (board.IsBoardFull())
        {
            return GameOutcome.Draw;
        }

        // 3. Match continues
        return GameOutcome.InProgress;
    }

    public bool IsWinningMove(IBoardReader board, int lastRow, int lastCol) {
        string symbol = board.GetCell(lastRow, lastCol);

        // 4 Directional Vectors: (rowDelta, colDelta)
        int[][] directions = new int[][] {
            new int[] { 0, 1 },  // Horizontal (-)
            new int[] { 1, 0 },  // Vertical (|)
            new int[] { 1, 1 },  // Diagonal (\)
            new int[] { 1, -1 }  // Anti-Diagonal (/)
        };

        foreach (var dir in directions) {
            int rowDelta = dir[0];
            int colDelta = dir[1];

            // Start with 1 for the move just made
            int consecutiveCount = 1;

            // Check in the positive direction
            consecutiveCount += CountInDirection(board, lastRow, lastCol, rowDelta, colDelta, symbol);

            // Check in the opposite (negative) direction
            consecutiveCount += CountInDirection(board, lastRow, lastCol, -rowDelta, -colDelta, symbol);

            // Check if win condition sequence met
            if (consecutiveCount >= winSequenceLength) {
                return true;
            }
        }

        return false;
    }

    private int CountInDirection(IBoardReader board, int startRow, int startCol, int rowDelta, int colDelta, string symbol) {
        int count = 0;
        int r = startRow + rowDelta;
        int c = startCol + colDelta;

        // Traverse as long as coordinates stay inside bounds and symbols match
        while (r >= 0 && r < board.GetLength(0) && c >= 0 && c < board.GetLength(1)) {
            if (board.GetCell(r, c) == symbol) {
                count++;
                r += rowDelta;
                c += colDelta;
            }
            else {
                break; // Sequence broken
            }
        }

        return count;
    }
}
