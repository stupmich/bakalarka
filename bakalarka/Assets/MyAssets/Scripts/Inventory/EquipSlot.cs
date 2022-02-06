using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Equipment item;
    public Image icon;
    public Button removeButton;
    public EquipmentSlot equipmentSlot;

    public GameObject equipInfoPrefab;
    public GameObject equipInfo;

    public Text textPrefab;
    private Text[] texts;

    public Sprite defaultIcon;

    public void AddItem(Equipment newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        icon.color = new Color32(255, 255, 255, 255);

        removeButton.gameObject.SetActive(true) ;
        removeButton.interactable = true;
    }


    public void OnRemoveButton()
    {
        Inventory.instance.RemoveEquipFromSlot(item);
        EquipmentManager.instance.UnequipByItem(item);
        removeButton.gameObject.SetActive(false);
        icon.sprite = defaultIcon;
        icon.color = new Color32(0, 0, 0, 195);
        item = null;
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && equipInfoPrefab != null && equipInfo == null)
        {
            equipInfo = GameObject.Instantiate(equipInfoPrefab, Input.mousePosition, Quaternion.identity, GameObject.FindGameObjectWithTag("CanvasUI").transform);

            texts = equipInfo.GetComponentsInChildren<Text>();

            texts[0].text = item.name;
            texts[1].text = item.equipmentSlot.ToString();

            if (item.damageModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Damage: " + item.damageModifier.ToString();
            }

            if (item.armorModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Armor: " + item.armorModifier.ToString();
            }

            if (item.vitalityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Vitality: " + item.vitalityModifier.ToString();
            }

            if (item.strengthModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Strength: " + item.strengthModifier.ToString();
            }

            if (item.dexterityModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Dexterity: " + item.dexterityModifier.ToString();
            }

            if (item.blockChanceModifier > 0)
            {
                Text tempTextBox = Instantiate(textPrefab, new Vector3(0, 0, 0), transform.rotation) as Text;
                //Parent to the panel
                tempTextBox.transform.SetParent(equipInfo.transform.GetChild(2), false);
                //Set the text box's text element font size and style:
                tempTextBox.fontSize = 10;
                //Set the text box's text element to the current textToDisplay:
                tempTextBox.text = "Block chance: " + item.blockChanceModifier.ToString();
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
