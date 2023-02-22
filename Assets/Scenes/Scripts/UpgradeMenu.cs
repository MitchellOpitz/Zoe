using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradePanel;
    public Button closeButton;
    public GameObject player;

    private bool menuOpen;

    void Start()
    {
        // Add a listener to the close button to hide the upgrade panel
        closeButton.onClick.AddListener(HideUpgradePanel);
    }

    void Update()
    {
        // Check for input to open the upgrade panel
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!menuOpen)
            {
                OpenUpgradePanel();
            } else
            {
                Debug.Log("Closed.");
                HideUpgradePanel();
            }
        } 
    }

    public void OpenUpgradePanel()
    {
        // Pause gameplay
        Time.timeScale = 0;

        // Show the upgrade panel
        upgradePanel.SetActive(true);

        // Disable player movement and abilities
        ToggleAbilities(false);
    }

    void HideUpgradePanel()
    {
        // Hide the upgrade panel
        upgradePanel.SetActive(false);

        // Resume gameplay
        Time.timeScale = 1;

        // Enable player movement and abilities
        ToggleAbilities(true);
    }

    private void ToggleAbilities(bool toggle)
    {
        player.GetComponent<Movement>().enabled = toggle;
        player.GetComponent<QAbility>().enabled = toggle;
        player.GetComponent<EAbility>().enabled = toggle;
        player.GetComponent<RAbility>().enabled = toggle;
    }
}
