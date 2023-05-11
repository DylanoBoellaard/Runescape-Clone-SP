using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private PlayerController player;
    [SerializeField] private EquipWindow equipWindow;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();

        /* For testing the level and experience system
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 0
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 0
        levelSystem.AddExperience(60);
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 1*/

        levelWindow.SetLevelSystem(levelSystem);
        equipWindow.SetLevelSystem(levelSystem);

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);
    }
}
