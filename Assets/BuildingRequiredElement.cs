using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class BuildingRequiredElement : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Text itemCost;

    [SerializeField]
    private Image slotImage;

    [SerializeField]
    private Color greenColor;

    [SerializeField]
    private Color redColor;

    public bool hasRessource = false;
    public void Setup(ItemInInventory ressourceRequired)
    {
        itemImage.sprite = ressourceRequired.itemData.visual;
        itemCost.text = ressourceRequired.count.ToString();

        ItemInInventory[] itemInInventory = Inventory.instance.GetContent().Where(elem => elem.itemData == ressourceRequired.itemData).ToArray();

        int totalRequiredQuantityInInventory = 0;
        for (int i = 0; i < itemInInventory.Length; i++)
        {
            totalRequiredQuantityInInventory += itemInInventory[i].count;
        }

        if(totalRequiredQuantityInInventory >= ressourceRequired.count)
        {
            hasRessource = true;
            slotImage.color = greenColor;
        } else
        {
            slotImage.color = redColor;
        }
    }
}
