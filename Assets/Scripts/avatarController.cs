using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpForce = 20;

    private bool jumpAbility = true;
    private Rigidbody rigidbody;
    private avatarLife avatarLife;
    private float maximumJumpY;

    private Inventory inventory;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
        avatarLife = GetComponent<avatarLife>();

    }

    // Update is called once per frame
    void Update()
    {
        movement();
        jump();
    }

    void movement()
    {
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.right * h);
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpAbility)
        {
            rigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            maximumJumpY = rigidbody.position.y;
            jumpAbility = false;
        }
        if(!jumpAbility)
        {
            if(rigidbody.position.y > maximumJumpY)
            {
                maximumJumpY = rigidbody.position.y;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Collision Recoltable
        if (collision.gameObject.tag == "Recoltable")
        {
            ItemController item = collision.gameObject.GetComponent<ItemController>();
            item.ApplyEffect(gameObject);
       
            inventory.GetItem(item);
            print("get an item !");
            Destroy(collision.gameObject);
        } else if(collision.gameObject.tag == "Plateform")
        {
            float highFall = maximumJumpY - rigidbody.velocity.y;

            if (highFall >= 6)
            {
                avatarLife.TakeDamage(5);
            } else if (highFall >= 5)
            {
                avatarLife.TakeDamage(4);
            } else if (highFall >= 4)
            {
                avatarLife.TakeDamage(2);
            } else if (highFall >= 3)
            {
                avatarLife.TakeDamage(1);
            }
            jumpAbility = true;
        }

    }

}
