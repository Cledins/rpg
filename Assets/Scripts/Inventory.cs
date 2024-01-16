using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [Header("OTHER SCRIPTS REFERENCES")]
    [SerializeField]
    private CraftingSystem craftingSystem;

    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private ItemActionsSystem itemActionsSystem;

    [SerializeField]
    private BuildSystem buildSystem;

    [Header("INVENTORY SYSTEM VARIABLES")]

    [SerializeField]
    private List<ItemInInventory> content = new List<ItemInInventory>();


    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;


    const int InventorySize = 24;

    public Sprite emptySlotVisual;

    public static Inventory instance;
    private bool isOpen = false;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        CloseInventory();
        RefreshContent();
    }

    public void AddItem(ItemData item)
    {

        ItemInInventory[] itemInInventory = content.Where(elem => elem.itemData == item).ToArray();

        bool itemAdded = false;  

        if(itemInInventory.Length > 0 && item.stackable)
        {
            for (int i = 0; i < itemInInventory.Length; i++)
            {
                if(itemInInventory[i].count < item.maxStack)
                {
                    itemAdded = true;
                    itemInInventory[i].count++;
                    break;
                }
            }

            if(!itemAdded)
            {
                content.Add(
                new ItemInInventory
                {
                    itemData = item,
                    count = 1
                });
            }
        } else
        {
            content.Add(
                new ItemInInventory
                {
                    itemData = item,
                    count = 1
            });
        }

        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        ItemInInventory itemInInventory = content.Where(elem => elem.itemData == item).FirstOrDefault();

        if(itemInInventory != null && itemInInventory.count > 1)
        {
            itemInInventory.count -= 1;
        }
        else
        {
            content.Remove(itemInInventory);
        }
        RefreshContent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        itemActionsSystem.actionPanel.SetActive(false);
        TooltipSystem.instance.Hide();
        isOpen = false;
    }

    public void RefreshContent()
    {
        //Empty all visuals/slots
        for (int i = 0; i < inventorySlotsParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
            currentSlot.countText.enabled = false;
        }

        //Populationg slots visual with actual inventory content
        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i].itemData;
            currentSlot.itemVisual.sprite = content[i].itemData.visual;

            if(currentSlot.item.stackable)
            {
                currentSlot.countText.enabled = true;
                currentSlot.countText.text = content[i].count.ToString();
            }
        }

        equipment.UpdateEquipmentDesequipButtons();
        craftingSystem.UpdateDisplayedRecipes();
        buildSystem.UpdateDisplayedCosts();
    }

    public List<ItemInInventory> GetContent()
    {
        return content;
    }

    public void LoadData(List<ItemInInventory> savedData)
    {
        content = savedData;
        RefreshContent();
    }

    public void ClearContent()
    {
        content.Clear();
    }

    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
  
}

[System.Serializable]
public class ItemInInventory
{
    public ItemData itemData;
    public int count;
}