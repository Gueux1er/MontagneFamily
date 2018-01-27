using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : Stuff
{

    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<avatarController>().moveSpeed += 3;
    }
}
