using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPlantController : MonoBehaviour {

    public GameObject[] plants = new GameObject[3];
    private int currentPlant = 0;

    // Use this for initialization
    void Start () {
		for(int i = 0; i < plants.Length; i++)
        {
            plants[i].SetActive(false);
        }
        plants[0].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void growUp()
    {
        print("grow");
        plants[currentPlant].SetActive(false);
        currentPlant++;
        plants[currentPlant].SetActive(true);
    }
}
