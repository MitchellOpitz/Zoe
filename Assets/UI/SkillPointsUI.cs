using TMPro;
using UnityEngine;

public class SkillPointsUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI skillPointsText;

    private void Update()
    {
        // Update the level text to show the player's current level
        skillPointsText.text = "Skill Points: " + playerManager.SkillPoints;
    }
}
