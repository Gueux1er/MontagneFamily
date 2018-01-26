using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpForce = 20;
    private bool jumpAbility = true;
    private Rigidbody rigidbody;

    private Inventory inventory;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        inventory = GetComponent<Inventory>();
    }

    // Use this for initialization
    void Start()
    {

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
            jumpAbility = false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //Collision Sol
        if (collision.contacts[0].normal == Vector3.up && rigidbody.velocity.y == 0)
        {
            jumpAbility = true;
        }
      
    }

    void OnCollisionEnter(Collision collision)
    {
        print("on collision enter");
        print(collision.gameObject.name);
        //Collision Recoltable
        if (collision.gameObject.tag == "Recoltable")
        {
            ItemController item = collision.gameObject.GetComponent<ItemController>();
            item.ApplyEffect(gameObject);
       
            inventory.GetItem(item);
            print("get an item !");
            Destroy(collision.gameObject);
        }

    }

}
