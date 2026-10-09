using UnityEngine;
using UnityEngine.UI;

public interface IBoardPresenter {
    IBoardPresenter Initialize(Button[,] cellButtons);

    // Cell Rendering
    void SetCellSymbol(int row, int col, string symbol);
    void SetCellInteractable(int row, int col, bool interactable);
    void ClearBoard();

    // Tile Syncing
    void SyncTileState(int row, int col, string symbol, bool isInteractable);
    void SyncBoard(IBoard board);

    // Game Status Feedback
    void SetStatusText(string message);

    // End Game Polish
    void HighlightWinningLine(int[] lineIndices);
    void ShowGameOverScreen(string winnerName);
}
