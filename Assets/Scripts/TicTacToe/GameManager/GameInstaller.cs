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

        //Queue<IPlayer> playerQueue = new Queue<IPlayer>();
        //IPlayer p1 = new HumanPlayer("Human Player", "X", inputProvider);
        //playerQueue.Enqueue(p1);
        //IPlayer p2 = new RandomAIPlayer("AI Player 1", "O", 1000);
        //playerQueue.Enqueue(p2);

        //IPlayer p3 = new StupidAIPlayer("Stupid Player", "Y", 2000);
        //playerQueue.Enqueue(p3);

        // Create the match
        MatchFactory matchFactory = new MatchFactory();
        IMatchMaker aiMatchMaker = new AIVsAIMatchMaker(1000);
        IMatchMaker pvpMatchMaker = new LocalPVPMatchMaker(inputProvider);
        IMatchMaker blitzMatchMaker = new AIBlitzMatchMaker(10);

        // Create match settings
        MatchSettings matchSettings = MatchPreset.CreateBattleRoyaleBlitz();

        MatchController controller = matchFactory.CreateMatchInstance(matchSettings, boardPresenter, blitzMatchMaker.CreateMatchup());

        GameManager.Instance.InitializeSession(controller, boardView, inputProvider, restartScreenView);
    }
}
