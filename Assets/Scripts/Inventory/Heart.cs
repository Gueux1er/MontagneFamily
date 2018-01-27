using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Stuff
{
    public static string spriteName = "images/hearts";

    public Heart()
    {
        sprite = Resources.Load<Sprite>(spriteName);
    }

    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
