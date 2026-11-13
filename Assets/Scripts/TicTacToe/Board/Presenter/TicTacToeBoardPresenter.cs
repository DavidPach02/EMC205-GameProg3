using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeBoardPresenter : MonoBehaviour, IBoardPresenter {
    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] MarkFlyweightFactory flyweightFactory;

    private Button[,] cellButtons;
    private bool isTargetingForRemoval = false;

    public void SetTargetingMode(bool active) {
        isTargetingForRemoval = active;
    }

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

    public void HighlightWinningLine(int[] lineIndices) {
        throw new System.NotImplementedException();
    }

    public void SetCellInteractable(int row, int col, bool interactable) {
        cellButtons[row, col].interactable = interactable;
    }

    public void SetCellSymbol(int row, int col, string symbol) {
        if (cellButtons == null) return;

        SyncTileState(row, col, symbol, false);
    }

    public void SetStatusText(string message) {
        if (statusText != null) {
            statusText.text = message;
        }
    }

    public void ShowGameOverScreen(string winnerName) {
        throw new System.NotImplementedException();
    }

    public void SyncTileState(int row, int col, string symbol, bool isInteractable) {
        if (cellButtons == null) return;

        Button button = cellButtons[row, col];
        if (button == null) return;

        // Update Visual Symbol
        //Image buttonImage = button.transform.Find("SymbolImage")?.GetComponent<Image>();
        Image buttonImage = null;
        foreach (Transform child in button.transform) {
            if (child.name != "Button" && child.GetComponent<Image>() != null) {
                buttonImage = child.GetComponent<Image>();
                break;
            }
        }

        TextMeshProUGUI cellText = button.GetComponentInChildren<TextMeshProUGUI>();
        
        if (string.IsNullOrEmpty(symbol)) {
            if (buttonImage != null) buttonImage.gameObject.SetActive(false);
            if (cellText != null) cellText.text = "";
        }
        else {
            MarkFlyweight flyweight = flyweightFactory != null ? flyweightFactory.GetFlyweight(symbol) : null;

            if (flyweight != null && buttonImage != null) {
                buttonImage.gameObject.SetActive(true);
                buttonImage.sprite = flyweight.symbolSprite;
                buttonImage.color = flyweight.displayColor;
                if (cellText != null) cellText.text = ""; // Clear text if using sprites
            }
            else if (cellText != null) {
                // Fallback to text if flyweight/image isn't set up yet
                cellText.text = symbol;
            }
        }

        // Update Interactability based on Tile State
        button.interactable = isInteractable;
    }

    public void SyncBoard(IBoard board) {
        if (cellButtons == null || board == null) return;

        for (int r = 0; r < board.GetLength(0); r++) {
            for (int c = 0; c < board.GetLength(1); c++) {
                // Read symbol and clickability directly from domain board
                string symbol = board.GetCell(r, c);
                bool isInteractable;

                if (isTargetingForRemoval) {
                    // DURING TARGETING: Only cells with a mark (and not locked) can be clicked!
                    bool isOccupied = !string.IsNullOrEmpty(symbol);
                    bool isLocked = !board.IsCellInteractable(r, c) && isOccupied == false; // Adjust based on your lock check
                    isInteractable = isOccupied;
                }
                else {
                    // NORMAL MODE: Only empty and interactable cells can be clicked
                    isInteractable = board.IsCellInteractable(r, c) && string.IsNullOrEmpty(symbol);
                }

                SyncTileState(r, c, symbol, isInteractable);
            }
        }
    }
}
