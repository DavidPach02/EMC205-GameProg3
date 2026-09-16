using UnityEngine;

public class TicTacToeBoard : IBoard {
    private readonly string[,] grid = new string[3, 3];

    public void ClearBoard() {
        for (int r = 0; r < grid.GetLength(0); r++) {
            for (int c = 0; c < grid.GetLength(1); c++) {
                grid[r, c] = "";
            }
        }
    }

    public string GetCell(int row, int col) {
        return grid[row, col];
    }

    public int GetColCount() {
        return grid.GetLength(1);
    }

    public int GetLength(int dimension) {
        return grid.GetLength(dimension);
    }

    public bool IsCellEmpty(int row, int col) {
        return string.IsNullOrEmpty(GetCell(row, col));
    }

    public void SetCell(int row, int col, string symbol) {
        grid[row, col] = symbol;
    }
}
