using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    private int experiencePoints = 0;
    private int maxExperience = 100; // Set your maximum experience value.
    private int currentLevel = 1;

    public void IncrementExperience(int amount)
    {
        experiencePoints += amount;

        // Check for level-up conditions.
        if (experiencePoints >= maxExperience)
        {
            LevelUp();
        }

        // You can add any additional logic here, such as checking for level-up conditions.
        Debug.Log($"Experience increased by {amount}. Total experience: {experiencePoints}");
    }

    private void LevelUp()
    {
        experiencePoints = 0; // Reset experience points after leveling up.
        //maxExperience *= 2; // Double the maximum experience for the next level.
        currentLevel++;

        // Add any additional logic for level-up effects or actions here.
        // Debug.Log($"Level up! New level: {currentLevel}");
    }

    public int GetExperience()
    {
        return experiencePoints;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public float GetExperiencePercentage()
    {
        if (maxExperience <= 0)
        {
            Debug.LogError("Max experience should be greater than 0 to calculate percentage.");
            return 0.0f;
        }

        return (float)experiencePoints / maxExperience;
    }

    // Additional methods and logic...
}