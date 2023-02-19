using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int baseHealth = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = baseHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // TODO: Perform death animation and other death-related tasks
        PlayerManager player = GameObject.FindObjectOfType<PlayerManager>();
        player.GainExperience(100);
        Destroy(gameObject);
    }
}
