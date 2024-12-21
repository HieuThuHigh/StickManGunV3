using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseBtn : MonoBehaviour
{
    [SerializeField] private Button closeBtn;
    void Start()
    {
        closeBtn.onClick.AddListener(CloseEvent);
    }

    private void CloseEvent()
    {
        transform.parent.gameObject.SetActive(false);
    }

    
}
