using GameToolSample.GameDataScripts.Scripts;
using TMPro;
using UnityEngine;

public class CustomGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feezeCountText;

    private void Update()
    {
        feezeCountText.text = GameData.Freeze.ToString();
    }
}