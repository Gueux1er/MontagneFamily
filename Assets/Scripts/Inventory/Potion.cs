using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Stuff
{

    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<avatarTimeline>().ratioTime = 0.5f;
    }
}
