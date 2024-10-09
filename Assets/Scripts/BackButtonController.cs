using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonController : MonoBehaviour
{
   [SerializeField] private Button backButton;
   [SerializeField] private GameObject menuOff;
    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            menuOff.SetActive(false);
        });
    }

}
