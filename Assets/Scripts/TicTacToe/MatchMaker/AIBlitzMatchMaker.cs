using System.Collections.Generic;
using UnityEngine;

public class AIBlitzMatchMaker : IMatchMaker {
    private readonly int aiCount;
    private readonly int minThinkingDelayMs;
    private readonly int maxThinkingDelayMs;

    private readonly string[] symbols = { "X", "O", "A", "B", "Y", "W", "C" };

    public AIBlitzMatchMaker(int aiCount, int minThinking = 1000, int maxThinking = 3000) {
        this.aiCount = aiCount;
        this.minThinkingDelayMs = minThinking;
        this.maxThinkingDelayMs = maxThinking;
    }

    public List<IPlayer> CreateMatchup() {
        List<IPlayer> players = new List<IPlayer>();

        Queue<string> availableSymbols = new Queue<string>(symbols);

        for(int i = aiCount - 1; i >= 0; i--) {
            int randomThinkingTime = Random.Range(minThinkingDelayMs, maxThinkingDelayMs);

            string targetSymbol = availableSymbols.Dequeue();
            if (availableSymbols.Count <= 0) {
                availableSymbols = new Queue<string>(symbols);
            }

            players.Add(new RandomAIPlayer($"AI Player {aiCount - i}", targetSymbol, randomThinkingTime));
        }

        return players;
    }
}
