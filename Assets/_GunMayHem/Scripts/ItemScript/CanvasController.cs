using System;
using System.Collections;
using System.Collections.Generic;
using GameTool.Assistants.DesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : SingletonMonoBehaviour<CanvasController>
{
    [SerializeField] private Button pauseBtn;
    public  GameObject pauseObject;

    private void Start()
    {
        pauseBtn.onClick.AddListener(PauseEvent);
    }

    private void PauseEvent()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0;
    }
}
