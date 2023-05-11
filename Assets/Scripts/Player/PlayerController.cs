using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // LevelSystemAnimated stuff for XP bar
    private LevelSystemAnimated levelSystemAnimated;

    // Audio stuff for playing sound when player levels up
    public AudioSource audioSource;
    public AudioClip LevelUpSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated)
    {
        this.levelSystemAnimated = levelSystemAnimated;

        levelSystemAnimated.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        // Do something when the player reaches a new level (a.k.a. Runescape's level up sound + UI)
        Debug.Log("The Player has reached a new level!");
        PlayLevelUpSound(); // Create the function below
    }

    void PlayLevelUpSound()
    {
        audioSource.PlayOneShot(LevelUpSound);
    }

    public void SetEquip(string equipment)
    {
        switch (equipment)
        {
            case "none":
                Debug.Log("Equipped nothing");
                break;
            case "bronze":
                Debug.Log("Equipped Bronze Chestplate");
                break;
            case "iron":
                Debug.Log("Equipped Iron Chestplate");
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
}
