using System;
using UnityEngine;

public class DynamicBoard : IBoard
{
    private readonly string[,] grid = new string[3, 3];

    public DynamicBoard(int rows, int cols) {
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Board dimensions must be greater than zero.");

        grid = new string[rows, cols];
    }

    public void ClearBoard() {
        Array.Clear(grid, 0, grid.Length);
    }

    public string GetCell(int row, int col) {
        if (!IsValidCell(row, col)) return string.Empty;
        return grid[row, col];
    }

    public int GetLength(int dimension) {
        return grid.GetLength(dimension);
    }

    public bool IsBoardFull() {
        for (int r = 0; r < GetLength(0); r++) {
            for (int c = 0; c < GetLength(1); c++) {
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

    public void SetCell(int row, int col, string symbol) {
        grid[row, col] = symbol;
    }

    private bool IsValidCell(int row, int col) {
        return row >= 0 && row < GetLength(0) && col >= 0 && col < GetLength(1);
    }
}
