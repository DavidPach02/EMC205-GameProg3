using UnityEngine;

public class GameManager : MonoBehaviour
{
    IBoard board;
    IBoardPresenter boardPresenter;
    ITurnManager turnManager;
    IWinChecker winChecker;

    bool isGameOver;

    public GameManager Initialize(IBoard board, IBoardPresenter boardPresenter, IWinChecker winChecker, ITurnManager turnManager)
    {
        this.board = board;
        this.boardPresenter = boardPresenter;
        this.winChecker = winChecker;
        this.turnManager = turnManager;

        //this.inputProvider.OnCellSelected += HandleCellSelected;

        turnManager.AdvanceTurn();
        //UpdateTurnUI();
        StartTurn();

        return this;
    }

    private void StartTurn() {
        IPlayer activePlayer = turnManager.GetCurrentPlayer();
        boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()}'s Turn ({activePlayer.GetSymbol()})");

        // Human waits for input and AI immediately picks a slot
        activePlayer.MakeMove(board, ExecuteMove);
    }

    private void ExecuteMove(int row, int col)
    {
        IPlayer activePlayer = turnManager.GetCurrentPlayer();

        board.SetCell(row, col, activePlayer.GetSymbol());
        boardPresenter.SetCellSymbol(row, col, activePlayer.GetSymbol());
        //audioService.PlayMoveSound();

        GameOutcome outcome = winChecker.CheckOutcome(board, row, col);

        if (outcome == GameOutcome.Win)
        {
            boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()} Wins!");
        }
        else if (outcome == GameOutcome.Draw)
        {
            boardPresenter.SetStatusText("Game Ended in a Draw!");
        }
        else
        {
            turnManager.AdvanceTurn();
            StartTurn(); // Trigger next turn execution
        }
    }
}
