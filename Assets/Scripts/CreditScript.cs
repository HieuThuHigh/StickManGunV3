using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class CreditScript : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] buttonPopup;

    private Button lastClickedButton; // Button được click trước đó
    private Color defaultColor = new Color(1f, 1f, 1f, 0.4f); // Màu gốc (alpha 0.5f)

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttons[index]));
        }
    }

    // Sự kiện khi button được click
    void OnButtonClick(Button button)
    {
        // Nếu đã có button nào được click trước đó, đưa nó về màu gốc
        if (lastClickedButton != null && lastClickedButton != button)
        {
            Image lastButtonImage = lastClickedButton.GetComponent<Image>();
            if (lastButtonImage != null)
            {
                lastButtonImage.color = defaultColor; // Đưa màu của button cũ về màu gốc
            }
        }

        // Đổi màu button hiện tại
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color clickedColor = buttonImage.color;
            clickedColor.a = 1f; // Alpha = 255 (đầy đủ hiển thị)
            buttonImage.color = clickedColor;
        }

        // Lưu lại button hiện tại để xử lý lần sau
        lastClickedButton = button;

        // Thực hiện các hành động khác khi button được click
        for (int i = 0; i < buttonPopup.Length; i++)
        {
            // Giả sử bạn muốn làm gì đó với buttonPopup ở đây.
        }
    }
}