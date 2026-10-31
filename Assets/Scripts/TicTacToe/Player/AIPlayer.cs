using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AIPlayer : IPlayer {
    private string name;
    protected string symbol;
    protected int delayMs;

    public int powerUpCharges { get; private set; } = 5;

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
        for (int r = 0; r < board.GetLength(0); r++) {
            for (int c = 0; c < board.GetLength(1); c++) {
                // TODO: Add IsCellInteractable condition here
                if (board.IsCellEmpty(r, c) && board.IsCellInteractable(r,c)) {
                    emptyCells.Add((r, c));
                }
            }
        }

        return emptyCells;
    }

    private bool TryFindPowerUpTarget(IBoardReader boardReader, out int targetRow, out int targetCol) {
        targetRow = -1;
        targetCol = -1;

        // Simple strategy: Look for any occupied opponent cell to disrupt
        for (int r = 0; r < boardReader.GetLength(0); r++) {
            for (int c = 0; c < boardReader.GetLength(1); c++) {
                string cellSymbol = boardReader.GetCell(r, c);
                // If it contains a mark and isn't empty/locked hazard
                if (!string.IsNullOrEmpty(cellSymbol) && cellSymbol != symbol){
                    targetRow = r;
                    targetCol = c;
                    return true;
                }
            }
        }
        return false;
    }

    public async void MakeMove(IBoardReader board, Action<int, int> onMoveSelected){
        try {
            await Task.Delay(delayMs);
        }
        catch (OperationCanceledException) {
            return; // Exits cleanly if cancelled
        }

        // Ensure the game/application is still playing before processing
        if (!Application.isPlaying) return;

        // Set your desired chance percentage here (e.g., 30.0f for 30% chance)
        float removePowerUpChance = 30.0f;
        // Roll a random percentage (0 to 100)
        bool rollSucceeded = UnityEngine.Random.Range(0f, 100f) <= removePowerUpChance;

        if (rollSucceeded && powerUpCharges > 0 && TryFindPowerUpTarget(board, out int removeRow, out int removeCol)) {
            Debug.Log("Attempting remove");
            if (TryUsePowerUp()) {
                ICommand removeCommand = new RemoveMarkCommand(board as IBoard, removeRow, removeCol);
                removeCommand.Execute();
            }
        }

        // Select a random available slot
        List<(int, int)> emptyCells = FindEmptyCells(board);
        if (emptyCells.Count > 0) {
            var (row, col) = GenerateMove(board, emptyCells);

            // Instantiate and execute PlaceMarkCommand
            ICommand placeCommand = new PlaceMarkCommand(board as IBoard, row, col, symbol);
            bool success = placeCommand.Execute();

            if (success) {
                onMoveSelected?.Invoke(row, col);
            }
        }
    }

    public bool TryUsePowerUp() {
        if (powerUpCharges > 0) {
            powerUpCharges--;
            return true;
        }
        return false;
    }

    public void AddPowerUpCharge() {
        powerUpCharges++;
    }
}
