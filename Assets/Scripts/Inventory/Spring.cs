using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Stuff
{

    public override void ApplyEffect(GameObject gameObject)
    {
        gameObject.GetComponent<avatarController>().fallBonus += 1.5f;
        gameObject.GetComponent<avatarController>().jumpForce += 2.5f;
    }
}
