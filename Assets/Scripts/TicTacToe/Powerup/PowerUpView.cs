using UnityEngine;
using UnityEngine.UI;

public class PowerUpView : MonoBehaviour {
    [SerializeField] private Image[] chargeIcons;
    [SerializeField] private Button removeMarkButton;

    private PowerUpViewModel viewModel;

    public void Bind(PowerUpViewModel vm) {
        viewModel = vm;

        // Subscribe to ViewModel property change events (Data Binding)
        viewModel.OnChargesChanged += RenderCharges;
        viewModel.OnInteractableChanged += SetButtonInteractable;

        // Bind UI button click to ViewModel command
        removeMarkButton.onClick.AddListener(() => viewModel.ExecutePowerUpCommand());

        // Initial sync
        viewModel.UpdateState();
    }

    private void OnDestroy() {
        if (viewModel != null) {
            viewModel.OnChargesChanged -= RenderCharges;
            viewModel.OnInteractableChanged -= SetButtonInteractable;
        }
    }

    // -- View Logic
    private void RenderCharges(int charges) {
        for (int i = 0; i < chargeIcons.Length; i++) {
            if (chargeIcons[i] != null) {
                chargeIcons[i].gameObject.SetActive(i < charges);
            }
        }
    }

    private void SetButtonInteractable(bool isInteractable) {
        if (removeMarkButton != null) {
            removeMarkButton.interactable = isInteractable;
        }
    }
}
