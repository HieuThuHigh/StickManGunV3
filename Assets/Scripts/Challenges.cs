using UnityEngine;
using UnityEngine.UI;


public class Challenges : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] popup;

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() =>
            {
                EventClicked(index);
            });
        }
    }
    void EventClicked(int index)
    {
        for (int i = 0; i < popup.Length; i++)
        {
            popup[i].SetActive(i == index );
           
        }
    }
}
