using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpViewModel {
    private Func<IPlayer> getActivePlayerFunc;
    private IBoard board;
    private IInputProvider inputProvider;
    TicTacToeBoardPresenter boardPresenter;

    // --- Observable Properties / State ---
    public int CurrentCharges { get; private set; }
    public bool IsInteractable { get; private set; }

    // --- Binding Events ---
    public event Action<int> OnChargesChanged;
    public event Action<bool> OnInteractableChanged;

    public PowerUpViewModel(
        Func<IPlayer> getActivePlayerFunc,
        IBoard board,
        IInputProvider inputProvider,
        TicTacToeBoardPresenter boardPresenter) {

        this.getActivePlayerFunc = getActivePlayerFunc;
        this.board = board;
        this.inputProvider = inputProvider;
        this.boardPresenter = boardPresenter;
    }

    public void UpdateState() {
        IPlayer activePlayer = getActivePlayerFunc?.Invoke();
        CurrentCharges = activePlayer != null ? activePlayer.powerUpCharges : 0;
        IsInteractable = activePlayer is HumanPlayer && CurrentCharges > 0;

        // Notify the View of state changes (Data Binding)
        OnChargesChanged?.Invoke(CurrentCharges);
        OnInteractableChanged?.Invoke(IsInteractable);
    }

    public void ExecutePowerUpCommand() {
        IPlayer activePlayer = getActivePlayerFunc?.Invoke();

        if (activePlayer is HumanPlayer human && human.TryUsePowerUp()) {
            // Temporarily disable button via data binding
            IsInteractable = false;
            OnInteractableChanged?.Invoke(IsInteractable);

            boardPresenter.SetTargetingMode(true);
            boardPresenter.SyncBoard(board);

            // Listen for the next cell click to apply the remove command
            Action<int, int> targetingHandler = null;
            targetingHandler = (row, col) =>
            {
                // Unsubscribe input provider targeting listener
                inputProvider.OnCellSelected -= targetingHandler;

                // Execute the remove mark command
                ICommand removeCmd = new RemoveMarkCommand(board, row, col);
                if (removeCmd.Execute()) {
                    Debug.Log($"Successfully removed cell [{row}, {col}]");
                }

                boardPresenter.SetTargetingMode(false);
                boardPresenter.SyncBoard(board);

                // Refresh state after action completes
                UpdateState();
            };

            inputProvider.OnCellSelected += targetingHandler;
        }
    }
}
