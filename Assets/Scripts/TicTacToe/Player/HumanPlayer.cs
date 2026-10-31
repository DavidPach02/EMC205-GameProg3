using System;
using UnityEngine;

public class HumanPlayer : IPlayer
{
    private string name;
    private string symbol;
    private IInputProvider inputProvider;
    ICommand markCommand;

    public int powerUpCharges { get; private set; } = 5;

    public HumanPlayer(string name, string symbol, IInputProvider inputProvider) {
        this.name = name;
        this.symbol = symbol;
        this.inputProvider = inputProvider;
    }

    public void AddPowerUpCharge() {
        powerUpCharges++;
    }

    public bool TryUsePowerUp() {
        if (powerUpCharges > 0) {
            powerUpCharges--;
            return true;
        }
        return false;
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

                if (board is IBoard)
                {
                    markCommand = new PlaceMarkCommand(board as IBoard, row, col, symbol);
                    bool success = markCommand.Execute();

                    if (success)
                    {
                        onMoveSelected?.Invoke(row, col);
                    }
                }
            }
        };

        inputProvider.OnCellSelected += inputHandler;
    }
}
