using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float jumpForce = 20;
    public float defaultX = 0f;
    public float defaultY = 1.5f;

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
        if (Input.GetButton("Jump") && jumpAbility)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpAbility = false;
            saut.start(); // Joue le son une fois
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
            jumpAbility = true;
        }
        if (rigidbody.velocity.y < 0)
        {
            // Pour empecher de faire un saut dans le vide, si on est tombé en se lancant glisser
            jumpAbility = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plateform")
        {

            reception.start(); // Joue le son une fois

            float highFall = maximumJumpY - rigidbody.position.y;

            if (highFall >= 12)
            {
                avatarLife.TakeDamage(5);
            } else if (highFall >= 11)
            {
                avatarLife.TakeDamage(4);
            } else if (highFall >= 9)
            {
                avatarLife.TakeDamage(2);
            } else if (highFall >= 7)
            {
                avatarLife.TakeDamage(1);
            }

            if(highFall >= 7)
            {
                if (avatarLife.currentLife > 0)
                {
                    receptionTropHaut.start();
                }
                else
                {
                    mortAplati.start();
                }
            }

            jumpAbility = true;
            
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            inventory.SaveItems();
        }

        //Collision Recoltable
        if (other.gameObject.tag == "Recoltable")
        {
            collectible.start(); // Joue le son une fois
            ItemController item = other.gameObject.GetComponent<ItemController>();
            item.ApplyEffect(gameObject);

            inventory.GetItem(item);
            Destroy(other.gameObject);
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

    public void ResetPosition()
    {
        rigidbody.velocity = new Vector2(0,0);
        rigidbody.MovePosition(new Vector2(defaultX, defaultY));
    }

}
