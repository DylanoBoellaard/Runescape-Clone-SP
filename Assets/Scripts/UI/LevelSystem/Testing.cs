using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private LevelWindow levelWindow;
    [SerializeField] private PlayerController player;
    [SerializeField] private EquipWindow equipWindow;
    [SerializeField] private WoodcuttingLevelWindow woodcuttingLevelWindow;
    [SerializeField] private MiningLevelWindow miningLevelWindow;
    private void Awake()
    {
        LevelSystem levelSystem = new LevelSystem();
        WoodcuttingLevelSystem woodcuttingLevelSystem = new WoodcuttingLevelSystem();
        MiningLevelSystem miningLevelSystem = new MiningLevelSystem();

        /* For testing the level and experience system
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 0
        levelSystem.AddExperience(50);
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 0
        levelSystem.AddExperience(60);
        Debug.Log(levelSystem.GetLevelNumber()); // Expected result: 1*/

        // Default levelsystem
        levelWindow.SetLevelSystem(levelSystem);
        equipWindow.SetLevelSystem(levelSystem);

        LevelSystemAnimated levelSystemAnimated = new LevelSystemAnimated(levelSystem);
        levelWindow.SetLevelSystemAnimated(levelSystemAnimated);
        player.SetLevelSystemAnimated(levelSystemAnimated);

        // Woodcutting levelsystem
        woodcuttingLevelWindow.SetWoodcuttingLevelSystem(woodcuttingLevelSystem);
        WoodcuttingLevelSystemAnimated woodcuttingLevelSystemAnimated = new WoodcuttingLevelSystemAnimated(woodcuttingLevelSystem);
        woodcuttingLevelWindow.SetWoodcuttingLevelSystemAnimated(woodcuttingLevelSystemAnimated);

        // Mining levelsystem
        miningLevelWindow.SetMiningLevelSystem(miningLevelSystem);
        MiningLevelSystemAnimated miningLevelSystemAnimated = new MiningLevelSystemAnimated(miningLevelSystem);
        miningLevelWindow.SetMiningLevelSystemAnimated(miningLevelSystemAnimated);
    }
}