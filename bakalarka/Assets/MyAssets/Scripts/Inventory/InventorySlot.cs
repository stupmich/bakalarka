using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Item item;
    public Image icon;
    public Button removeButton;

    public GameObject equipInfoPrefab;
    public GameObject equipInfo;

    public Text textPrefab;
    private Text[] texts;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton ()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem ()
    {
        if(item != null)
        {
            item.Use();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && equipInfoPrefab != null && equipInfo == null && item.GetType().ToString() == "Equipment")
        {
            Equipment equip = (Equipment)item;
            equipInfo = GameObject.Instantiate(equipInfoPrefab, Input.mousePosition, Quaternion.identity, GameObject.FindGameObjectWithTag("CanvasUI").transform);

            texts = equipInfo.GetComponentsInChildren<Text>();

            texts[0].text = equip.name;
            texts[1].text = equip.equipmentSlot.ToString();

            if (equip.damageModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Damage: " + equip.damageModifier.ToString();
            }

            if (equip.armorModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Armor: " + equip.armorModifier.ToString();
            }

            if (equip.vitalityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Vitality: " + equip.vitalityModifier.ToString();
            }

            if (equip.strengthModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Strength: " + equip.strengthModifier.ToString();
            }

            if (equip.dexterityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Dexterity: " + equip.dexterityModifier.ToString();
            }

            if (equip.blockChanceModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Block chance: " + equip.blockChanceModifier.ToString();
            }

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (equipInfo != null)
        {
            Destroy(equipInfo);
        }
    }
}
