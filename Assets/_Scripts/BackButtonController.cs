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
    

    private int selectedLevel;

    public void SelectLevel(int level)
    {
        selectedLevel = level; 
          
    }

    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            menuOff.SetActive(false);
        
        });
        PlayButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Level" + selectedLevel);
        });
    }


}
