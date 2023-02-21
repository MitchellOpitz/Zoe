using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownUI : MonoBehaviour
{
    public enum AbilityKey
    {
        Q,
        W,
        E,
        R
    };

    public AbilityKey keyPressed;

    private bool isOnCooldown;
    private Image image; // Reference to the UI Image component
    private TextMeshProUGUI text;

    void Start()
    {
        GetCooldown();
        // Get references to the UI components
        image = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();

        // Hide the UI elements at start
        image.enabled = false;
        text.enabled = false;
    }

    void Update()
    {
        GetCooldown();
        // Hide the UI elements if the cooldown is over, show them otherwise
        if (isOnCooldown)
        {
            image.enabled = false;
            text.enabled = false;
        }
        else
        {
            image.enabled = true;
            text.enabled = true;
        }
    }

    private void GetCooldown()
    {
        switch (keyPressed)
        {
            case AbilityKey.Q:
                isOnCooldown = FindObjectOfType<QAbility>().isOnCooldown;
                break;
            case AbilityKey.E:
                isOnCooldown = FindObjectOfType<EAbility>().isOnCooldown;
                break;
            case AbilityKey.R:
                isOnCooldown = FindObjectOfType<RAbility>().isOnCooldown;
                break;
        }
    }
}
