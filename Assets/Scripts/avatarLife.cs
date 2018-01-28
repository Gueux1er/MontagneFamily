using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarLife : MonoBehaviour
{
    public int cptTry = 1;
    public int startingLife = 5;
    public int maxLife = 5;
    public int currentLife;
    public Image lifeImageEvnt;
    public float flashSpeed = 5f;
    public Color damagedColour = new Color(1f, 0f, 0f, 0.3f);
    public Color healedColour = new Color(0f, 1f, 0f, 0.3f);

    public Sprite heartFull;
    public Sprite heartEmpty;

    public GameObject pf_skeleton;
    public int nbSkeleton = 3;
    private Queue<GameObject> skeletons = new Queue<GameObject>();

    public Image[]  hearts;

    public GameObject pf_fmodEmitter;
    private GameObject fmodEmitter;

    avatarController avatarController;
    bool isDead;
    bool damaged;
    bool healed;
    bool displayNoFilter;


    FMOD.Studio.EventInstance ambianceJeu; //Instanciation du son

    // Use this for initialization
    void Start()
    {
        isDead = false;
        displayNoFilter = true;
        avatarController = GetComponent<avatarController>();
        currentLife = startingLife;

        fmodEmitter = Instantiate(pf_fmodEmitter);
        ambianceJeu = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/Ambiance"); // Chemin du son 
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            lifeImageEvnt.color = damagedColour;
        } else if(healed)
        {
            lifeImageEvnt.color = healedColour;
        }
        else if(displayNoFilter)
        {
            lifeImageEvnt.color = Color.Lerp(lifeImageEvnt.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        healed = false;
    }

    public void TakeDamage(int value)
    {
        damaged = true;
        currentLife -= value;

        if(currentLife <= 0)
        {
            currentLife = 0;
            Death();
        }

        UpdateHearts();
    }

    public void TakeHeal(int value)
    {
        healed = true;
        currentLife += value;

        if(currentLife > maxLife)
        {
            currentLife = maxLife;
        }

        UpdateHearts();
    }

    private void UpdateHearts()
    {

        for(int i=0; i<maxLife-currentLife; i++)
        {
            hearts[i].sprite = heartEmpty;
        }
        for(int i=maxLife-currentLife; i<maxLife; i++)
        {
            hearts[i].sprite = heartFull;
        }
    }

    public void Death()
    {
        if (isDead) return;
        isDead = true;

        StartCoroutine(BlinkWhite(true));
        GetComponent<avatarTimeline>().ratioTime = 0;

        GetComponent<avatarController>().moveEnable = false;
        GetComponent<avatarController>().StopAllAnim();

        // Evolution for all game objects
        GameObject[] tabGo = GameObject.FindGameObjectsWithTag("Recoltable");
        for(int i=0; i<tabGo.Length; i++)
        {
            if(tabGo[i].GetComponent<ItemController>().isEvolutive)
            {
                tabGo[i].GetComponent<ItemController>().NextGeneration();
            }
        }

        GameObject[] tabSuperplant = GameObject.FindGameObjectsWithTag("Superplant");
        for(int i=0; i<tabSuperplant.Length; i++)
        {
            tabSuperplant[i].GetComponent<SuperPlantController>().GrowUp();
        }

        Destroy(fmodEmitter);
    }

    private void instantiateSkeleton(Vector2 position)
    {
        GameObject skeleton = Instantiate(pf_skeleton);
        skeletons.Enqueue(skeleton);
        if (skeletons.Count > nbSkeleton)
        {
            Destroy(skeletons.Dequeue());
        }
        skeleton.transform.position = new Vector2 (position.x, position.y + 0.5f);
    }

    private void DeathReset()
    {
        fmodEmitter = Instantiate(pf_fmodEmitter);
        Vector2 position = gameObject.transform.position;
        // Reset position /life/etc
        GetComponent<avatarTimeline>().ResetTimeline();
        GetComponent<avatarController>().ResetPosition();
        GetComponent<avatarController>().ResetStats();
        GetComponent<avatarController>().setAllAgePourAudio(0.0f);
        currentLife = startingLife;
        UpdateHearts();
        GetComponent<Animator>().SetLayerWeight(1, 0);
        GetComponent<Animator>().SetLayerWeight(2, 0);

        instantiateSkeleton(position);

        GetComponent<Inventory>().EmptyCollected();
        GetComponent<Inventory>().AffectEffectSaved();
        GetComponent<avatarController>().moveEnable = true;
        displayNoFilter = true;
        isDead = false;
        cptTry++;
    }

    public IEnumerator BlinkWhite(bool DieAfterBlink)
    {
        SpriteRenderer sprite_avatar = GetComponent<SpriteRenderer>();

        for (int i =0; i<3; i++)
        {
            sprite_avatar.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.2f);
            sprite_avatar.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }

        if (DieAfterBlink)
        {
            StartCoroutine(FadeImage(false));
            yield return new WaitForSeconds(2f);
            DeathReset();
        }

        yield break;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                lifeImageEvnt.color = new Color(0, 0, 0, i);
                yield return null;
            }
            displayNoFilter = true;
        }
        // fade from transparent to opaque
        else
        {
            displayNoFilter = false;
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                lifeImageEvnt.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
