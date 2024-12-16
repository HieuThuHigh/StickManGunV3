using System.Collections;
using System.Collections.Generic;
using _GunMayHem.Gameplay;
using GameTool.Assistants.DesignPattern;
using TMPro;
using UnityEngine;

public class VictoryReward : SingletonMonoBehaviour<VictoryReward>
{
    [SerializeField] private GameObject freezeObjectImage;
    [SerializeField] private GameObject shieldObjectImage;
    [SerializeField] private GameObject jumpObjectImage;
        
    [SerializeField] private TextMeshProUGUI soluongText;

    public void UIReward(int reward, int typeItem)
    {
        freezeObjectImage.SetActive(false);
        shieldObjectImage.SetActive(false);
        jumpObjectImage.SetActive(false);
        
        soluongText.text = reward.ToString();

        if (typeItem == 0)
        {
            freezeObjectImage.SetActive(true);
        }
        else if(typeItem == 1)
        {
            shieldObjectImage.SetActive(true);
        }
        else
        {
            jumpObjectImage.SetActive(false);
        }
    }
    
}
