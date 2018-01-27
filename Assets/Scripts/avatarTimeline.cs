using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarTimeline : MonoBehaviour {

    public enum AvatarAge { YOUNG, ADULT, OLD, DEAD};
    private AvatarAge age;
    public float currentTime;
    public Slider timeSlider;
    public Image timedImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);

    avatarController avatarController;
    bool isDeadOld;
    bool timed;

	// Use this for initialization
	void Start () {

        avatarController = GetComponent<avatarController>();
        currentTime = 0f; ;
        age = AvatarAge.YOUNG;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if((int)currentTime == 3)
        {
            age = AvatarAge.ADULT;
            avatarController.setAgePourCollectible(1.0f);
            timed = true;

        } else if ((int)currentTime == 7){
            age = AvatarAge.OLD;
            avatarController.setAgePourCollectible(2.0f);
            timed = true;
        }


        if(currentTime <= 0)
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
