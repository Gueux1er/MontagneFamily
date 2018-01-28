using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesPlatform : MonoBehaviour {

    public int heightToPlatformType1;
    public int heightToPlatformType2;

    public Sprite[] spritePlatform0;
    public Sprite[] spritePlatform1;
    public Sprite[] spritePlatform2;
    public GameObject prefabGround;
    public Sprite[] spriteGround0;
    public Sprite[] spriteGround1;
    Sprite[] spritePlatformToApply;
    GameObject[] platforms;
    public bool isUp = false;
    public bool isDown = false;
    public bool isLeft = false;
    public bool isRight = false;
    
	void Start ()
    {
        platforms = GameObject.FindGameObjectsWithTag("Plateform");

        for (int i = 0; i < platforms.Length; ++i)
        {
            if (Vector3.Distance(transform.position, platforms[i].transform.position) <= 2.5f && gameObject != platforms[i])
            {
                if (platforms[i].transform.position.y > transform.position.y)
                    isUp = true;
                else if (platforms[i].transform.position.x > transform.position.x)
                    isRight = true;
                else if (platforms[i].transform.position.x < transform.position.x)
                    isLeft = true;
                else if (platforms[i].transform.position.y < transform.position.y)
                    isDown = true;
            }
        }

        
        if (transform.position.y >= heightToPlatformType2)
            spritePlatformToApply = spritePlatform2;
        else if (transform.position.y >= heightToPlatformType1)
            spritePlatformToApply = spritePlatform1;
        else
            spritePlatformToApply = spritePlatform0;
        

        if ((!isLeft && !isRight && !isDown && !isUp) || (isLeft && isRight && isDown && !isUp) || (isLeft && isRight && !isUp && !isDown) || (isDown && !isUp && !isLeft && !isRight))
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[0];
        }
        else if ((isUp && isLeft && isRight && isDown) || (isUp && isDown && !isRight && !isLeft))
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[Random.Range(8, 10)];
        }
        else if (isUp && isLeft && isRight && !isDown)
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[1];
        }
        else if (isUp && isRight && isDown)
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[2];
        }
        else if (isUp && isLeft && isDown)
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[3];
        }
        else if ((isLeft && isDown) || (isLeft && !isUp && !isDown && !isRight))
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[4];
        }
        else if ((isRight && isDown) || (isRight && !isUp && !isDown))
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[5];
        }
        else if (isRight && isUp)
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[6];
        }
        else if (isLeft && isUp)
        {
            GetComponent<SpriteRenderer>().sprite = spritePlatformToApply[7];
        }
    }
}
