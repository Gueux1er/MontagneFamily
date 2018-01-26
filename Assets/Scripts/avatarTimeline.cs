using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class avatarTimeline : MonoBehaviour {

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
		
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        if (((int)currentTime) == 70 || ((int)currentTime) == 30)
        {
            timed = true;
        }

        if(currentTime <= 0)
        {
            // TODO : death
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
