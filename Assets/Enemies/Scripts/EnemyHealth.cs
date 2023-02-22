using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int baseHealth = 10;
    public Image healthBar;
    public Image healthBarFill;

    private int currentHealth;

    private void Start()
    {
        currentHealth = baseHealth;
    }

    private void Update()
    {
        // Update health bar position and rotation to follow enemy
        healthBar.transform.position = transform.position + Vector3.up;
        healthBar.transform.rotation = Camera.main.transform.rotation;
        healthBarFill.transform.position = transform.position + Vector3.up;
        healthBarFill.transform.rotation = Camera.main.transform.rotation;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        // Update health bar fill amount based on enemy health
        float healthPercent = (float)currentHealth / (float)baseHealth;
        healthBarFill.fillAmount = healthPercent;
    }

    private void Die()
    {
        // TODO: Perform death animation and other death-related tasks
        PlayerManager player = GameObject.FindObjectOfType<PlayerManager>();
        player.GainExperience(100);
        player.AddScore(100);
        Destroy(gameObject);
    }
}
