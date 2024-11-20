using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public List<ItemData> content = new List<ItemData>();

    [SerializeField]
    public GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotsParent;

    const int maxSize = 3;

    public void Start()
    {
        RefreshContent();
    }

    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }

    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }

    private void RefreshContent()
    {
        for (int i = 0; i < content.Count; i++)
        {
            inventorySlotsParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = content[i].visuel;
        }
    }

    public bool HaveSpace()
    {
        return maxSize != content.Count;
    }
}
