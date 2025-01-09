using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptPlayerHieu : MonoBehaviour
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
    
    
    private void Start()
    {
        PlayerFunction();
        MapFunction();
        playButton.onClick.AddListener(PlayEvent);
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
                }
                return; // Thoát khỏi hàm sau khi load scene
            }
        }
        

        Debug.LogError("Please choose a map before playing!"); // Hiển thị lỗi nếu chưa chọn map
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
        Debug.LogError("nguu");
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
