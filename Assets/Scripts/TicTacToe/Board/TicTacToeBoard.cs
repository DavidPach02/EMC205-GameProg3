using UnityEngine;

public class TicTacToeBoard : IBoard {
    //private readonly string[,] grid = new string[3, 3];
    private readonly TileContext[,] tiles = new TileContext[3, 3];

    public TicTacToeBoard() {
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                tiles[r, c] = new TileContext(r, c);
    }

    public void AdvanceTileTurn() {
        foreach (var tile in tiles) {
            tile.PassTurn();
        }
    }

    public void ClearBoard() {
        for (int r = 0; r < tiles.GetLength(0); r++) {
            for (int c = 0; c < tiles.GetLength(1); c++) {
                tiles[r, c].occupyingSymbol = "";
            }
        }
    }

    public string GetCell(int row, int col) {
        if (!IsValidCell(row, col)) return string.Empty;
        return tiles[row, col]?.occupyingSymbol ?? string.Empty;
    }

    public int GetLength(int dimension) {
        return tiles.GetLength(dimension);
    }

    public bool IsBoardFull() {
        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                if (IsCellEmpty(r, c)) {
                    // Found an open space
                    return false;
                }
            }
        }
        // No empty cells left
        return true;
    }

    public bool IsCellEmpty(int row, int col) {
        return string.IsNullOrEmpty(GetCell(row, col));
    }

    public bool IsCellInteractable(int row, int col) {
        // Check array bounds
        if (row < 0 || row >= GetLength(0) || col < 0 || col >= GetLength(1)) {
            return false;
        }

        // Safely read state using null-conditional operator
        return tiles[row, col]?.currentState?.canBeClicked ?? false;
    }

    public bool IsValidCell(int row, int col) {
        return row >= 0 && row < GetLength(0) && col >= 0 && col < GetLength(1);
    }

    public bool SetCell(int row, int col, string symbol) {
        if (!IsValidCell(row, col)) return false;
        return tiles[row, col].TryClick(symbol); ;
    }
}
