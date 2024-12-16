using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;
    [SerializeField] private GameObject soundOnObj;
    [SerializeField] private GameObject soundOffObj;

    private void Start()
    {
        continueBtn.onClick.AddListener(ContinueEvent);
        quitBtn.onClick.AddListener(QuitEvent);
        soundOn.onClick.AddListener(SoundOnEvent);
        soundOff.onClick.AddListener(SoundOffEvent);
    }

    private void SoundOffEvent()
    {
        Debug.LogError("a");
        soundOffObj.SetActive(false);
        soundOnObj.SetActive(true);
    }

    private void SoundOnEvent()
    {
        soundOnObj.SetActive(false);
        soundOffObj.SetActive(true);
    }

    private void QuitEvent()
    {
        SceneManager.LoadScene("SPL");
    }

    private void ContinueEvent()
    {
        Time.timeScale = 1;
        CanvasController.Instance.pauseObject.SetActive(false);
    }
}
