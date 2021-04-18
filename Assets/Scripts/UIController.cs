using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are multiple instances of the UIController! Make sure there's always exactly one!");
        }
    }

    public GameObject inGameParent;
    public GameObject gameOverParent;

    [Header("Inventory")]
    public Transform inventoryParent;
    public GameObject inventorySlotPreafb;
    private GameObject[] slotUis;

    void Start()
    {
        inGameParent.SetActive(true);
        gameOverParent.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenGameoverScreen()
    {
        inGameParent.SetActive(false);
        gameOverParent.SetActive(true);
    }

    static public void InitInventoryUI(int slotCount)
    {
        instance.slotUis = new GameObject[slotCount];
        // Remove all slots
        for (int i = 0; i < instance.inventoryParent.childCount; i++)
        {
            Destroy(instance.inventoryParent.GetChild(i).gameObject);
        }
        // Create slots
        for (var i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(instance.inventorySlotPreafb, instance.inventoryParent);
            slotObj.GetComponent<InventorySlotUI>().SetDisplay(i + 1, null, i == 0);
            instance.slotUis[i] = slotObj;
        }
    }

    public static void UpdateInventoryUI(InventoryItem[] slots, int selectedSlot)
    {
        // Create slots
        for (int i = 0; i < slots.Length; i++)
        {
            instance.slotUis[i].GetComponent<InventorySlotUI>().SetDisplay(i + 1, slots[i].IconSprite, i == selectedSlot);
        }
    }
}
