using UnityEngine;

public class GameManager : MonoBehaviour
{
    IBoard board;
    IBoardPresenter boardPresenter;
    IInputProvider inputProvider;
    ITurnManager turnManager;
    IWinChecker winChecker;

    bool isGameOver;

    public GameManager Initialize(IBoard board, IBoardPresenter boardPresenter, IInputProvider inputProvider, IWinChecker winChecker, ITurnManager turnManager)
    {
        this.board = board;
        this.boardPresenter = boardPresenter;
        this.inputProvider = inputProvider;
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

    //private void HandleCellSelected(int row, int col) {
    //    // Guard clauses: Ignore clicks if game is over or cell is occupied
    //    if (isGameOver || !board.IsCellEmpty(row, col)) return;

    //    IPlayer activePlayer = turnManager.GetCurrentPlayer();

    //    // Apply move to domain model and UI presenter
    //    board.SetCell(row, col, activePlayer.GetSymbol());
    //    boardPresenter.SetCellSymbol(row, col, activePlayer.GetSymbol());
    //    //audioService.PlayMoveSound();

    //    GameOutcome outcome = winChecker.CheckOutcome(board, row, col);
    //    // Check victory
    //    if (outcome == GameOutcome.Win || outcome == GameOutcome.Draw) {
    //        isGameOver = true;

    //        if (outcome == GameOutcome.Win)
    //        {
    //            boardPresenter.SetStatusText($"{activePlayer.GetPlayerName()} Wins!");
    //        }

    //        if (outcome == GameOutcome.Draw)
    //        {
    //            boardPresenter.SetStatusText($"Nobody Wins!");
    //        }
    //        //audioService.PlayWinSound();
    //        return;
    //    }

    //    turnManager.AdvanceTurn();
    //    UpdateTurnUI();
    //}

    private void UpdateTurnUI() {
        boardPresenter.SetStatusText($"{turnManager.GetCurrentPlayer().GetPlayerName()}'s Turn ({turnManager.GetCurrentPlayer().GetSymbol()})");
    }

    //private void OnDestroy() {
    //    // Unsubscribe to avoid memory leaks when scene unloads
    //    if (inputProvider != null)
    //    {
    //        inputProvider.OnCellSelected -= HandleCellSelected;
    //    }
    //}
}
