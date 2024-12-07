using UnityEngine;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;

public class ColorMovement : MonoBehaviour
{
    [SerializeField] private Button backBtn;
    [SerializeField] private GameObject colorPop;
    void Start()
    {
        backBtn.onClick.AddListener(BackEvent);
    }

    void BackEvent()
    {
        
    }
}
