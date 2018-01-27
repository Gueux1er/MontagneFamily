using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpForce = 20;

    private bool jumpAbility = true;
    private Rigidbody2D rigidbody;
    private avatarLife avatarLife;
    private float maximumJumpY;

    private Inventory inventory;

    //*** Sons ***//

    FMOD.Studio.EventInstance collectible; //Instanciation du son
    FMOD.Studio.ParameterInstance agePourCollectible; //Instanciation du paramètre lié au son

    FMOD.Studio.EventInstance saut;
    FMOD.Studio.ParameterInstance agePourSaut;

    FMOD.Studio.EventInstance reception;
    FMOD.Studio.ParameterInstance agePourReception;

    FMOD.Studio.EventInstance receptionTropHaut;
    FMOD.Studio.ParameterInstance agePourReceptionTropHaut;

    FMOD.Studio.EventInstance mortAplati;
    FMOD.Studio.ParameterInstance agePourMortAplati;


    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        avatarLife = GetComponent<avatarLife>();

        //*** Sons ***//

        collectible = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Collectible"); // Chemin du son 
        collectible.getParameter("Age", out agePourCollectible); // Va chercher le paramètre FMOD "Age" et le stocke dans le paramètre "agePourCollectible".
        agePourCollectible.setValue(0.0f); // Valeur du paramètre en début de partie

        saut = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Saut");
        saut.getParameter("Age", out agePourSaut); 
        agePourSaut.setValue(0.0f);

        reception = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Reception");
        reception.getParameter("Age", out agePourReception);
        agePourReception.setValue(0.0f);

        receptionTropHaut = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Reception_Trop_Haut");
        receptionTropHaut.getParameter("Age", out agePourReceptionTropHaut);
        agePourReceptionTropHaut.setValue(0.0f);

        mortAplati = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Mort_Aplati");
        mortAplati.getParameter("Age", out agePourMortAplati);
        agePourMortAplati.setValue(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        jump();
        inventoryManager();
    }

    void movement()
    {
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector2.right * h);
    }

    void inventoryManager()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            GetComponent<Inventory>().ToggleVisibility();
        }
    }

    void jump()
    {
        if (Input.GetButtonDown("Jump") && jumpAbility)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpAbility = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Saut"); // Joue le son une fois
        }
        if(!jumpAbility)
        {
            if(rigidbody.position.y > maximumJumpY)
            {
                maximumJumpY = rigidbody.position.y;
            }
        }
        if(rigidbody.velocity.y == 0)
        {
            // QUand on est au sol, on reset la position du maxjump
            maximumJumpY = rigidbody.position.y; ;
        }
        if(rigidbody.velocity.y < 0)
        {
            // Pour empecher de faire un saut dans le vide, si on est tombé en se lancant glisser
            //jumpAbility = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("test");
        //Collision Recoltable
        if (collision.gameObject.tag == "Recoltable")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Collectible"); // Joue le son une fois
            ItemController item = collision.gameObject.GetComponent<ItemController>();
            item.ApplyEffect(gameObject);

            inventory.GetItem(item);
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plateform")
        {

            float highFall = maximumJumpY - rigidbody.position.y;

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

            if (!jumpAbility)
            {
                jumpAbility = true;

                if (highFall < 3)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Reception"); // Joue le son une fois

                }
                else
                {
                    if (avatarLife.currentLife > 0)
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Reception_Trop_Haut");
                    } else
                    {
                        FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Mort_Aplati");
                    }
                }
                
            }
            
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            inventory.SaveItems();
        }
    }

    public void setAllAgePourAudio(float value)
    {
        agePourCollectible.setValue(value);
        agePourSaut.setValue(value);
        agePourReception.setValue(value);
        agePourReceptionTropHaut.setValue(value);
        agePourMortAplati.setValue(value);
    }

}
