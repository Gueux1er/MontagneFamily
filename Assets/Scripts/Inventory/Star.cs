using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Stuff
{


    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
