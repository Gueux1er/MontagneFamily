using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public enum ItemType { POTION, HEART }
    public ItemType type;
    protected Stuff stuff;
    public bool canBeTaken;
    public bool isEvolutive;
    public float initX;
    public float initY;

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
        other.GetComponent<Inventory>().GetItem(this);
        ApplyEffect(other);
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
            case ItemType.POTION:
                isEvolutive = false;
                stuff = new Potion();
                break;
            case ItemType.HEART:
                isEvolutive = true;
                stuff = new Heart();
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
