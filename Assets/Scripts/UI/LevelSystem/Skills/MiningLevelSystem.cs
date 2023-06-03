using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLevelSystem
{
    // https://www.youtube.com/watch?v=kKCLMvsgAR0
    public event EventHandler MiningOnExperienceChanged;
    public event EventHandler MiningOnLevelChanged;


    // Level variables and stuff
    private const int maxLevel = 100;
    private const int baseExperience = 100;
    private const float experienceMultiplier = 1.1f;

    private int[] experiencePerLevel = new int[maxLevel];
    private int level;
    private int currentExperience;

    public MiningLevelSystem()
    {
        for (int level = 0; level < maxLevel; level++)
        {
            experiencePerLevel[level] = Mathf.RoundToInt(baseExperience * Mathf.Pow(experienceMultiplier, level));
        }
        level = 0;
        currentExperience = 0;
    }

    public void AddMiningExperience(int amount)
    {
        if (level < maxLevel)
        {
            currentExperience += amount; // TO DO: Show currentExperience on XP bar
            while (level < maxLevel && currentExperience >= GetMiningExperienceToNextLevel(level)) // Change GetExperiencetoNextLevel(level) to experiencePerLevel;[level] ?
            {
                // If enough experience to level up, level it up
                currentExperience -= GetMiningExperienceToNextLevel(level);
                level++;
                if (MiningOnLevelChanged != null) MiningOnLevelChanged(this, EventArgs.Empty);
            }
            if (MiningOnExperienceChanged != null) MiningOnExperienceChanged(this, EventArgs.Empty);
        }
    }

    public int GetMiningLevelNumber()
    {
        return level;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public float GetMiningExperienceNormalized()
    {
        if (level >= maxLevel)
        {
            return 1f;
        }
        else
        {
            return (float)currentExperience / GetMiningExperienceToNextLevel(level); // Converts currentExperience to float, then divides with experienceToNextLevel
        }
    }

    public int GetMiningExperience()
    {
        return currentExperience;
    }

    public int GetMiningExperienceToNextLevel(int level)
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


    public bool MiningIsMaxLevel()
    {
        return MiningIsMaxLevel(level);
    }
    public bool MiningIsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }
}