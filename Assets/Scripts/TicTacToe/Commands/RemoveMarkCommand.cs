using UnityEngine;

public class RemoveMarkCommand : ICommand
{
    private readonly IBoard board;
    private readonly int row;
    private readonly int col;
    //private string removedSymbol; // Saved for potential Undo support

    public RemoveMarkCommand(IBoard board, int row, int col) {
        this.board = board;
        this.row = row;
        this.col = col;
    }

    public bool Execute() {
        if (board == null) return false;

        Debug.Log($"Removed [{row}, {col}]");

        return board.ForceClearCell(row, col);
    }
}
