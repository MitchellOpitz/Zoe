using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI levelText;

    private void Update()
    {
        // Update the level text to show the player's current level
        levelText.text = "Level: " + playerManager.Level;
    }
}
