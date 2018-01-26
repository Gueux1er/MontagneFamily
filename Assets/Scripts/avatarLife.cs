using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarLife : MonoBehaviour
{

    public int startingLife = 5;
    public int maxLife = 5;
    public int currentLife;
    public Image lifeImageEvnt;
    public float flashSpeed = 5f;
    public Color damagedColour = new Color(1f, 0f, 0f, 0.3f);
    public Color healedColour = new Color(0f, 1f, 0f, 0.3f);

    public Sprite heartFull;
    public Sprite heartEmpty;

    public Image[]  hearts;


    avatarController avatarController;
    bool isDeadOld;
    bool damaged;
    bool healed;

    // Use this for initialization
    void Start()
    {

        avatarController = GetComponent<avatarController>();
        currentLife = startingLife;

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
        else
        {
            lifeImageEvnt.color = Color.Lerp(lifeImageEvnt.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        currentLife = 3;
        UpdateHearts();
        damaged = false;
        healed = false;
    }

    public void TakeDamage(int value)
    {
        damaged = true;
        currentLife -= value;

        if(currentLife <= 0)
        {
            // TODO : death
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
            // TODO : set with correct empty heart image
            hearts[i].sprite = heartEmpty;
        }
        for(int i=maxLife-currentLife; i<maxLife; i++)
        {
            // TODO : set with correct heart image
            hearts[i].sprite = heartFull;
        }
    }
}
