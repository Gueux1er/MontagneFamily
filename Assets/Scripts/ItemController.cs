using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public enum ItemType { POTION, HEART}
    public ItemType type;
    protected Stuff stuff;
    public float initX;
    public float initY;

    protected SpriteRenderer imageRenderer;

    public SpriteRenderer GetSpriteRender()
    {
        return imageRenderer;
    }

    // Use this for initialization
    void Start () {
        imageRenderer = GetComponent<SpriteRenderer>();
        InitType();
        InitPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void take()
    {
        gameObject.SetActive(false);
    }

    public void ApplyEffect(GameObject gameObject)
    {
        stuff.ApplyEffect(gameObject);
    }

    private void InitType()
    {
        switch (type)
        {
            case ItemType.POTION: stuff = new Potion();
                break;
            case ItemType.HEART: stuff = new Heart();
                break;
        }
    }

    public void InitPosition()
    {
        GetComponent<Transform>().position.Set(initX, initY, 0f);
    }

    public IEnumerator DisableGatherStart()
    {
        
        yield return new WaitForSeconds(2f);
        yield break;
    }
