using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public PlayerManager playerManager;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        // Update the level text to show the player's current level
        scoreText.text = "Score: " + playerManager.Score;
    }
}
