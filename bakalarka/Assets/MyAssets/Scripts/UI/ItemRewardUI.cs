using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemRewardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private GameObject equipInfoPrefab;
    [SerializeField]
    private GameObject itemInfo;
    [SerializeField]
    private Text textPrefab;
    private Text[] texts;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && equipInfoPrefab != null && itemInfo == null && item.GetType().ToString() == "Equipment")
        {
            itemInfo = GameObject.Instantiate(equipInfoPrefab, Input.mousePosition, Quaternion.identity, GameObject.FindGameObjectWithTag("CanvasUI").transform);
            texts = itemInfo.GetComponentsInChildren<Text>();

            InventoryUI.instace.itemInfo = this.itemInfo;

            Equipment equip = (Equipment)item;

            texts[0].text = equip.name;
            texts[1].text = equip.equipmentSlot.ToString();

            if (equip.damageModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Damage: " + equip.damageModifier.ToString();
            }

            if (equip.armorModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Armor: " + equip.armorModifier.ToString();
            }

            if (equip.vitalityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Vitality: " + equip.vitalityModifier.ToString();
            }

            if (equip.strengthModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Strength: " + equip.strengthModifier.ToString();
            }

            if (equip.dexterityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Dexterity: " + equip.dexterityModifier.ToString();
            }

            if (equip.blockChanceModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(itemInfo.transform.GetChild(2), false);
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Block chance: " + equip.blockChanceModifier.ToString();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (itemInfo != null)
        {
            Destroy(itemInfo);
        }
    }

    public void SetItem(Item pItem)
    {
        this.item = pItem;
    }

    public GameObject GetItemInfo()
    {
        return this.itemInfo;
    }
}
