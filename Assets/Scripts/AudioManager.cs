using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;                    //Drag a reference to the audio source which will play the sound effects.
    public AudioSource musicSource;
    public AudioClip[] songs;
    public AudioClip[] engineSounds;
    public AudioClip[] pickupSounds;

    public static AudioManager instance = null;        //Allows other scripts to call functions from SoundManager.                
    public float lowPitchRange = .95f;                //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    int songNumber = 0;


    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
        {
            //if not, set it to this.
            instance = this;
            Setup();
        }
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    void Setup()
    {
        SwapSong(songs[songNumber], songNumber);
    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }

    public void PlayLoop(AudioClip clip)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;
        efxSource.loop = true;
        //Play the clip.
       // efxSource.volume = 0.01f;
        efxSource.Play();
    }

    public void SwapSong(AudioClip clip, int index)
    {
        float volume = 1.0f;
        switch (index)
        {
            case 0:
                volume = 0.8f;
                break;
            case 1:
                volume = 0.9f;
                break;
            case 2:
                volume = 0.25f;
                break;
            case 3:
                volume = 0.4f;
                break;
            case 4:
                volume = 1.0f;
                break;
            default:
                break;
        }
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.volume = volume;
        musicSource.Play();
    }



    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public void RandomizeSfx(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];

        //Play the clip.
        efxSource.Play();
    }

    public void IncreaseDanger()
    {
        if (songNumber < songs.Length - 1)
        {
            songNumber++;
            SwapSong(songs[songNumber], songNumber);
        }
    }

    public void DecreaseDanger()
    {
        if (songNumber > 0)
        {
            songNumber--;
            SwapSong(songs[songNumber], songNumber);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            IncreaseDanger();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DecreaseDanger();
        }
    }

    public void PlayEngineSounds(int index)
    {
        PlayLoop(engineSounds[index]);
    }

    public void PlayPickupSounds(int index)
    {
        PlayLoop(pickupSounds[index]);
    } 




}