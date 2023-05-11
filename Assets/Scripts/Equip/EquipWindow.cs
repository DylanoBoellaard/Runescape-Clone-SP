using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class EquipWindow : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private LevelSystem levelSystem;

    private void Awake()
    {
        transform.Find("EquipNoneBtn").GetComponent<Button>().onClick.AddListener(() => player.SetEquip("none"));

        transform.Find("EquipBronzeBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (levelSystem.GetLevelNumber() >= 4) // On visual level 5
            {
                player.SetEquip("bronze");
            } else
            {
                Debug.Log("Cannot equip Bronze Chestplate. Player level too low!");
            }
        });

        transform.Find("EquipIronBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (levelSystem.GetLevelNumber() >= 7) // On visual level 8
            {
                player.SetEquip("iron");
            } else
            {
                Debug.Log("Cannot equip Iron Chestplate. Player level too low!");
            }
        });
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }
}
