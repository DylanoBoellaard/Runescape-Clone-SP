using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodcuttingLevelSystem
{
    // https://www.youtube.com/watch?v=kKCLMvsgAR0
    public event EventHandler WoodcuttingOnExperienceChanged;
    public event EventHandler WoodcuttingOnLevelChanged;


    // Level variables and stuff
    private const int maxLevel = 100;
    private const int baseExperience = 100;
    private const float experienceMultiplier = 1.1f;

    private int[] experiencePerLevel = new int[maxLevel];
    private int level;
    private int currentExperience;

    public WoodcuttingLevelSystem()
    {
        for (int level = 0; level < maxLevel; level++)
        {
            experiencePerLevel[level] = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));
        }
        level = 0;
        currentExperience = 0;
    }

    public void AddWoodcuttingExperience(int amount)
    {
        if (level < maxLevel)
        {
            currentExperience += amount; // TO DO: Show currentExperience on XP bar
            while (level < maxLevel && currentExperience >= GetWoodcuttingExperienceToNextLevel(level)) // Change GetExperiencetoNextLevel(level) to experiencePerLevel;[level] ?
            {
                // If enough experience to level up, level it up
                currentExperience -= GetWoodcuttingExperienceToNextLevel(level);
                level++;
                if (WoodcuttingOnLevelChanged != null) WoodcuttingOnLevelChanged(this, EventArgs.Empty);
            }
            if (WoodcuttingOnExperienceChanged != null) WoodcuttingOnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetWoodcuttingLevelNumber()
    {
        return level;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public float GetWoodcuttingExperienceNormalized()
    {
        if (level >= maxLevel)
        {
            return 1f;
        }
        else
        {
            return (float)currentExperience / GetWoodcuttingExperienceToNextLevel(level); // Converts currentExperience to float, then divides with experienceToNextLevel
        }
    }

    public int GetWoodcuttingExperience()
    {
        return currentExperience;
    }

    public int GetWoodcuttingExperienceToNextLevel(int level)
    {
        if (level < maxLevel)
        {
            return experiencePerLevel[level];
        }
        else
        { // Level Invalid
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }


    public bool WoodcuttingIsMaxLevel()
    {
        return WoodcuttingIsMaxLevel(level);
    }
    public bool WoodcuttingIsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
}