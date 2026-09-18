using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance {
        get;
        private set;
    }

    private BoardView boardView;
    private IBoardPresenter boardPresenter;
    private IRestartScreenView restartScreenView;
    private IInputProvider inputProvider;
    private Queue<IPlayer> playerQueue;
    private MatchController activeMatch;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this as GameManager;
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeSession(
        BoardView boardView, 
        IBoardPresenter boardPresenter, 
        IInputProvider inputProvider, 
        Queue<IPlayer> playerQueue, 
        IRestartScreenView restartScreenView) 
    {
        this.boardView = boardView;
        this.boardPresenter = boardPresenter;
        this.inputProvider = inputProvider;
        this.playerQueue = playerQueue;
        this.restartScreenView = restartScreenView;

        StartNewMatch();
    }

    public void StartNewMatch() {
        // Hide restart screen
        restartScreenView?.Hide();

        // Create models
        IBoard board = new TicTacToeBoard();
        IWinChecker winChecker = new Standard3x3WinChecker();
        ITurnManager turnManager = new StandardTurnManager();
        turnManager.SetPlayerQueue(playerQueue);

        // Rebuild or reset UI Board
        Button[,] spawnedButtons = boardView.BuildBoard(
            board.GetLength(0),
            board.GetLength(1),
            inputProvider.HandleCellClicked
        );

        if (boardPresenter != null) {
            boardPresenter.Initialize(spawnedButtons);
            boardPresenter.ClearBoard();
        }

        // Instantiate and run clean match
        activeMatch = new MatchController();
        activeMatch.Initialize(board, boardPresenter, winChecker, turnManager);
        // Subscribe to event
        activeMatch.OnMatchEnded += ActiveMatch_OnMatchEnded;

        activeMatch.StartMatch();
    }

    private void ActiveMatch_OnMatchEnded(GameOutcome outcome, IPlayer activePlayer) {
        if (activeMatch != null) {
            activeMatch.OnMatchEnded -= ActiveMatch_OnMatchEnded;
        }

        restartScreenView?.Show();
    }

    public void RestartGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
