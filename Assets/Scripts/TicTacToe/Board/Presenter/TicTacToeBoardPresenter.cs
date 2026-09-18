using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeBoardPresenter : MonoBehaviour, IBoardPresenter
{
    [SerializeField] TextMeshProUGUI statusText;

    private Button[,] cellButtons;

    IBoardPresenter IBoardPresenter.Initialize(Button[,] cellButtons) {
        this.cellButtons = cellButtons;
        return this;
    }

    public void ClearBoard() {
        if (cellButtons == null) return;

        foreach (Button button in cellButtons) {
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            button.interactable = true;
        }
    }

    public void HighlightWinningLine(int[] lineIndices)
    {
        throw new System.NotImplementedException();
    }

    public void SetCellInteractable(int row, int col, bool interactable) {
        cellButtons[row, col].interactable = interactable;
    }

    public void SetCellSymbol(int row, int col, string symbol) {
        if (cellButtons == null) return;

        // Update the visual text on the specific button
        TextMeshProUGUI cellText = cellButtons[row, col].GetComponentInChildren<TextMeshProUGUI>();
        if (cellText != null) {
            cellText.text = symbol;
        }

        // Disable button so it can't be clicked again
        cellButtons[row, col].interactable = false;
    }

    public void SetStatusText(string message) {
        if (statusText != null) {
            statusText.text = message;
        }
    }

    public void ShowGameOverScreen(string winnerName) {
        throw new System.NotImplementedException();
    }
}
