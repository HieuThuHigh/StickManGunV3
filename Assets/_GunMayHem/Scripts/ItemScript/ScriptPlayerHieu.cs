using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPlayerHieu : MonoBehaviour
{
    [SerializeField] private Button[] playerButtons;
    [SerializeField] private GameObject[] playerImages;
    [SerializeField] private Button choosePlayerButton;
    [SerializeField] private GameObject choosePlayerGameobject;

    private void Start()
    {
        for (int i = 0; i < playerButtons.Length; i++)
        {
            int index = i;
            playerButtons[i].onClick.AddListener(() => ShowPlayerImage(index));
        }
        HideAllPlayerImages();
        choosePlayerButton.onClick.AddListener(ChoosePlayer);
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
    }

    private void HideAllPlayerImages()
    {
        foreach (GameObject image in playerImages)
        {
            image.SetActive(false);
        }
    }
}
