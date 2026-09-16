using UnityEngine;

public enum GameOutcome
{
    InProgress,
    Win,
    Draw
}

public interface IWinChecker
{
    GameOutcome CheckOutcome(IBoard board, int lastRow, int lastCol);
}
