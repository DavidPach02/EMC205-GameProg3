using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RandomAIPlayer : AIPlayer {
    public RandomAIPlayer(string name, string symbol, int delayMs) : base(name, symbol, delayMs) { }

    protected override (int, int) GenerateMove(IBoardReader board, List<(int, int)> emptyCells) {
        // Get a random tuple
        int randomIndex = UnityEngine.Random.Range(0, emptyCells.Count);
        return emptyCells[randomIndex];
    }
}
