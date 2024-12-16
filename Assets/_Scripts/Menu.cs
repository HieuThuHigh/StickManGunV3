using System.Collections;
using System.Collections.Generic;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuPanel; // Panel chứa các chức năng
    public GameObject dimBackground;   // Lớp nền mờ
    void Start()
    {
        menuPanel.SetActive(false);
        dimBackground.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleMenu()
    {
        bool isPanelActive = menuPanel.activeSelf;

        if (isPanelActive)
        {
            // Ẩn panel và cho game tiếp tục
            menuPanel.SetActive(false);
            dimBackground.SetActive(false);
            Time.timeScale = 1f; // Game chạy lại bình thường
        }
        else
        {
            // Hiện panel và tạm dừng game
            menuPanel.SetActive(true);
            dimBackground.SetActive(true);
            Time.timeScale = 0f; // Dừng game
        }
        
    }

    public void Thoat()
    {
        SceneManager.LoadScene("Home");
    }
    // Hàm thoát game
    public void ExitGame()
    {
        // Lưu ý: chỉ hoạt động khi build ra game, không hoạt động trong editor
        //Application.Quit();
        
        SceneLoadManager.Instance.LoadSceneWithName("Home");
    }
}
