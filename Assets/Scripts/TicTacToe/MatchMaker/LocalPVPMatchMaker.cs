using UnityEngine;
using System.Collections.Generic;

public class LocalPVPMatchMaker : IMatchMaker {
    private readonly IInputProvider inputProvider;

    public LocalPVPMatchMaker(IInputProvider inputProvider) {
        this.inputProvider = inputProvider;
    }

    public List<IPlayer> CreateMatchup() {
        List<IPlayer> players = new List<IPlayer>();

        IPlayer player1 = new HumanPlayer("Player 1", "X", inputProvider);
        players.Add(player1);
        IPlayer player2 = new HumanPlayer("Player 2", "O", inputProvider);
        players.Add(player2);

        return players;
    }
}
