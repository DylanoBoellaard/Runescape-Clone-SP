using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiningLevelWindow : MonoBehaviour
{
    private TMP_Text LevelText;
    [SerializeField] Image experienceBarImage;
    private MiningLevelSystem levelSystem;
    private MiningLevelSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        LevelText = transform.Find("SkillWindowCanvas").Find("Mining").Find("MiningLevelText").GetComponent<TMP_Text>();

        // Add button functionality
        transform.Find("SkillWindowCanvas").Find("Mining").Find("MiningExperienceButton5").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddMiningExperience(5));
        //transform.Find("ExperienceButton50").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(50));
        //transform.Find("ExperienceButton500").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(500));
        //transform.Find("ExperienceButtonCustom").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(5000));
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        if (LevelText != null)
            LevelText.text = "Level\n" + (levelNumber + 1) + " / " + levelSystem.GetMaxLevel();
    }

    public void SetMiningLevelSystem(MiningLevelSystem levelsystem)
    {
        this.levelSystem = levelsystem;
    }

    public void SetMiningLevelSystemAnimated(MiningLevelSystemAnimated levelSystemAnimated)
    {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;


        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Subscribe to the changed events
        levelSystemAnimated.OnMiningExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnMiningLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Level changed, update the text
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e)
    {
        // Experience changed, update the bar size
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }
}
