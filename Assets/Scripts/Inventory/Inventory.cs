using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<ItemController> collectedItems = new List<ItemController>();
    public List<ItemController> savedItems = new List<ItemController>();

    public GameObject pf_imgInventory;
    public Transform parentNode;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetItem(ItemController item)
    {
        GameObject tmp = Instantiate(pf_imgInventory, parentNode);
        tmp.GetComponent<Image>().sprite = item.image.sprite;
        collectedItems.Add(item);
    }

    public void RemoveItem(ItemController item)
    {
        parentNode.GetChild(collectedItems.IndexOf(item)).parent = null;
        collectedItems.Remove(item);
    }

    public void SaveItems()
    {
        print("save items");
        foreach(ItemController item in collectedItems)
        {
            savedItems.Add(item);
        }
        parentNode.DetachChildren();
        collectedItems = new List<ItemController>();
    }

}
