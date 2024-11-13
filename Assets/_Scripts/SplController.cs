using System;
using UnityEngine;
using UnityEngine.Serialization;
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
    [SerializeField] private Button compButton;
    [SerializeField] private GameObject compPopup;
    [SerializeField] private GameObject gameSetupPopup;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject soundButton;
    [FormerlySerializedAs("CampaignPlay")] [SerializeField] private GameObject campaignPlay;

    private void Start()
    {
        weaponLibButton.onClick.AddListener(WeaponLib);
        challengButton.onClick.AddListener(Challenge);
        gameSetupButton.onClick.AddListener(GameSetup);
        weaponLibButton.onClick.AddListener(WeaponLib);
        creditButton.onClick.AddListener(CreditaEvent);
        compButton.onClick.AddListener(ComplainEvent);

    }
    void SoundEvent()
    {
        line.SetActive(!line.activeSelf);
    }

    void CreditaEvent()
    {
        Debug.LogError("Credit");
        creditPopup.gameObject.SetActive(true);
    }

    void WeaponLib()
    {
        Debug.LogError("Weapon");
        weaponLibPopup.gameObject.SetActive(true);
    }

    void Challenge()
    {
        Debug.LogError("Challenge");
        challengPopup.gameObject.SetActive(true);
    }
    void GameSetup()
    {
        Debug.LogError("GameSetup");
        gameSetupPopup.gameObject.SetActive(true);
    }

    void ComplainEvent()
    {
        Debug.LogError("Complain");
        compPopup.gameObject.SetActive(true);
    }
   public  void CampaignPlays()
    {
        Debug.LogError("CampaignPlay");
        campaignPlay.gameObject.SetActive(true);
    }
}