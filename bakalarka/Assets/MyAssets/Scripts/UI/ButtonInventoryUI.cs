using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ButtonInventoryUI : MonoBehaviour
{
    public static event Action<bool> InventoryButtonClicked = delegate { };
    private bool on;
    public GameObject inventoryUI;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
        on = true;
    }

    public void ButtonClicked()
    {
        on ^= true;
        Debug.Log("agags");
        inventoryUI.SetActive(!inventoryUI.activeSelf);
        InventoryButtonClicked(on);
    }
}
