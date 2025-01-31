﻿using System.Collections;
using System.Collections.Generic;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject menuOff;
    [SerializeField] private Button PlayButton;


    private int _selectedLevel;

    public void SelectLevel(int level)
    {
        _selectedLevel = level;
    }

    void Start()
    {
        if (backButton)
        {
            backButton.onClick.AddListener(() => { menuOff.SetActive(false); });
        }

        // if (PlayButton)
        // {
        //     PlayButton.onClick.AddListener(() => { SceneLoadManager.Instance.LoadSceneWithName("Gameplay"); });
        // }
    }
}