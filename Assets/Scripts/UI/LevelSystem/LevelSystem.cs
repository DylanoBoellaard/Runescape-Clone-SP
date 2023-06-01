using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    // https://www.youtube.com/watch?v=kKCLMvsgAR0
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;


    // Level variables and stuff
    private const int maxLevel = 100;
    private const int baseExperience = 100;
    private const float experienceMultiplier = 1.1f;

    private int[] experiencePerLevel = new int[maxLevel];
    private int level;
    private int currentExperience;

    public LevelSystem()
    {
        for (int level = 0; level < maxLevel; level++)
        {
            experiencePerLevel[level] = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));
        }
        level = 0;
        currentExperience = 0;
    }

    public void AddExperience(int amount)
    {
        if (level < maxLevel)
        {
            currentExperience += amount; // TO DO: Show currentExperience on XP bar
            while (level < maxLevel && currentExperience >= GetExperienceToNextLevel(level)) // Change GetExperiencetoNextLevel(level) to experiencePerLevel;[level] ?
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
        if (level >= maxLevel)
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


    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }
    public bool IsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
}