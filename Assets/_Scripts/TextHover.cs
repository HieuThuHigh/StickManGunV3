
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour
{
    
    // Tham chiếu tới EventTrigger để xử lý các sự kiện
    [SerializeField] private EventTrigger eventTrigger;
    
    // Tham chiếu tới TextMeshProUGUI để thay đổi màu văn bản
    [SerializeField] TextMeshProUGUI text;
    
    private void Start()
    {
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
        if (ColorUtility.TryParseHtmlString("#00EFFF", out Color newColor))
        {
            text.color = newColor;
        }
    }

    private void OnPointerExit(BaseEventData arg0)
    {
        // Đặt lại màu text thành màu trắng khi không hover
        text.color = Color.white; 
    }
   
}