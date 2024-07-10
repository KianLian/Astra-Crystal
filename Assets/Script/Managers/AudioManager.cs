using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;  } = null;

    [Header("Music")]
    [SerializeField]
    private AudioMixer mainMixer = null;

    [SerializeField]
    private Text[] musicText = null;
    [SerializeField]
    private Slider myMusicslider = null;

    [SerializeField]
    private AudioSource[] musicsToPlay = null;

    [Header("Sound Effects")]
    [SerializeField]
    private Text[] soundText = null;
    [SerializeField]
    private Slider mySoundslider = null;

    [Header("Adjust Sliders")]
    private int percentageToSlider = 20;
    private int percentageToText = 100;



    public void ChangeMusicVolume(float volumeValue)
    {
        Debug.Log(volumeValue);

        mainMixer.SetFloat("MusicVolume", Mathf.Log(volumeValue) * percentageToSlider);
        string volumeText = Mathf.FloorToInt(volumeValue * percentageToText).ToString();
        myMusicslider.SetValueWithoutNotify(volumeValue);   
        string percentage = "%";

        for (int i = 0; i < musicText.Length; i++)
        {
            musicText[i].text = volumeText + percentage;

        }
    }
   
    public void ChangeSoundVolume(float volumeValue)
    {

        mainMixer.SetFloat("SoundEffetcsVolume", Mathf.Log(volumeValue) * percentageToSlider);
        mySoundslider.SetValueWithoutNotify(volumeValue);
        string volumeText = Mathf.FloorToInt(volumeValue * percentageToText).ToString();
        string percentage = "%";
        for (int i = 0; i < soundText.Length; i++)
        {
            soundText[i].text = volumeText + percentage;

        }
    }

   public void StopMusic()
    {

        for (int a = 0; a < musicsToPlay.Length; a++)
        {
            musicsToPlay[a].Stop();
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeMusic(int i)
    {
        if(!musicsToPlay[i].isPlaying)
        {
            StopMusic();

            musicsToPlay[i].Play();
        }
      

    }

}
