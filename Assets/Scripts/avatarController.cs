using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarController : MonoBehaviour
{
    public float moveSpeed;
    private float defaultMovespeed = 8;
    public float fallBonus = 0;
    public float jumpForce;
    private float defaultJumpForce = 15;
    private Vector2 originPosition;

    public bool moveEnable = true;

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
        moveSpeed = defaultMovespeed;
        jumpForce = defaultJumpForce;

        rigidbody = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        avatarLife = GetComponent<avatarLife>();

        originPosition = transform.position;

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
        if(moveEnable)
        {
            movement();
            jump();
            inventoryManager();
        }
    }

    public void StopAllAnim()
    {
        GetComponent<Animator>().SetBool("IsWalk", false);
        GetComponent<Animator>().SetBool("IsJump", false);
    }

    void movement()
    {
        float h = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        GetComponent<Animator>().SetBool("IsWalk", (h != 0) ? true : false);
        if (h != 0)
            GetComponent<SpriteRenderer>().flipX = (h < 0) ? true : false;
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
            GetComponent<Animator>().SetBool("IsJump", true);
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
        if (collision.gameObject.tag == "Plateform" && collision.contacts[0].normal == Vector2.up)
        {
            float highFall = maximumJumpY - rigidbody.position.y;
            if (highFall < 1) return;
            GetComponent<Animator>().SetBool("IsJump", false);

            reception.start(); // Joue le son une fois


            if (highFall >= 12 + fallBonus*3)
            {
                avatarLife.TakeDamage(5);
            } else if (highFall >= 11 + fallBonus * 3)
            {
                avatarLife.TakeDamage(4);
            } else if (highFall >= 9 + fallBonus *2)
            {
                avatarLife.TakeDamage(2);
            } else if (highFall >= 7 + fallBonus)
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
        try
        {
            if (collision.gameObject.tag == "SuperplantLast"
                && collision.contacts[0].normal == Vector2.up
                && collision.gameObject.GetComponent<BoxCollider2D>().enabled)
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(ReableSuperplant(collision.gameObject));
            }
        } catch {}

    }

    public IEnumerator ReableSuperplant(GameObject superPlantLast)
    {
        yield return new WaitForSeconds(2f);
        superPlantLast.GetComponent<BoxCollider2D>().enabled = true;
        yield break;
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
            ItemController item = other.gameObject.GetComponent<ItemController>();
            if(item.canBeTaken)
            {

                item.take(gameObject);

                collectible.start(); // Joue le son une fois
            }
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
        transform.position = originPosition;
    }

    public void ResetStats()
    {
        moveSpeed = defaultMovespeed;
        fallBonus = 0;
        maximumJumpY = originPosition.y;
        jumpForce = defaultJumpForce;
        GetComponent<avatarTimeline>().ratioTime = 1;
    }

}
