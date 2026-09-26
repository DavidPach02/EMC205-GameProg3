using UnityEngine;
using System.Collections.Generic;

public class MatchFactory {

    public MatchController CreateMatchInstance(MatchSettings settings, IBoardPresenter presenter, List<IPlayer> players)
    {
        IBoard board = new DynamicBoard(settings.boardRows, settings.boardCols);

        // Dynamic win checker configured by MatchSettingsBuilder!
        IWinChecker winChecker = new DynamicWinChecker(settings.winSequenceLength);
        ITurnManager turnManager = new StandardTurnManager();
        turnManager.SetPlayerQueue(new Queue<IPlayer>(players));
        //ITurnTimer turnTimer = new TurnTimer(settings.TurnTimeoutSeconds);

        MatchController matchController = new MatchController();

        return matchController.Initialize(board, presenter, winChecker, turnManager);
    }
}
