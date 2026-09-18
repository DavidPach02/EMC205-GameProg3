using UnityEngine;
using UnityEngine.UI;

public class StandardRestartScreen : MonoBehaviour, IRestartScreenView {

    [SerializeField] GameObject screenPanel;
    [SerializeField] Button restartButton;

    void Start() {
        if (restartButton != null) {
            restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        }
    }

    public void Hide() {
        if (screenPanel) {
            screenPanel.SetActive(false);
        }
    }

    public void Show() {
        if (screenPanel) {
            screenPanel.SetActive(true);
        } 
    }
}
