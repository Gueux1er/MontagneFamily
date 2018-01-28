using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarTimeline : MonoBehaviour {

    public enum AvatarAge {YOUNG, ADULT, OLD, DEAD};
    private AvatarAge age;
    public Collider2D[] colliderObjectsAge;
    public float currentTime;
    public float lifeExpectancy = 100f;
    public Slider timeSlider;
    public Image timedImage;
    public float flashSpeed = 5f;
    public float ratioTime = 1;

    avatarController avatarController;
    bool isDeadOld;
    bool timed;

    FMOD.Studio.EventInstance essouflement; //Instanciation du son
    bool playingEssouflement = false;
    FMOD.Studio.EventInstance mortVieillissement; //Instanciation du son

    // Use this for initialization
    void Start () {

        avatarController = GetComponent<avatarController>();
        currentTime = 0f; ;
        age = AvatarAge.YOUNG;

        essouflement = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Essouflement"); // Chemin du son 
        mortVieillissement = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Mort_Vieillissement"); // Chemin du son 
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime * ratioTime;
        if((int)currentTime == 0.3* lifeExpectancy && age != AvatarAge.ADULT)
        {
            age = AvatarAge.ADULT;
            avatarController.setAllAgePourAudio(1.0f);
            timed = true;
            GetComponent<Animator>().SetLayerWeight(1, 1);
            colliderObjectsAge[0].enabled = false;
            colliderObjectsAge[1].enabled = true;

        } else if ((int)currentTime == 0.8* lifeExpectancy && age != AvatarAge.OLD)
        {
            age = AvatarAge.OLD;
            avatarController.setAllAgePourAudio(2.0f);
            timed = true;
            GetComponent<Animator>().SetLayerWeight(2, 1);
            colliderObjectsAge[1].enabled = false;
            colliderObjectsAge[2].enabled = true;

        } else if ((int)currentTime == lifeExpectancy - 5 && !playingEssouflement) 
        {
            //L'avatar n'a plus que 5 secondes à vivre
            essouflement.start(); // Jouer un son une fois
            playingEssouflement = true;

        } else if((int)currentTime == lifeExpectancy && age != AvatarAge.DEAD)
        {
            age = AvatarAge.DEAD;
            mortVieillissement.start(); // Jouer un son une fois

            essouflement.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            GetComponent<avatarLife>().Death();
        }

        if (timed)
        {
            timedImage.color = new Color(0.8f, 0.8f, 0.8f, 0.3f);
            StartCoroutine(GetComponent<avatarLife>().BlinkWhite(false));

        }
        else
        {
            timedImage.color = Color.Lerp(timedImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        timed = false;
        timeSlider.value = currentTime;
	}

    public void ResetTimeline()
    {
        currentTime = 0f; ;
        age = AvatarAge.YOUNG;
        colliderObjectsAge[0].enabled = true;
        colliderObjectsAge[2].enabled = false;
    }
}
