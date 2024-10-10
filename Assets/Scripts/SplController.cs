using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplController : MonoBehaviour
{
    [SerializeField] private Button weaponLibButton;
    [SerializeField] private GameObject weaponLibPopup;
    [SerializeField] private GameObject challengPopup;
    [SerializeField] private Button challengButton;
    [SerializeField] private Button gameSetupButton;
    [SerializeField] private GameObject splPopup;  
    [SerializeField] private Button creditButton;
    [SerializeField] private GameObject creditPopup;
    [SerializeField] private GameObject gameSetupPopup;
    [SerializeField] private GameObject line;
    [SerializeField] private Button soundButton;
    
    private void Start()
    {
        soundButton.onClick.AddListener(SoundEvent);
        weaponLibButton.onClick.AddListener(WeaponLib);
        challengButton.onClick.AddListener(Challenge);
        gameSetupButton.onClick.AddListener(GameSetup);
        weaponLibButton.onClick.AddListener(WeaponLib);
        creditButton.onClick.AddListener(CreditaEvent);
    }

    void SoundEvent()
    {
        line.SetActive(!line.activeSelf);
    }

    void CreditaEvent()
    {
        creditPopup.gameObject.SetActive(true);
    }

    void WeaponLib()
    {
        weaponLibPopup.gameObject.SetActive(true);
    }

    void Challenge()
    {
        challengPopup.gameObject.SetActive(true);
    }
    void GameSetup()
    {
        gameSetupPopup.gameObject.SetActive(true);
    }
}