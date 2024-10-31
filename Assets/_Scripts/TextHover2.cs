using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextHover2 : MonoBehaviour
{
    // Tham chiếu tới EventTrigger để xử lý các sự kiện
    [SerializeField] private EventTrigger eventTrigger;

    // Tham chiếu tới TextMeshProUGUI để thay đổi màu văn bản
    [SerializeField] private TextMeshProUGUI text;

    // Tham chiếu tới Image để thay đổi màu nền của button
    [SerializeField] private Image buttonImage;

    private Color originalTextColor;
    private Color originalButtonColor;

    private void Start()
    {
        // Lưu trữ màu gốc của text và button
        originalTextColor = text.color;
        originalButtonColor = buttonImage.color;

        // Thêm sự kiện khi con trỏ chuột di chuyển vào
        AddEventTrigger(EventTriggerType.PointerEnter, OnPointerEnter);
        // Thêm sự kiện khi con trỏ chuột rời đi
        AddEventTrigger(EventTriggerType.PointerExit, OnPointerExit);
    }

    private void AddEventTrigger(EventTriggerType eventID, UnityEngine.Events.UnityAction<BaseEventData> action)
    {
        // Tạo mới một EventTrigger.Entry và gán eventID
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventID };
        // Thêm phương thức callback vào sự kiện
        entry.callback.AddListener(action);
        // Thêm sự kiện vào danh sách trigger
        eventTrigger.triggers.Add(entry);
    }

    private void OnPointerEnter(BaseEventData arg0)
    {
        if (ColorUtility.TryParseHtmlString("#00EFFF", out Color textColor))
        {
            text.color = textColor;
        }
        if (ColorUtility.TryParseHtmlString("#A7EAAC", out Color buttonColor))
        {
            buttonImage.color = buttonColor; // Thay đổi màu nền của button
        }
    }

    private void OnPointerExit(BaseEventData arg0)
    {
        // Đặt lại màu text và màu nền của button về màu gốc khi không hover
        text.color = originalTextColor;
        buttonImage.color = originalButtonColor;
    }
}