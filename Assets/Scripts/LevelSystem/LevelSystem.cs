using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    // https://www.youtube.com/watch?v=kKCLMvsgAR0
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private static readonly int[] experiencePerLevel = new[] { 100, 150, 200, 250, 300, 375, 450, 575, 650, 725 };
    private int level;
    private int currentExperience;

    public LevelSystem()
    {
        level = 0;
        currentExperience = 0;
    }

    public void AddExperience(int amount)
    {
        if (!IsMaxLevel())
        {
            currentExperience += amount;
            while (!IsMaxLevel() && currentExperience >= GetExperienceToNextLevel(level))
            {
                // If enough experience to level up, level it up
                currentExperience -= GetExperienceToNextLevel(level);
                level++;
                if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
            }
            if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float GetExperienceNormalized()
    {
        if (IsMaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)currentExperience / GetExperienceToNextLevel(level); // Converts currentExperience to float, then divides with experienceToNextLevel
        }
    }

    public int GetExperience()
    {
        return currentExperience;
    }

    public int GetExperienceToNextLevel(int level)
    {
        if (level < experiencePerLevel.Length)
        {
            return experiencePerLevel[level];
        }
        else
        { // Level Invalid
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }


    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }
    public bool IsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
}