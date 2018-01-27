using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Stuff
{
    public static string spriteName = "images/hearts";

    public Star()
    {
        sprite = Resources.Load<Sprite>(spriteName);
    }

    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
