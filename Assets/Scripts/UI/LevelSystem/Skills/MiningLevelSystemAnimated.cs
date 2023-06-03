using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class MiningLevelSystemAnimated
{
    public event EventHandler OnMiningExperienceChanged;
    public event EventHandler OnMiningLevelChanged;

    private MiningLevelSystem levelSystem;
    private bool isAnimating;
    private float updateTimer;
    private float updateTimerMax;

    private int level;
    private int currentExperience;
    private int animationExperience;

    public MiningLevelSystemAnimated(MiningLevelSystem levelSystem)
    {
        SetLevelSystem(levelSystem);
        updateTimerMax = .016f;

        // Runs the update every time the LevelSystemAnimated gets updated.
        // Provides a way to update without using monobehaviours and attaching it to a gameobject
        FunctionUpdater.Create(() => Update());
    }

    public void SetLevelSystem(MiningLevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        level = levelSystem.GetMiningLevelNumber();
        currentExperience = levelSystem.GetMiningExperience();

        levelSystem.MiningOnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.MiningOnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        { // Check if it's time to update
            updateTimer += Time.deltaTime;
            while (updateTimer > updateTimerMax)
            { // Time to update
                updateTimer -= updateTimerMax;
                UpdateAddExperience();
            }
        }
    }

    private void UpdateAddExperience()
    {
        if (level < levelSystem.GetMiningLevelNumber())
        {
            // Local level under target level
            AddExperience(); // Change this for faster animating bar https://youtu.be/kKCLMvsgAR0?t=1517
        }
        else
        {
            // Local level equals target level
            if (currentExperience < levelSystem.GetMiningExperience())
            {
                AddExperience();
            }
            else
            {
                isAnimating = false;
            }
        }
    }

    private void AddExperience()
    {
        //currentExperience++; OLD ANIMATED XP BAR

        int experienceToNextLevel = levelSystem.GetMiningExperienceToNextLevel(level);
        float experienceToAdd = experienceToNextLevel * 0.01f; // Adjust this value to change the animation speed

        currentExperience += Mathf.RoundToInt(experienceToAdd);

        if (currentExperience >= levelSystem.GetMiningExperienceToNextLevel(level))
        {
            level++;
            currentExperience = 0;
            if (OnMiningLevelChanged != null) OnMiningLevelChanged(this, EventArgs.Empty);
        }
        if (OnMiningExperienceChanged != null) OnMiningExperienceChanged(this, EventArgs.Empty);
    }
    public int GetLevelNumber()
    {
        return level;
    }
    public float GetExperienceNormalized()
    {
        if (levelSystem.MiningIsMaxLevel(level))
        {
            return 1f;
        }
        else
        {
            return (float)currentExperience / levelSystem.GetMiningExperienceToNextLevel(level); // Converts currentExperience to float, then divides with experienceToNextLevel
        }
    }
}
