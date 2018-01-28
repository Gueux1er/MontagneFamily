using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPlantController : MonoBehaviour
{

    public GameObject[] plants = new GameObject[3];
    public int shiftStart;
    private int currentPlant = 0;

    void Start()
    {
        shiftStart = 0;

        for (int i = 1; i < plants.Length; i++)
        {
            plants[i].SetActive(false);
        }
        plants[0].SetActive(true);

	}
	
    public void GrowUp()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<avatarLife>().cptTry < shiftStart) return;

        if (currentPlant == plants.Length - 1) return;

        plants[currentPlant].SetActive(false);
        currentPlant++;
        plants[currentPlant].SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            GetComponent<Animator>().SetTrigger("Bounce");
    }
}
