using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AIPlayer : IPlayer {
    private string name;
    protected string symbol;
    protected int delayMs;

    public AIPlayer(string name, string symbol, int delayMs) {
        this.name = name;
        this.symbol = symbol;
        this.delayMs = delayMs;
    }

    public string GetPlayerName() {
        return name;
    }

    public string GetSymbol() {
        return symbol;
    }

    protected abstract (int, int) GenerateMove(IBoardReader board, List<(int, int)> emptyCells);

    private List<(int, int)> FindEmptyCells(IBoardReader board) {
        List<(int row, int col)> emptyCells = new List<(int, int)>();

        // Scan board for free slots
        for (int r = 0; r < 3; r++) {
            for (int c = 0; c < 3; c++) {
                if (board.IsCellEmpty(r, c)) {
                    emptyCells.Add((r, c));
                }
            }
        }

        return emptyCells;
    }

    public async void MakeMove(IBoardReader board, Action<int, int> onMoveSelected){
        List<(int, int)> emptyCells = FindEmptyCells(board);

        // Select a random available slot
        if (emptyCells.Count > 0) {
            await Task.Delay(delayMs);

            var (row, col) = GenerateMove(board, emptyCells);

            // Trigger the move callback
            onMoveSelected?.Invoke(row, col);
        }
    }
}
