using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public enum ItemType { STAR, HEART}
    public ItemType type;
    private Stuff stuff;
    public float initX;
    public float initY;

    private SpriteRenderer imageRenderer;

    public SpriteRenderer GetSpriteRender()
    {
        return imageRenderer;
    }

    // Use this for initialization
    void Start () {
        InitType();
        imageRenderer = GetComponent<SpriteRenderer>();
        imageRenderer.sprite = stuff.GetSprite();
        InitPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ApplyEffect(GameObject gameObject)
    {
        stuff.ApplyEffect(gameObject);
    }

    private void InitType()
    {
        switch (type)
        {
            case ItemType.STAR: stuff = new Star();
                break;
            case ItemType.HEART: stuff = new Heart();
                break;
        }
    }

    public void InitPosition()
    {
        GetComponent<Transform>().position.Set(initX, initY, 0f);
    }
}
