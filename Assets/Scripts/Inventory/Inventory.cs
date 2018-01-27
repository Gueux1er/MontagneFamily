using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<ItemController> collectedItems = new List<ItemController>();
    public List<ItemController> savedItems = new List<ItemController>();

    public GameObject pf_imgInventory;
    public GameObject pf_bgInventory;
    public Transform InventoryUI;
    private Transform InventoryContainerUI;
    private Transform InventoryBgContainerUI;
    private Transform InventoryClosedContainerUI;

    private Sprite firstBgInventory;
    private Sprite closedBgInventory;



    // Use this for initialization
    void Start () {
        InventoryBgContainerUI = InventoryUI.GetChild(0);
        InventoryContainerUI = InventoryUI.GetChild(1);
        InventoryClosedContainerUI = InventoryUI.GetChild(2);

        InventoryClosedContainerUI.gameObject.SetActive(false);

        firstBgInventory = Resources.Load<Sprite>("images/firstBgInventory");
        closedBgInventory = Resources.Load<Sprite>("images/closedBgInventory");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GetItem(ItemController item)
    {
        //Add the object to UI
        collectedItems.Add(item);
        updateDisplay();
    }

    public void RemoveItem(ItemController item)
    {
        InventoryContainerUI.GetChild(collectedItems.IndexOf(item)).parent = null;
        collectedItems.Remove(item);
        updateDisplay();
    }

    public void ToggleVisibility()
    {
        if(InventoryContainerUI.gameObject.activeSelf)
        {
            InventoryContainerUI.gameObject.SetActive(false);
            InventoryBgContainerUI.gameObject.SetActive(false);
            InventoryClosedContainerUI.gameObject.SetActive(true);
        }
        else
        {
            InventoryContainerUI.gameObject.SetActive(true);
            InventoryBgContainerUI.gameObject.SetActive(true);
            InventoryClosedContainerUI.gameObject.SetActive(false);
        }
    }

    private void updateDisplay()
    {
        GameObject tmp;

        InventoryBgContainerUI.DetachChildren();
        InventoryContainerUI.DetachChildren();

        int nbItems = savedItems.Count;
        for (int i=0; i<nbItems; i++)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/savedBgInventory");

            tmp = Instantiate(pf_imgInventory, InventoryContainerUI);
            tmp.GetComponent<Image>().sprite = savedItems[i].GetSpriteRender().sprite;
        }

        nbItems = collectedItems.Count;
        for (int i = 0; i < nbItems; i++)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/collectedBgInventory");

            tmp = Instantiate(pf_imgInventory, InventoryContainerUI);
            print(collectedItems[i]);
            tmp.GetComponent<Image>().sprite = collectedItems[i].GetSpriteRender().sprite;
        }
    }

    public void SaveItems()
    {
        print("save items");
        foreach(ItemController item in collectedItems)
        {
            savedItems.Add(item);
        }
        InventoryContainerUI.DetachChildren();
        collectedItems = new List<ItemController>();
        updateDisplay();
    }

    public void EmptyCollected()
    {
        for(int i=0; i<collectedItems.Count; i++)
        {
            print(collectedItems[i]);
            collectedItems[i].gameObject.SetActive(true);
            RemoveItem(collectedItems[i]);
        }
        updateDisplay();
    }

}
