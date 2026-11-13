using UnityEngine;

[CreateAssetMenu(fileName = "MarkFlyweight", menuName = "Scriptable Objects/MarkFlyweight")]
public class MarkFlyweight : ScriptableObject {
    [Header("Visuals")]
    public string symbolIdentifier; // "X", "O", "Hazard"
    public Sprite symbolSprite;
    public Color displayColor;

    [Header("Audio")]
    public AudioClip placementSound;
    public AudioClip removalSound;
}
