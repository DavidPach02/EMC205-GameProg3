using UnityEngine;

public interface IBoardPresenter {
    // Cell Rendering
    void SetCellSymbol(int row, int col, string symbol);
    void SetCellInteractable(int row, int col, bool interactable);
    void ClearBoard();

    // Game Status Feedback
    void SetStatusText(string message);

    // End Game Polish
    void HighlightWinningLine(int[] lineIndices);
    void ShowGameOverScreen(string winnerName);
}
