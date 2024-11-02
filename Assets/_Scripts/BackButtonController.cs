using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
   [SerializeField] private Button backButton;
   [SerializeField] private GameObject menuOff; 
    [SerializeField] private Button PlayButton;
  // [SerializeField] private GameObject menuOff;
    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            menuOff.SetActive(false);
        });
        PlayButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("level1"); // Tải lại scene hiện tại
        });
    }


}
