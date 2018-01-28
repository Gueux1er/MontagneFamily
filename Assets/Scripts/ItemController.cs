using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public enum ItemType { POTION, HEART, SHOES, SPRING }
    public ItemType type;
    protected Stuff stuff;
    public bool canBeTaken;
    public bool isEvolutive;
    public float initX;
    public float initY;
    protected GameObject gameObjectTaken;

    protected SpriteRenderer imageRenderer;

    public SpriteRenderer GetSpriteRender()
    {
        return imageRenderer;
    }

    // Use this for initialization
    void Start()
    {
        imageRenderer = GetComponent<SpriteRenderer>();
        InitType();
        InitPosition();

        canBeTaken = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void take(GameObject other)
    {
        canBeTaken = false;
        gameObjectTaken = other;
        gameObjectTaken.GetComponent<Inventory>().GetItem(this);
        ApplyEffect();
        gameObject.SetActive(false);
    }

    public void ApplyEffect()
    {
        stuff.ApplyEffect(gameObjectTaken);
    }

    private void InitType()
    {
        switch (type)
        {
            case ItemType.POTION:
                isEvolutive = false;
                stuff = new Potion();
                break;
            case ItemType.HEART:
                isEvolutive = true;
                stuff = new Heart();
                break;
            case ItemType.SHOES:
                isEvolutive = false;
                stuff = new Shoes();
                break;
            case ItemType.SPRING:
                isEvolutive = false;
                stuff = new Spring();
                break;
        }
    }

    public virtual void NextGeneration()
    {
        // for heritage
    }

    public void InitPosition()
    {
        GetComponent<Transform>().position.Set(initX, initY, 0f);
    }

    public IEnumerator DisableGatherStart()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        yield break;
    }
}
