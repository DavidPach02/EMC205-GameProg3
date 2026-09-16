using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardView : MonoBehaviour
{
    [Header("Prefab & Parent Dependencies")]
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gridParent;

    public Button[,] BuildBoard(int rows, int cols, Action<int, int> onCellClicked)
    {
        // 1. Clean up any existing cells (useful when restarting a match)
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        Button[,] gridButtons = new Button[rows, cols];

        // Dynamically instantiate cells in a row/col layout
        for (int r = 0; r < rows; r++)
        {
            GameObject rowObj = Instantiate(rowPrefab, gridParent);

            for (int c = 0; c < cols; c++)
            {
                int row = r;
                int col = c;

                // Instantiate prefab inside the grid panel
                GameObject cellObj = Instantiate(cellPrefab, rowObj.transform);
                cellObj.name = $"Cell_{row}_{col}";

                Button cellButton = cellObj.GetComponent<Button>();

                // Bind the button click directly to your input provider's handler
                cellButton.onClick.AddListener(() => onCellClicked?.Invoke(row, col));

                gridButtons[row, col] = cellButton;
            }
        }

        return gridButtons;
    }
}
