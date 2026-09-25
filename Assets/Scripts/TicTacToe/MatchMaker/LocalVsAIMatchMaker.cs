using UnityEngine;
using System.Collections.Generic;

public class LocalVsAIMatchMaker : IMatchMaker {
    private readonly IInputProvider inputProvider;
    private readonly string aiDifficulty;
    private readonly int thinkingDelayMs;

    public LocalVsAIMatchMaker(IInputProvider inputProvider, string aiDifficulty = "Stupid", int thinkingDelayMs = 800) {
        this.inputProvider = inputProvider;
        this.aiDifficulty = aiDifficulty;
        this.thinkingDelayMs = thinkingDelayMs;
    }

    public List<IPlayer> CreateMatchup() {
        List<IPlayer> players = new List<IPlayer>();
        IPlayer human = new HumanPlayer("Player 1", "X", inputProvider);
        players.Add(human);

        IPlayer ai = aiDifficulty == "Stupid"
            ? new StupidAIPlayer("CPU Bot", "O", thinkingDelayMs)
            : new RandomAIPlayer("CPU Bot", "O", thinkingDelayMs);
        players.Add(ai);

        return players;
    }
}
