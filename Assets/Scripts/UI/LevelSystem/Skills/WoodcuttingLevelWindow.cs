using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WoodcuttingLevelWindow : MonoBehaviour
{
    private TMP_Text LevelText;
    [SerializeField] Image experienceBarImage;
    private WoodcuttingLevelSystem levelSystem;
    private WoodcuttingLevelSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        LevelText = transform.Find("SkillWindowCanvas").Find("Woodcutting").Find("WoodcuttingLevelText").GetComponent<TMP_Text>();

        // Add button functionality
        transform.Find("SkillWindowCanvas").Find("Woodcutting").Find("WoodcuttingExperienceButton5").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddWoodcuttingExperience(5));
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

    public void SetWoodcuttingLevelSystem(WoodcuttingLevelSystem levelsystem)
    {
        this.levelSystem = levelsystem;
    }

    public void SetWoodcuttingLevelSystemAnimated(WoodcuttingLevelSystemAnimated levelSystemAnimated)
    {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;


        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Subscribe to the changed events
        levelSystemAnimated.OnWoodcuttingExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnWoodcuttingLevelChanged += LevelSystemAnimated_OnLevelChanged;
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
