using System;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPresenter : MonoBehaviour
{
    [SerializeField] private Image[] chargeIcons; // Drag your 5 red power-up squares here in the Inspector
    [SerializeField] private Button removeMarkPowerUpButton; // The main trigger button
    [SerializeField] TicTacToeBoardPresenter boardPresenter;

    private Action<int, int> onTargetSelectedCallback;
    private IInputProvider inputProvider;
    private IBoard board;
    private Func<IPlayer> getActivePlayerFunc;

    public void Initialize(IInputProvider inputProvider, IBoard board, Func<IPlayer> getActivePlayerFunc) {
        this.inputProvider = inputProvider;
        this.board = board;
        this.getActivePlayerFunc = getActivePlayerFunc;
    }

    public void UpdatePowerUpUI(IPlayer activePlayer)
    {
        int availableCharges = activePlayer != null ? activePlayer.powerUpCharges : 0;

        // 1. Fill/highlight icons based on available charges
        for (int i = 0; i < chargeIcons.Length; i++)
        {
            if (chargeIcons[i] != null)
            {
                chargeIcons[i].gameObject.SetActive(i < availableCharges);
            }
        }

        // 2. Disable the power-up button if active player has 0 charges OR if active player is AI
        if (removeMarkPowerUpButton != null)
        {
            bool isHuman = activePlayer is HumanPlayer;
            removeMarkPowerUpButton.interactable = isHuman && availableCharges > 0;
        }
    }

    public void OnHumanPowerUpButtonClicked() {
        IPlayer activePlayer = getActivePlayerFunc?.Invoke();

        if (activePlayer is HumanPlayer human && human.TryUsePowerUp()) {
            // Disable the power-up button temporarily while targeting
            removeMarkPowerUpButton.interactable = false;

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
                UpdatePowerUpUI(activePlayer);
            };

            inputProvider.OnCellSelected += targetingHandler;
        }
    }
}
