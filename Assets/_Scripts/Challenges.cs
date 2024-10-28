using UnityEngine;
using UnityEngine.UI;


public class Challenges : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] popup;
    private Color _defaultColor = Color.clear;
    private Color _selectedColor = Color.yellow;
    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => EventClicked(index));
        }

        ResetButtonColors();
    }
    void ResetButtonColors()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].image.color = _defaultColor;
        }
    }

    void EventClicked(int index)
    {
        ResetButtonColors();
        for (int i = 0; i < popup.Length; i++)
        {
            popup[i].SetActive(i == index);
        }

        buttons[index].image.color = _selectedColor;
    }
}
