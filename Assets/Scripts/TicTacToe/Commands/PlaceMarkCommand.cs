using UnityEngine;

public class PlaceMarkCommand : ICommand {

    private readonly IBoard board;
    private readonly int row;
    private readonly int col;
    private readonly string symbol;
    private bool isSuccessful;

    public PlaceMarkCommand(IBoard board, int row, int col, string symbol) {
        this.board = board;
        this.row = row;
        this.col = col;
        this.symbol = symbol;
    }

    public bool Execute() {
        isSuccessful = board.SetCell(row, col, symbol);
        return isSuccessful;
    }
}
