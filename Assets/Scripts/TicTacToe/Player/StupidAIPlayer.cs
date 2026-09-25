using System.Collections.Generic;
using UnityEngine;

public class StupidAIPlayer : AIPlayer {

    public StupidAIPlayer(string name, string symbol, int delayMs) : base(name, symbol, delayMs) { }

    protected override (int, int) GenerateMove(IBoardReader board, List<(int, int)> emptyCells) {
        return emptyCells[0];
    }
}
