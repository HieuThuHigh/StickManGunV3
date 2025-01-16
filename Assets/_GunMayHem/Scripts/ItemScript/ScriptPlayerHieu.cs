using System;
using System.Collections;
using System.Collections.Generic;
using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptPlayerHieu : SingletonMonoBehaviour<ScriptPlayerHieu>
{
    [SerializeField] private Button[] playerButtons;
    [SerializeField] private GameObject[] playerImages;
    [SerializeField] private Button choosePlayerButton;
    [SerializeField] private GameObject choosePlayerGameobject;
    
    [SerializeField] private Button[] mapButtons;
    [SerializeField] private GameObject[] mapImages;
    [SerializeField] private Button chooseMapButton;
    [SerializeField] private GameObject chooseMapGameobject;
    [SerializeField] private Button playButton;
    [SerializeField] private Button buttonCong;
    
    private void Start()
    {
        PlayerFunction();
        MapFunction();
        playButton.onClick.AddListener(PlayEvent);
        buttonCong.onClick.AddListener(BUttonCongEvent);
    }

    private void BUttonCongEvent()
    {
        GameData.Freeze++;
        GameData.Jump++;
        GameData.Shield++;
    }

    private void PlayEvent()
    {
        for (int i = 0; i < mapImages.Length; i++)
        {
            if (mapImages[i].activeSelf) // Kiểm tra map nào đang được hiển thị
            {
                switch (i)
                {
                    case 0:
                        SceneManager.LoadScene("Gameplay");
                        break;
                    case 1:
                        SceneManager.LoadScene("Scenes2");
                        break;
                    case 2:
                        SceneManager.LoadScene("Scenes3");
                        break;
                    case 3:
                        SceneManager.LoadScene("Scenes4");
                        break;
                    case 4:
                        SceneManager.LoadScene("Scenes5");
                        break;
                    case 5:
                        SceneManager.LoadScene("Scenes6");
                        break;
                    case 6:
                        SceneManager.LoadScene("Scenes7");
                        break;
                    case 7:
                        SceneManager.LoadScene("Scenes8");
                        break;
                    case 8:
                        SceneManager.LoadScene("Scenes9");
                        break;
                    case 9:
                        SceneManager.LoadScene("Scenes10");
                        break;
                    case 10:
                        SceneManager.LoadScene("Scenes11");
                        break;
                    case 11:
                        SceneManager.LoadScene("Scenes12");
                        break;
                }
                return; // Thoát khỏi hàm sau khi load scene
            }
        }
        

    }


    void PlayerFunction()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            int index = i;
            playerButtons[i].onClick.AddListener(() => ShowPlayerImage(index));
        }
        HideAllPlayerImages();
        choosePlayerButton.onClick.AddListener(ChoosePlayer);
    }
    void MapFunction()
    {
        for (int i = 0; i < mapButtons.Length; i++)
        {
            int index = i;
            mapButtons[i].onClick.AddListener(() => ShowMapImage(index));
        }
        HideAllMapImages();
        chooseMapButton.onClick.AddListener(ChooseMap);
    }

    private void ChoosePlayer()
    {
        choosePlayerGameobject.SetActive(true);
    }

    private void ShowPlayerImage(int activeIndex)
    {
        for (int i = 0; i < playerImages.Length; i++)
        {
            playerImages[i].SetActive(i == activeIndex);
        }

        // Lưu chỉ số Player được chọn vào PlayerPrefs
        PlayerPrefs.SetInt("SelectedPlayer", activeIndex);
        PlayerPrefs.Save();
    }


    private void HideAllPlayerImages()
    {
        foreach (GameObject image in playerImages)
        {
            image.SetActive(false);
        }
    }
    
    private void ChooseMap()
    {
        chooseMapGameobject.SetActive(true);
    }

    private void ShowMapImage(int activeIndex)
    {
        for (int i = 0; i < mapImages.Length; i++)
        {
            mapImages[i].SetActive(i == activeIndex);
        }
    }

    private void HideAllMapImages()
    {
        foreach (GameObject image in mapImages)
        {
            image.SetActive(false);
        }
    }
}
