using System;
using UnityEngine;

public class HumanPlayer : IPlayer
{
    private string name;
    private string symbol;
    private IInputProvider inputProvider;

    public HumanPlayer(string name, string symbol, IInputProvider inputProvider) {
        this.name = name;
        this.symbol = symbol;
        this.inputProvider = inputProvider;
    }

    public string GetPlayerName() {
        return name;
    }

    public string GetSymbol() {
        return symbol;
    }

    public void MakeMove(IBoardReader board, Action<int, int> onMoveSelected) {
        Action<int, int> inputHandler = null;

        inputHandler = (row, col) => {
            // Only accept clicks on empty board cells
            if (board.IsCellEmpty(row, col)) {
                // Unsubscribe immediately so subsequent clicks aren't processed twice
                inputProvider.OnCellSelected -= inputHandler;
                onMoveSelected?.Invoke(row, col);
            }
        };

        inputProvider.OnCellSelected += inputHandler;
    }
}
