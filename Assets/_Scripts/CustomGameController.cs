using System;
using System.Collections;
using System.Collections.Generic;
using GameToolSample.GameDataScripts.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomGameController : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI feezeCountText;

    private void Update()
    {
        feezeCountText.text = GameData.Freeze.ToString();
    }

  
}