using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayMusic : MonoBehaviour {

    FMOD.Studio.EventInstance ambianceMenu; //Instanciation du son
    //FMOD.Studio.EventInstance ambianceJeu; //Instanciation du son

    FMOD.Studio.EventInstance ambiance;

	public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
	public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up


	private float resetTime = .01f;					//Very short time used to fade in near instantly without a click

    public GameObject pf_fmodEmitterMenu;
    private GameObject fmodEmitter;

    void Awake () 
	{
        fmodEmitter = Instantiate(pf_fmodEmitterMenu);

        ambianceMenu = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/Menu"); // Chemin du son 
        //ambianceJeu = FMODUnity.RuntimeManager.CreateInstance("event:/Environnement/Ambiance"); // Chemin du son 

    }


	public void PlayLevelMusic()
	{
		//This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
		switch (SceneManager.GetActiveScene().buildIndex)
		{
			//If scene index is 0 (usually title scene) assign the clip titleMusic to musicSource
			case 0:
                fmodEmitter = Instantiate(pf_fmodEmitterMenu);
                break;
			//If scene index is 1 (usually main scene) assign the clip mainMusic to musicSource
			case 1:
                //ambiance = ambianceJeu;
				break;
		}
		//Fade up the volume very quickly, over resetTime seconds (.01 by default)
		//FadeUp (resetTime);
        //Play the assigned music clip in musicSource
        //ambiance.start();
    }
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(int musicChoice)
	{

		//This switch looks at the integer parameter musicChoice to decide which music clip to play.
		switch (musicChoice) 
		{
		//if musicChoice is 0 assigns titleMusic to audio source
		case 0:
                fmodEmitter = Instantiate(pf_fmodEmitterMenu);
                break;
			//if musicChoice is 1 assigns mainMusic to audio source
		case 1:
                //ambiance = ambianceJeu;
			break;
		}
        //Play the selected clip
       // ambiance.start();

    }

	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
        //call the TransitionTo function of the audioMixerSnapshot volumeUp;
        //volumeUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
        //call the TransitionTo function of the audioMixerSnapshot volumeDown;
        //volumeDown.TransitionTo (fadeTime);
        //ambiance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Destroy(fmodEmitter);
    }

}
