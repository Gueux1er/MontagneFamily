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

    private Sprite closedBgInventory;



    // Use this for initialization
    void Start () {
        InventoryBgContainerUI = InventoryUI.GetChild(0);
        InventoryContainerUI = InventoryUI.GetChild(1);
        InventoryClosedContainerUI = InventoryUI.GetChild(2);

        InventoryClosedContainerUI.gameObject.SetActive(false);

        closedBgInventory = Resources.Load<Sprite>("images/closedBgInventory");
        updateDisplay();
        ToggleVisibility();
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

        DestroyAllChildren(InventoryBgContainerUI);
        DestroyAllChildren(InventoryContainerUI);

        //Display empty inventory in case he have nothing
        if(savedItems.Count + collectedItems.Count == 0)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/tileset_item");
            return;
        }

        int nbItems = savedItems.Count;

        for (int i=0; i<nbItems; i++)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = Resources.LoadAll<Sprite>("images/tileset_item")[1];

            tmp = Instantiate(pf_imgInventory, InventoryContainerUI);
            tmp.GetComponent<Image>().sprite = savedItems[i].GetSpriteRender().sprite;
        }

        nbItems = collectedItems.Count;
        for (int i = 0; i < nbItems; i++)
        {
            tmp = Instantiate(pf_bgInventory, InventoryBgContainerUI);
            tmp.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/tileset_item");

            tmp = Instantiate(pf_imgInventory, InventoryContainerUI);
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
        DestroyAllChildren(InventoryContainerUI);
        collectedItems = new List<ItemController>();
        updateDisplay();
    }

    public void EmptyCollected()
    {
        for(int i=0; i<collectedItems.Count; i++)
        {
            collectedItems[i].gameObject.SetActive(true);
            StartCoroutine(collectedItems[i].DisableGatherStart());
        }
        collectedItems.Clear();

        updateDisplay();
    }

    public void AffectEffectSaved()
    {
        for(int i=0; i<savedItems.Count; i++)
        {
            savedItems[i].ApplyEffect();
        }
    }

    public void DestroyAllChildren(Transform t)
    {
        for (int i = 0; i < t.childCount; i++)
        {
            Destroy(t.GetChild(i).gameObject);
        }
    }

}
