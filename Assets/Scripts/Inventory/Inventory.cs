using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private List<ItemController> items = new List<ItemController>();


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetItem(ItemController item)
    {
        items.Add(item);
    }

    public void RemoveItem(ItemController item)
    {
        items.Remove(item);
    }
}
