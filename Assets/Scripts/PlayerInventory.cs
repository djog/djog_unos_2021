using UnityEngine;
using System.Collections.Generic;

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

    const int SLOT_COUNT = 4;
    public InventoryItem[] slots;
    public int selectedSlot = 0;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Pickup"))
        {
            PickupWeapon(collider.gameObject);
        }
    }

    public void PickupWeapon(GameObject weaponObject)
    {
        int freeSlots = 0;
        int? firstFreeSlot = null;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                freeSlots++;
                if (firstFreeSlot == null)
                {
                    firstFreeSlot = i;
                }
            }
        }
        if (freeSlots >= 1)
        {
            Weapon weapon = weaponObject.GetComponent<Weapon>();
            weapon.transform.SetParent(weaponParent);
            weapon.transform.localPosition = Vector3.zero;
            slots[firstFreeSlot.Value] = new InventoryItem() { weapon = weapon };
            UpdateInventory();
        }
    }

    public InventoryItem SelectedItem()
    {
        var selected = slots[selectedSlot];
        return selected;
    }
}
