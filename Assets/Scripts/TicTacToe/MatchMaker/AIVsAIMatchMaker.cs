using UnityEngine;
using System.Collections.Generic;

public class AIVsAIMatchMaker : IMatchMaker {
    private readonly int thinkingDelayMs;

    public AIVsAIMatchMaker(int thinkingDelayMs = 200) {
        this.thinkingDelayMs = thinkingDelayMs;
    }

    public List<IPlayer> CreateMatchup() {
        List<IPlayer> players = new List<IPlayer>();

        IPlayer bot1 = new RandomAIPlayer("AlphaBot", "X", thinkingDelayMs);
        players.Add(bot1);
        IPlayer bot2 = new RandomAIPlayer("BetaBot", "O", thinkingDelayMs);
        players.Add(bot2);

        return players;
    }
}
