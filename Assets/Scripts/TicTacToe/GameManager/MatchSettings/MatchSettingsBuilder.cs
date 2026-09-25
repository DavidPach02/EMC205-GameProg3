using System;
using UnityEngine;

public class MatchSettingsBuilder {
    // Defaults
    private int boardRows = 3;
    private int boardCols = 3;
    private int winSequenceLength = 3;
    private float turnTimeoutSeconds = 15f;
    private int maxPlayers = 2;
    private bool fillWithBots = true;
    private string botDifficulty = "Random";

    public MatchSettingsBuilder WithBoardSize(int rows, int cols) {
        boardRows = rows;
        boardCols = cols;
        return this;
    }

    public MatchSettingsBuilder WithWinCondition(int sequenceLength) {
        winSequenceLength = sequenceLength;
        return this;
    }

    public MatchSettingsBuilder WithTurnTimer(float seconds) {
        turnTimeoutSeconds = seconds;
        return this;
    }

    public MatchSettingsBuilder WithBattleRoyaleLobby(int maxPlayers, bool fillWithBots = true, string botDifficulty = "Random") {
        this.maxPlayers = maxPlayers;
        this.fillWithBots = fillWithBots;
        this.botDifficulty = botDifficulty;
        return this;
    }

    //public MatchSettingsBuilder EnableSpectators(bool enable = true) {
    //    allowSpectators = enable;
    //    return this;
    //}

    //public MatchSettingsBuilder WithObstacleCells(bool enable = true) {
    //    enableObstacleCells = enable;
    //    return this;
    //}

    public MatchSettings Build() {
        // Validation logic inside the builder
        if (winSequenceLength > Mathf.Max(boardRows, boardCols)) {
            throw new InvalidOperationException($"Win sequence ({winSequenceLength}) cannot exceed maximum board dimension ({Math.Max(boardRows, boardCols)}).");
        }

        if (maxPlayers < 2) {
            throw new InvalidOperationException("A match must allow at least 2 players.");
        }

        return new MatchSettings(
            boardRows,
            boardCols,
            winSequenceLength,
            turnTimeoutSeconds,
            maxPlayers,
            fillWithBots,
            botDifficulty
        );
    }
}
