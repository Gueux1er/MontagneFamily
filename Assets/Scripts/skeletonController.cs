using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonController : MonoBehaviour
{
    public GameObject skull;
    float countdown;
    bool isDown;

    private void Update()
    {
        if (!isDown)
            countdown += Time.deltaTime;
        if (countdown > 0.1f && !isDown)
        {
            isDown = true;
            skull.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Plateform")
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            skull.GetComponent<Rigidbody2D>().freezeRotation = false;
        }
    }
}
