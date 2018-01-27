using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stuff {

    protected Sprite sprite;

    // Apply the stuff effect to the given gameObject
    public abstract void ApplyEffect(GameObject gameObject);

    public Sprite GetSprite()
    {
        return sprite;
    }
}
