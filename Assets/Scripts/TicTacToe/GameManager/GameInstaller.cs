using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameInstaller : MonoBehaviour
{
    [Header("UI & View References")]
    [SerializeField] private BoardView boardView;
    [SerializeField] private TicTacToeBoardPresenter boardPresenter;
    [SerializeField] private StandardRestartScreen restartScreenView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        IInputProvider inputProvider = new DynamicInputProvider();

        Queue<IPlayer> playerQueue = new Queue<IPlayer>();
        IPlayer p1 = new HumanPlayer("Human Player", "X", inputProvider);
        playerQueue.Enqueue(p1);
        IPlayer p2 = new RandomAIPlayer("AI Player 1", "O", 1000);
        playerQueue.Enqueue(p2);
        //IPlayer p3 = new AIPlayer("AI Player 2", "Y");
        //turnManager.QueuePlayer(p3);

        GameManager.Instance.InitializeSession(boardView, boardPresenter, inputProvider, playerQueue, restartScreenView);
    }
}
