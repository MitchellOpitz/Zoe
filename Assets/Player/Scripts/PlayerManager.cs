using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int Level { get; private set; } = 1;
    public int SkillPoints { get; private set; } = 0;
    public int Experience { get; private set; } = 0;
    public int ExperienceToLevelUp { get; private set; } = 100;
    public int Score { get; private set; } = 0;

    public void AddSkillPoints(int amount)
    {
        SkillPoints += amount;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }

    public void GainExperience(int amount)
    {
        Experience += amount;

        // Check if player has enough experience to level up
        if (Experience >= ExperienceToLevelUp)
        {
            // Level up and reset experience
            Level++;
            GameObject.FindObjectOfType<EnemySpawner>().ResetSpawner(Level);
            SkillPoints++;
            Experience -= ExperienceToLevelUp;
            ExperienceToLevelUp = Mathf.RoundToInt(ExperienceToLevelUp * 1.1f); // Increase experience needed to level up by 10%
        }
    }
}
