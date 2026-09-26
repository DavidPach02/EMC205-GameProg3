using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // PART 1: Create a private instance, we don't want other scripts to override the instance
    private static GameManager instance;
    // PART 2: Create public access to the private instance 
    public static GameManager Instance {
        get;
        private set;
    }

    private BoardView boardView;
    //private IBoardPresenter boardPresenter;
    private IRestartScreenView restartScreenView;
    private IInputProvider inputProvider;
    //private IMatchMaker matchMaker;

    private MatchController activeMatch;

    // PART 3: Keeping it alive on awake
    private void Awake() {
        // If another instance of game manager comes up and it's not this one
        if (Instance != null && Instance != this) {
            // Destroy that object
            Destroy(gameObject);
            return;
        }

        // If that is not the case, then we assign this instance as the global god script
        Instance = this as GameManager;
        // Don't destroy if you switch screens
        DontDestroyOnLoad(gameObject);
    }

    public void InitializeSession(
        MatchController match,
        BoardView boardView,
        IInputProvider inputProvider,
        IRestartScreenView restartScreenView) 
    {
        this.boardView = boardView;
        //this.boardPresenter = boardPresenter;
        this.inputProvider = inputProvider;
        //this.matchMaker = matchMaker;
        this.activeMatch = match;
        this.restartScreenView = restartScreenView;

        StartNewMatch();
    }

    public void StartNewMatch() {
        // Hide restart screen
        restartScreenView?.Hide();

        // Tie in button UI with the match
        Button[,] spawnedButtons = boardView.BuildBoard(
            activeMatch.board.GetLength(0),
            activeMatch.board.GetLength(1),
            inputProvider.HandleCellClicked
        );

        // Create models - handled by MatchFactory now
        //IBoard board = new TicTacToeBoard();
        //IWinChecker winChecker = new Standard3x3WinChecker();
        //ITurnManager turnManager = new StandardTurnManager();
        //turnManager.SetPlayerQueue(new Queue<IPlayer>(matchMaker.CreateMatchup()));

        if (activeMatch.boardPresenter != null) {
            activeMatch.boardPresenter.Initialize(spawnedButtons);
            activeMatch.boardPresenter.ClearBoard();
        }

        // Instantiate and run clean match
        //activeMatch = new MatchController();
        //activeMatch.Initialize(board, boardPresenter, winChecker, turnManager);

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

    private void OnDestroy()
    {
        
    }

    public void RestartGame() {
        SceneManager.LoadScene("SampleScene");
    }
}
