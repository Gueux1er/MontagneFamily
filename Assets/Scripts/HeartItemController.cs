using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItemController : ItemController {

    private SpriteRenderer plantSprite;
    public GameObject heart;
    public GameObject plant;
    public Sprite emptyPlantSprite;

	// Use this for initialization
	void Start () {
        
        imageRenderer = heart.GetComponent<SpriteRenderer>();
        plantSprite = plant.GetComponent<SpriteRenderer>();
        stuff = new Heart();
        InitPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void take()
    {
        heart.SetActive(false);
        plant.GetComponent<SpriteRenderer>().sprite = emptyPlantSprite;
    }
}
