using UnityEngine;
using UnityEngine.UI;

public class GameInstaller : MonoBehaviour
{
    [SerializeField] private BoardView boardView;
    [SerializeField] private TicTacToeBoardPresenter boardPresenter;
    [SerializeField] private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        IBoard board = new TicTacToeBoard();

        IInputProvider inputProvider = new DynamicInputProvider();
        Button[,] spawnedButtons = boardView.BuildBoard(board.GetLength(0), board.GetLength(1), inputProvider.HandleCellClicked);

        if (boardPresenter != null)
        {
            boardPresenter.Initialize(spawnedButtons);
        }

        IWinChecker winChecker = new Standard3x3WinChecker();
        ITurnManager turnManager = new StandardTurnManager();

        IPlayer p1 = new HumanPlayer("Human Player", "X", inputProvider);
        turnManager.QueuePlayer(p1);
        IPlayer p2 = new RandomAIPlayer("AI Player 1", "O", 1000);
        turnManager.QueuePlayer(p2);
        //IPlayer p3 = new AIPlayer("AI Player 2", "Y");
        //turnManager.QueuePlayer(p3);

        gameManager.Initialize(board, boardPresenter, winChecker, turnManager);
    }
}
