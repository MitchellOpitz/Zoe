using TMPro;
using UnityEngine;

public class ExpUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI expText;

    private void Update()
    {
        // Update the level text to show the player's current level
        expText.text = "Exp: " + playerManager.Experience + " / " + playerManager.ExperienceToLevelUp;
    }
}
