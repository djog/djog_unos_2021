using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class InventoryItem
{
    public bool IsEmpty
    {
        get
        {
            return weapon == null;
        }
    }

    public bool IsWeapon
    {
        get
        {
            return weapon != null;
        }
    }
    public Sprite IconSprite
    {
        get
        {
            if (weapon)
            {
                return weapon.iconSprite;
            }
            else
            {
                return null;
            }
        }
    }


    public Weapon weapon;
}

public class PlayerInventory : MonoBehaviour
{
    public Transform weaponParent;

    public float pickupRadius = 1.0f;

    const int SLOT_COUNT = 4;
    private InventoryItem[] slots;
    private int selectedSlot = 0;

    void Start()
    {
        slots = new InventoryItem[SLOT_COUNT];
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            slots[i] = new InventoryItem();
        }
        UIController.InitInventoryUI(SLOT_COUNT);
    }

    public void Update()
    {
        // Inventory Input
        if (Input.mouseScrollDelta.y < 0.0)
        {
            selectedSlot++;
            selectedSlot %= SLOT_COUNT;
            UpdateInventory();
        }
        else if (Input.mouseScrollDelta.y > 0.0)
        {
            selectedSlot--;
            if (selectedSlot < 0)
            {
                selectedSlot = SLOT_COUNT - 1;
            }
            UpdateInventory();
        }

        // Pickup Update
        bool pickupInRadius = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        List<GameObject> pickups = new List<GameObject>();
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Pickup"))
            {
                pickups.Add(collider.gameObject);
                pickupInRadius = true;
            }
        }
        if (pickupInRadius)
        {
            UIController.SetPickupHint(slots[selectedSlot].IsEmpty ? "[F] Pickup" : "[F] Replace");
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameObject closest = pickups.OrderBy(p => transform.position - p.transform.position).ToList()[0];
                PickupOrReplaceWeapon(selectedSlot, closest.gameObject);
            }
        }
        else
        {
            UIController.SetPickupHint(null);
        }
        if (!slots[selectedSlot].IsEmpty && Input.GetKeyDown(KeyCode.G))
        {
            DropItem(selectedSlot);
        }
    }


    void UpdateInventory()
    {
        // Hide/show the selected object
        for (int i = 0; i < SLOT_COUNT; i++)
        {
            if (slots[i].IsEmpty)
            {
                continue;
            }
            if (selectedSlot == i)
            {
                slots[i].weapon.gameObject.SetActive(true);
            }
            else
            {
                slots[i].weapon.gameObject.SetActive(false);
            }
        }
        // Update UI
        UIController.UpdateInventoryUI(slots, selectedSlot);
    }

    public void DropItem(int slot)
    {
        GameObject obj = slots[slot].weapon.gameObject;
        obj.transform.SetParent(null);
        obj.tag = "Pickup";
        slots[slot].weapon = null;
        UpdateInventory();
    }

    public void PickupOrReplaceWeapon(int slot, GameObject weaponObject)
    {
        if (!slots[selectedSlot].IsEmpty)
        {
            DropItem(slot);
        }

        Weapon weapon = weaponObject.GetComponent<Weapon>();
        weapon.transform.SetParent(weaponParent);
        weapon.transform.localPosition = Vector3.zero;
        weapon.gameObject.tag = "InventoryItem";
        slots[selectedSlot] = new InventoryItem() { weapon = weapon };
        UpdateInventory();
    }

    public InventoryItem SelectedItem()
    {
        var selected = slots[selectedSlot];
        return selected;
    }
}
