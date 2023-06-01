using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class OpenClose : MonoBehaviour
{
    [SerializeField] private GameObject LevelWindow;
    [SerializeField] private GameObject EquipWindow;
    [SerializeField] private GameObject SkillWindow;

    [SerializeField] private Button DevToolsButton;
    [SerializeField] private Button SkillsButton;

    private void Start()
    {
        // Seperate functions cause otherwise DevToolsButton panels won't hide anymore because of erros and conflicts :)
        DevToolsButton.onClick.AddListener(() => OpenCloseDevtools());
        SkillsButton.onClick.AddListener(() => OpenCloseSkillWindow());
    }

    private void Awake()
    {
        LevelWindow.SetActive(false);
        EquipWindow.SetActive(false);
        SkillWindow.SetActive(false);
    }

    public void OpenCloseDevtools()
    {
        if (LevelWindow.activeSelf == false && EquipWindow.activeSelf == false)
        {       
                    LevelWindow.SetActive(true);
                    EquipWindow.SetActive(true);
        }
        else
        {
                    LevelWindow.SetActive(false);
                    EquipWindow.SetActive(false);
        }
    }

    public void OpenCloseSkillWindow()
    {
        if (SkillWindow.activeSelf == false)
        {
            SkillWindow.SetActive(true);
        } else
        {
            SkillWindow.SetActive(false);
        }
    }
}
