
using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;

public class HieuScript1 : MonoBehaviour
{
    public TMP_InputField nameInputField; // Sử dụng TMP_InputField thay vì InputField
    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(SavePlayerName);
    }

    public void SavePlayerName()
    {
        SharedData.PlayerName = nameInputField.text;
        Debug.Log("Player Name: " + SharedData.PlayerName);
    }
}

public static class SharedData
{
    public static string PlayerName = "";
}