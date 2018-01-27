using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<ItemController> items = new List<ItemController>();

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
        items.Add(item);
    }

    public void RemoveItem(ItemController item)
    {
        parentNode.GetChild(items.IndexOf(item)).parent = null;
        items.Remove(item);
    }
}
