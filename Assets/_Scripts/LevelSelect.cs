using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
   
    public GameObject playPanel; 

    private int selectedLevel;

    public void SelectLevel(int level)
    {
        selectedLevel = level; // Gán màn chơi đã chọn
        
        playPanel.SetActive(true); // Hiện panel chơi
    }

    public void PlaySelectedLevel()
    {
        SceneManager.LoadScene("Level" + selectedLevel); // Chuyển đến scene đã chọn
    }

    public void GoBack()
    {
        playPanel.SetActive(false); // Ẩn panel chơi
       // Hiện lại panel chọn màn chơi
    }
}