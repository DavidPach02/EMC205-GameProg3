using UnityEngine;
using UnityEngine.UI;

public class PowerUpView : MonoBehaviour {
    [SerializeField] private Image[] chargeIcons;
    [SerializeField] private Button removeMarkButton;

    private PowerUpViewModel viewModel;

    // Call this from your MatchController during setup
    public void Bind(PowerUpViewModel vm) {
        viewModel = vm;

        // Subscribe to ViewModel property change events (Data Binding)
        viewModel.OnChargesChanged += RenderCharges;
        viewModel.OnInteractableChanged += SetButtonInteractable;

        // Bind UI button click to ViewModel command
        removeMarkButton.onClick.AddListener(() => viewModel.ExecutePowerUpCommand());

        // 3. Initial sync
        viewModel.UpdateState();
    }

    private void OnDestroy() {
        // Always unsubscribe to prevent memory leaks
        if (viewModel != null) {
            viewModel.OnChargesChanged -= RenderCharges;
            viewModel.OnInteractableChanged -= SetButtonInteractable;
        }
    }

    // View-only logic: Updates red charge squares visually
    private void RenderCharges(int charges) {
        for (int i = 0; i < chargeIcons.Length; i++) {
            if (chargeIcons[i] != null) {
                chargeIcons[i].gameObject.SetActive(i < charges);
            }
        }
    }

    // View-only logic: Updates button interactivity
    private void SetButtonInteractable(bool isInteractable) {
        if (removeMarkButton != null) {
            removeMarkButton.interactable = isInteractable;
        }
    }
}
