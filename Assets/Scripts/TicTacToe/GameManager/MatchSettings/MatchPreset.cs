using UnityEngine;

public static class MatchPreset {
    public static MatchSettings CreateClassic1v1() {
        return new MatchSettingsBuilder()
            .WithBoardSize(3, 3)
            .WithWinCondition(3)
            .WithTurnTimer(30f)
            .WithBattleRoyaleLobby(maxPlayers: 2, fillWithBots: false)
            .Build();
    }

    public static MatchSettings CreateBattleRoyaleBlitz() {
        return new MatchSettingsBuilder()
            .WithBoardSize(10, 10)
            .WithWinCondition(5)
            .WithTurnTimer(5f)
            .WithBattleRoyaleLobby(maxPlayers: 50, fillWithBots: true, botDifficulty: "Random")
            .Build();
    }
}
