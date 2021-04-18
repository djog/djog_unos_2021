using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [Header("Border")]
    public Image borderImage;
    public Color normalColor;
    public Color selectedColor;

    [Header("Display")]
    public Text numberText;
    public Image iconImage;

    public void SetDisplay(int number, Sprite sprite, bool isSelected)
    {
        numberText.text = number.ToString();
        if (sprite)
        {
            iconImage.enabled = true;
            iconImage.sprite = sprite;
        } else {
            iconImage.enabled = false;
        }

        if (!isSelected)
        {
            borderImage.color = normalColor;
        }
        else
        {
            borderImage.color = selectedColor;
        }
    }
}