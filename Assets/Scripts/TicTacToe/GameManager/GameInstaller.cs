using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameInstaller : MonoBehaviour
{
    [Header("UI & View References")]
    [SerializeField] private BoardView boardView;
    [SerializeField] private TicTacToeBoardPresenter boardPresenter;
    [SerializeField] private StandardRestartScreen restartScreenView;
    [SerializeField] private PowerUpView powerUpView;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        IInputProvider inputProvider = new DynamicInputProvider();

        // Create the match
        MatchFactory matchFactory = new MatchFactory();
        IMatchMaker aiMatchMaker = new AIVsAIMatchMaker(1000);
        IMatchMaker pvpMatchMaker = new LocalPVPMatchMaker(inputProvider);
        IMatchMaker blitzMatchMaker = new AIBlitzMatchMaker(10);

        // Create match settings
        MatchSettings matchSettings = MatchPreset.CreateBattleRoyaleBlitz();

        // Create Match controller
        MatchController controller = matchFactory.CreateMatchInstance(matchSettings, boardPresenter, pvpMatchMaker.CreateMatchup());

        // Create the PowerUpViewModel
        PowerUpViewModel vm = new PowerUpViewModel(
            () => controller.turnManager.GetCurrentPlayer(),
            controller.board,
            inputProvider,
            boardPresenter
        );

        // Bind View to ViewModel
        if (powerUpView != null) {
            powerUpView.Bind(vm);
        }

        controller.SetPowerUpPresenter(inputProvider, vm);

        GameManager.Instance.InitializeSession(controller, boardView, inputProvider, restartScreenView);
    }
}
