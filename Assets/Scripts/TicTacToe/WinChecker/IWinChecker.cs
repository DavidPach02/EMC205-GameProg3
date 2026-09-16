using UnityEngine;

public enum GameOutcome
{
    InProgress,
    Win,
    Draw
}

public interface IWinChecker
{
    GameOutcome CheckOutcome(IBoardReader board, int lastRow, int lastCol);
}
