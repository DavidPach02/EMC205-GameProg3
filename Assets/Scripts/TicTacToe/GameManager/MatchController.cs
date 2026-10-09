using UnityEngine;
using System;

public class MatchController
{
    public IBoard board;
    public IBoardPresenter boardPresenter;
    public ITurnManager turnManager;
    public IWinChecker winChecker;

    bool isGameOver;

    int turnCounter = 0;
    int ringLockCounter = 5;
    int ringIndex = 0;

    public event Action<GameOutcome, IPlayer> OnMatchEnded;

    public MatchController Initialize(IBoard board, IBoardPresenter boardPresenter, IWinChecker winChecker, ITurnManager turnManager) {
        this.board = board;
        this.boardPresenter = boardPresenter;
        this.winChecker = winChecker;
        this.turnManager = turnManager;

        return this;
    }

    public void StartMatch() {
        turnCounter = 0;
        isGameOver = false;
        turnManager.AdvanceTurn();
        StartTurn();
    }

    private void StartTurn() {
        turnCounter++;

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

        if (outcome == GameOutcome.Win) {
            EndMatch(GameOutcome.Win, activePlayer);
            return;
        }

        board.AdvanceTileTurn();
        if (turnCounter % ringLockCounter == 0) {
            if (board is DynamicBoard dynamicBoard) {
                dynamicBoard.LockOuterRing(ringIndex);
                ringIndex++;
            }
        }
        boardPresenter.SyncBoard(board);

        if (board.IsBoardFull()) {
            EndMatch(GameOutcome.Draw, activePlayer);
            return;
        }

        turnManager.AdvanceTurn();
        StartTurn(); // Trigger next turn execution
    }

    private void EndMatch(GameOutcome outcome, IPlayer activePlayer) {
        isGameOver = true;
    
        if (outcome == GameOutcome.Win) {
            boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()} Wins!");
        }
        else{
            boardPresenter.SetStatusText("Game Ended in a Draw!");
        }

        OnMatchEnded?.Invoke(outcome, activePlayer);
    }
}
