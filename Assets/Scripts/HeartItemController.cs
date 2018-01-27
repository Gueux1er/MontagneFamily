using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItemController : ItemController {

    private SpriteRenderer plantSprite;
    public GameObject heart;
    public GameObject plant;
    public Sprite emptyPlantSprite;
    public Sprite fullPlantSprite;

    public int appearEveryXGeneration = 3;
    public int delaySpawn = 0;

	// Use this for initialization
	void Start () {
        
        imageRenderer = heart.GetComponent<SpriteRenderer>();
        plantSprite = plant.GetComponent<SpriteRenderer>();
        stuff = new Heart();
        InitPosition();
        isEvolutive = true;
        canBeTaken = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void take(GameObject gameObject)
    {
        gameObjectTaken = gameObject;
        ApplyEffect();
        heart.SetActive(false);
        plant.GetComponent<SpriteRenderer>().sprite = emptyPlantSprite;
        canBeTaken = false;
    }

    public override void NextGeneration()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<avatarLife>().cptTry % appearEveryXGeneration == delaySpawn)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        heart.SetActive(true);
        canBeTaken = true;
        plant.GetComponent<SpriteRenderer>().sprite = fullPlantSprite;
    }

}
