using GameToolSample.GameDataScripts.Scripts;
using TMPro;
using UnityEngine;

public class CustomGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI freezeCountText;
    [SerializeField] private TextMeshProUGUI shieldCountText;
    [SerializeField] private TextMeshProUGUI jumpCountText;

    private void Update()
    {
        freezeCountText.text = GameData.Freeze.ToString();
        shieldCountText.text = GameData.Shield.ToString();
        jumpCountText.text = GameData.Jump.ToString();
    }
}