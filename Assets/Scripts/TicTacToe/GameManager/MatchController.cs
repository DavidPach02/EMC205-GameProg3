using UnityEngine;
using System;

public class MatchController
{
    IBoard board;
    IBoardPresenter boardPresenter;
    ITurnManager turnManager;
    IWinChecker winChecker;

    bool isGameOver;

    public event Action<GameOutcome, IPlayer> OnMatchEnded;

    public MatchController Initialize(IBoard board, IBoardPresenter boardPresenter, IWinChecker winChecker, ITurnManager turnManager)
    {
        this.board = board;
        this.boardPresenter = boardPresenter;
        this.winChecker = winChecker;
        this.turnManager = turnManager;

        return this;
    }

    public void StartMatch() {
        isGameOver = false;
        turnManager.AdvanceTurn();
        StartTurn();
    }

    private void StartTurn() {
        IPlayer activePlayer = turnManager.GetCurrentPlayer();
        boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()}'s Turn ({activePlayer.GetSymbol()})");

        // Human waits for input and AI immediately picks a slot
        activePlayer.MakeMove(board, ExecuteMove);
    }

    private void ExecuteMove(int row, int col) {
        if (isGameOver) return;

        IPlayer activePlayer = turnManager.GetCurrentPlayer();

        board.SetCell(row, col, activePlayer.GetSymbol());
        boardPresenter.SetCellSymbol(row, col, activePlayer.GetSymbol());
        //audioService.PlayMoveSound();

        GameOutcome outcome = winChecker.CheckOutcome(board, row, col);

        if (outcome == GameOutcome.Win || outcome == GameOutcome.Draw) {
            isGameOver = true;
            if (outcome == GameOutcome.Win) {
                boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()} Wins!");
            }
            else {
                boardPresenter.SetStatusText("Game Ended in a Draw!");
            }

            OnMatchEnded?.Invoke(outcome, activePlayer);
        }
        else {
            turnManager.AdvanceTurn();
            StartTurn(); // Trigger next turn execution
        }
    }
}
