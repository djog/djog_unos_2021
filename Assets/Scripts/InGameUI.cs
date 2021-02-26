using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TextItem
{
    public string key;
    public Text textObject;
}

public class InGameUI : MonoBehaviour
{
    private static InGameUI instance;
    public TextItem[] textItems;
    
    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.LogError("Multiple instances of InGameUI!");
        }
    }

    public static void UpdateText(string key, string text)
    {
        foreach (var item in instance.textItems)
        {
            if (item.key == key)
            {
                item.textObject.text = text;
                return;
            }
        }
        Debug.LogError("Key " + key + " not found!");
    }
}

