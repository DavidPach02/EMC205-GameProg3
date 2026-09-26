using UnityEngine;

public class MatchSettings {
    public int boardRows { get; }
    public int boardCols { get; }
    public int winSequenceLength { get; }
    public float turnTimeoutSecs { get; }
    public int maxPlayers { get; }
    public bool fillWithBots { get; }
    public string botDifficulty { get; }

    public MatchSettings(
        int boardRows,
        int boardCols,
        int winSequenceLength,
        float turnTimeoutSecs,
        int maxPlayers,
        bool fillWithBots,
        string botDifficulty)
    {
        this.boardRows = boardRows;
        this.boardCols = boardCols;
        this.winSequenceLength = winSequenceLength;
        this.turnTimeoutSecs = turnTimeoutSecs;
        this.maxPlayers = maxPlayers;
        this.fillWithBots = fillWithBots;
        this.botDifficulty = botDifficulty;
    }
}
