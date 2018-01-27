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
    private Sprite middleBgInventory;
    private Sprite lastBgInventory;
    private Sprite firstAndLastBgInventory;
    private Sprite closedBgInventory;



    // Use this for initialization
    void Start () {
        InventoryContainerUI = InventoryUI.GetChild(1);
        InventoryBgContainerUI = InventoryUI.GetChild(0);
        InventoryClosedContainerUI = InventoryUI.GetChild(2);

        InventoryClosedContainerUI.gameObject.SetActive(false);

        firstBgInventory = Resources.Load<Sprite>("images/firstBgInventory");
        middleBgInventory = Resources.Load<Sprite>("images/middleBgInventory");
        lastBgInventory = Resources.Load<Sprite>("images/lastBgInventory");
        firstAndLastBgInventory = Resources.Load<Sprite>("images/firstAndLastBgInventory");
        closedBgInventory = Resources.Load<Sprite>("images/closedBgInventory");

        // Init the display to set it empty
        GameObject tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
        tmp.GetComponent<Image>().sprite = firstAndLastBgInventory;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GetItem(ItemController item)
    {
        //Add the object to UI
        GameObject tmp = Instantiate(pf_imgInventory, InventoryContainerUI);
        tmp.GetComponent<Image>().sprite = item.image.sprite;
        collectedItems.Add(item);
        updateBackground();
    }

    public void RemoveItem(ItemController item)
    {
        InventoryContainerUI.GetChild(collectedItems.IndexOf(item)).parent = null;
        collectedItems.Remove(item);
        updateBackground();
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

    private void updateBackground()
    {
        int nbItems = collectedItems.Count;
        GameObject tmp;
        InventoryBgContainerUI.DetachChildren();

        if (nbItems == 0 ||nbItems == 1) {
            //Add the background to UI
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = firstAndLastBgInventory;
        } else
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = firstBgInventory;
        }
        if(nbItems > 2)
        {
            for(int i=1; i<nbItems-1; i++)
            {
                tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
                tmp.GetComponent<Image>().sprite = middleBgInventory;
            }
        }
        if(nbItems > 1)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = lastBgInventory;
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
        updateBackground();
    }

}
