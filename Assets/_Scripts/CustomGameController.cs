using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomGameController : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject mainPopup;
    [SerializeField] private GameObject otherPopup;
    [SerializeField] private Button boxButton;
    [SerializeField] private Button heartButton;
    [SerializeField] private Image greenImageBox;
    [SerializeField] private Image redImageBox;
    [SerializeField] private Image greenImageHeart;
    [SerializeField] private Image gredImageHeart;

    private void Start()
    {
        EventClick();
    }

    void EventClick()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[index].onClick.AddListener(() =>
            {
                // Đổi màu trước, sau đó reset lại các màu khác
                if (buttons[index].TryGetComponent(out Image clickedImage))
                {
                    ResetButtonColors(); // Reset tất cả nút về trắng trước
                    ColorUtility.TryParseHtmlString("#FF6600", out var color);
                    clickedImage.color = color; // Sau đó đổi màu của nút hiện tại
                }

                // Hiển thị popup tùy theo nút bấm
                if (index == 2)
                {
                    mainPopup.SetActive(false);
                    otherPopup.SetActive(true);
                }
                else
                {
                    mainPopup.SetActive(true);
                    otherPopup.SetActive(false);
                }
            });
        }

        // Gán sự kiện cho nút boxButton
        boxButton.onClick.AddListener(ToggleBoxImages);

        // Gán sự kiện cho nút heartButton
        heartButton.onClick.AddListener(ToggleHeartImages);
    }



    void ResetButtonColors()
    {
        foreach (var button in buttons)
        {
            if (button.TryGetComponent(out Image image))
            {
                image.color = Color.white;
            }
        }
    }

    void ToggleBoxImages()
    {
        Debug.LogError("BoxClicked");
        ToggleImages(greenImageBox, redImageBox);
    }

    void ToggleHeartImages()
    {
        Debug.LogError("HeartClicked");
        ToggleImages(greenImageHeart, gredImageHeart);
    }

    void ToggleImages(Image greenImage, Image redImage)
    {
        if (greenImage.gameObject.activeSelf)
        {
            greenImage.gameObject.SetActive(false);
            redImage.gameObject.SetActive(true);
        }
        else
        {
            greenImage.gameObject.SetActive(true);
            redImage.gameObject.SetActive(false);
        }
    }
}