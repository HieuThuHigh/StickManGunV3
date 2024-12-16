using System;
using GameTool.Audio.Scripts;
using GameToolSample.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SplController : MonoBehaviour
{
    [SerializeField] private Button weaponLibButton;
    [SerializeField] private GameObject weaponLibPopup;
    [SerializeField] private GameObject challengPopup;
    [SerializeField] private Button challengButton;
    [SerializeField] private Button customGameButton;
    [SerializeField] private GameObject splPopup;
    [SerializeField] private Button creditButton;
    [SerializeField] private GameObject creditPopup;
    [SerializeField] private Button compButton;
    [SerializeField] private GameObject compPopup;
    [SerializeField] private GameObject customGamePopup;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private Button btnSoundButton;
    [FormerlySerializedAs("CampaignPlay")][SerializeField] private GameObject campaignPlay;

    private void Start()
    {
        //btnSoundButton.onClick.AddListener(SoundEvent);
        challengButton.onClick.AddListener(Challenge);
        customGameButton.onClick.AddListener(GameSetup);
        creditButton.onClick.AddListener(CreditaEvent);
        compButton.onClick.AddListener(ComplainEvent);
        AudioManager.Instance.PlayMusic(eMusicName.MusicMain);
    }
    void SoundEvent()
    {
        line.SetActive(!line.activeSelf);
    }

    void CreditaEvent()
    {
        Debug.LogWarning("Credit");
        creditPopup.gameObject.SetActive(true);
    }

    void WeaponLib()
    {
        Debug.LogWarning("Weapon");
        weaponLibPopup.gameObject.SetActive(true);
    }

    void Challenge()
    {
        Debug.LogWarning("Challenge");
        challengPopup.gameObject.SetActive(true);
    }
    void GameSetup()
    {
        Debug.LogWarning("GameSetup");
        customGamePopup.gameObject.SetActive(true);
    }

    void ComplainEvent()
    {
        Debug.LogWarning("Complain");
        compPopup.gameObject.SetActive(true);
    }
    public void CampaignPlays()
    {
        Debug.LogWarning("CampaignPlay");
        campaignPlay.gameObject.SetActive(true);
    }
    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }
}