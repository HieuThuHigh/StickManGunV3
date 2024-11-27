using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMovement : MonoBehaviour
{
   [SerializeField] private Button pauseBtn;
   [SerializeField] private Button continueBtn;
   [SerializeField] private Button quitBtn;
   [SerializeField] private GameObject framePausePanel;
   [SerializeField] private GameObject soundBtnOff;
   [SerializeField] private Button soundBtn;

   private void Start()
   {
      pauseBtn.onClick.AddListener(PauseEvent);
      quitBtn.onClick.AddListener(QuitEvent);
      continueBtn.onClick.AddListener(ContinueEvent);
      soundBtn.onClick.AddListener(SoundEvent);
   }

   private void SoundEvent()
   {
      
      if (soundBtnOff == true)
      {
         soundBtnOff.SetActive(false);
      }
      else
      {
         soundBtnOff.SetActive(true);
      }
   }

   private void ContinueEvent()
   {
      framePausePanel.SetActive(false);
      Time.timeScale = 1;
   }

   private void QuitEvent()
   {     
      LoadingSPL.Instance.homePopup.SetActive(true);
      framePausePanel.SetActive(false);
   }

   private void PauseEvent()
   {
      Debug.LogError("da pause");
      framePausePanel.SetActive(true);
      Time.timeScale = 0;
   }
}
