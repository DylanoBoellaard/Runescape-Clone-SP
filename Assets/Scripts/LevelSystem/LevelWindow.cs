using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    private TMP_Text LevelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    private LevelSystemAnimated levelSystemAnimated;

    private void Awake()
    {
        LevelText = transform.Find("LevelText").GetComponent<TMP_Text>();
        experienceBarImage = transform.Find("ExperienceBar").Find("Bar").GetComponent<Image>();

        // Add button functionality
        transform.Find("ExperienceButton5").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(5));
        transform.Find("ExperienceButton50").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(50));
        transform.Find("ExperienceButton500").GetComponent<Button>().onClick.AddListener(() => levelSystem.AddExperience(500));
    }

    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int levelNumber)
    {
        LevelText.text = "Level\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelsystem)
    {
        this.levelSystem = levelsystem;
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;

        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Subscribe to the changed events
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
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
