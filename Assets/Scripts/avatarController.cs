using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{

    public float moveSpeed = 2;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * h);
    }
}
