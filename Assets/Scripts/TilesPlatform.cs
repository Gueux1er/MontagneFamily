using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesPlatform : MonoBehaviour {

    public Sprite[] spritePlatform0;
    public Sprite[] spritePlatform1;
    public Sprite[] spritePlatform2;
    public Sprite[] spritePlatform3;
    public Sprite[] spriteGround0;
    public Sprite[] spriteGround1;
    GameObject[] platforms;
    bool isUp = false;
    bool isDown = false;
    bool isLeft = false;
    bool isRight = false;
    
	void Start ()
    {
        platforms = GameObject.FindGameObjectsWithTag("Plateforme");

        for (int i = 0; i < platforms.Length; ++i)
        {
            if (platforms[i].transform.position.y == transform.position.y + 1)
                isUp = true;
            else if (platforms[i].transform.position.x == transform.position.x - 1)
                isLeft = true;
            else if (platforms[i].transform.position.x == transform.position.x + 1)
                isRight = true;
            else if (platforms[i].transform.position.y == transform.position.y - 1)
                isDown = true;
        }

        if (isUp && isLeft && isRight)
        {

        }
	}
	
	void Update ()
    {
		
	}
}
