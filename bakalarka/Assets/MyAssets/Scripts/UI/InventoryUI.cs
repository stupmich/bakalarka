using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region Singleton
    public static InventoryUI instace;

    void Awake()
    {
        instace = this;

        xpManager.OnXPChanged += OnXPChanged;
        playerStats.OnStatChanged += UpdateStats;
        inventory.onItemChangedCallBack += UpdateUI;
    }
    #endregion

    [SerializeField]
    ExperienceManager xpManager;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    PlayerStats playerStats;

    public Transform itemsParent;
    public GameObject inventoryUI;
    InventorySlot[] slots;
    EquipSlot[] equipSlots;

    public Text levelText;
    public Text strengthText;
    public Text dexterityText;
    public Text vitalityText;
    public Text armorText;
    public Text damageText;
    public Text goldText;

    public GameObject itemInfo;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        equipSlots = itemsParent.GetComponentsInChildren<EquipSlot>();

        levelText.text = xpManager.level.ToString();
        strengthText.text = playerStats.strength.GetValue().ToString();
        dexterityText.text = playerStats.dexterity.GetValue().ToString();
        vitalityText.text = playerStats.vitality.GetValue().ToString();
        armorText.text = playerStats.armor.GetValue().ToString();
        damageText.text = playerStats.damage.GetValue().ToString();
        goldText.text = playerStats.gold.GetValue().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (this.inventoryUI.activeSelf == true && this.itemInfo != null)
            {
                Object.Destroy(this.itemInfo);
            }
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void OnXPChanged(int maxXP, int currentXP, int level, int statPoints)
    {
        if (levelText != null)
        {
            levelText.text =  level.ToString();
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }

        for (int i = 0; i < equipSlots.Length; i++)
        {
            for (int j = 0; j < inventory.equipedItems.Count ; j++)
            {
                if (equipSlots[i].equipmentSlot == inventory.equipedItems[j].equipmentSlot)
                {
                    equipSlots[i].AddItem(inventory.equipedItems[j]);
                }
            }
        }

        UpdateStats();
    }

    void UpdateStats()
    {
        strengthText.text = playerStats.strength.GetValue().ToString();
        dexterityText.text = playerStats.dexterity.GetValue().ToString();
        vitalityText.text = playerStats.vitality.GetValue().ToString();
        armorText.text = playerStats.armor.GetValue().ToString();
        damageText.text = playerStats.damage.GetValue().ToString();
        goldText.text = playerStats.gold.GetValue().ToString();
    }

}
