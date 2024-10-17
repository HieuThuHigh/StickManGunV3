using System;
using UnityEngine;
using UnityEngine.UI;

public class CampaignPlayController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] gameObjectsPopup;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => EventClicked(index));
        }
    }
    void EventClicked(int index)
    {
        for (int i = 0; i < gameObjectsPopup.Length; i++)
        {
            gameObjectsPopup[i].SetActive(i == index);
        }
    }
}