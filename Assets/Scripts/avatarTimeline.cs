using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarTimeline : MonoBehaviour {

    public enum AvatarAge { YOUNG, ADULT, OLD, DEAD};
    private AvatarAge age;
    public float currentTime;
    public float lifeExpectancy = 100f;
    public Slider timeSlider;
    public Image timedImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);

    avatarController avatarController;
    bool isDeadOld;
    bool timed;

    FMOD.Studio.EventInstance essouflement; //Instanciation du son

    // Use this for initialization
    void Start () {

        avatarController = GetComponent<avatarController>();
        currentTime = 0f; ;
        age = AvatarAge.YOUNG;

        essouflement = FMODUnity.RuntimeManager.CreateInstance("event:/Avatar/Essouflement"); // Chemin du son 
    }
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if((int)currentTime == 0.3* lifeExpectancy)
        {
            age = AvatarAge.ADULT;
            avatarController.setAgePourCollectible(1.0f);
            timed = true;

        } else if ((int)currentTime == 0.7* lifeExpectancy)
        {
            age = AvatarAge.OLD;
            avatarController.setAgePourCollectible(2.0f);
            timed = true;

        } else if ((int)currentTime == lifeExpectancy - 16) //L'avatar n'a plus que 16 secondes à vivre
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Avatar/Essouflement"); // Jouer un son une fois
        }


        else if(currentTime >= lifeExpectancy)
        {
            // TODO : death
            age = AvatarAge.DEAD;
        }

        if (timed)
        {
            timedImage.color = flashColour;
        } else
        {
            timedImage.color = Color.Lerp(timedImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        timed = false;
        timeSlider.value = currentTime;
	}
}
