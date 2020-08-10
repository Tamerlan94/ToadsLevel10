using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SoundManager : MonoBehaviour
{
    [Header("Music")]
    public AudioClip titleMusic;
    public AudioClip gameMusic;
    [Header("SFX")]
    public AudioClip[] sfx;
    [Header("Button")]
    public Button buttonStart;
    public Button buttonPause;
    public Button buttonResume;
    public Button buttonSound;

    private AudioSource sfxSource;
    private AudioSource musicSource;
    private AudioSource sfxSource2;
    private bool isPause;
    private bool isStop;
    private bool isMuted;
    private int muted;

    public Sprite[] imageButtonSound;

    public static SoundManager current;
    private void Awake()
    {
        current = this;
        sfxSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource2 = this.gameObject.AddComponent<AudioSource>();
        musicSource = this.gameObject.AddComponent<AudioSource>();
    }
    private void Start()
    {
        muted = PlayerPrefs.GetInt("Muted");
        isMuted = Convert.ToBoolean(muted);
        buttonSound.image.sprite = imageButtonSound[muted];
        if (muted == 1)
        {
            musicSource.mute = true;
            sfxSource.mute = true;
            sfxSource2.mute = true;           
        }
        else
        {
            musicSource.mute = false;
            sfxSource.mute = false;
            sfxSource2.mute = false;
        }

        musicSource.clip = titleMusic;
        musicSource.Play();

        buttonStart.onClick.AddListener(StartGame);
        buttonPause.onClick.AddListener(PauseMusic);
        buttonResume.onClick.AddListener(PauseMusic);
        buttonSound.onClick.AddListener(MuteAll);
    }
    private void Update()
    {
        if (isPause)
        {
            sfxSource.volume = 0;
            sfxSource2.volume = 0;
        }
        else
        {
            sfxSource.volume = 1;
            sfxSource2.volume = 1;
        }

        if (isStop)
            sfxSource2.volume = 0;
        else
            sfxSource2.volume = 1;

    }
    public void StartGame()
    {
        musicSource.clip = gameMusic;
        musicSource.volume = .6f;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySFX(int index)
    {
        sfxSource.PlayOneShot(sfx[index]);
    }
    public void PlaySFX(int index, bool loop)
    {
        sfxSource2.clip = sfx[index];
        sfxSource2.loop = loop;
        sfxSource2.Play();
    }
    public void StopSFX()
    {
        sfxSource2.Stop();
    }    
    public void StopMusic()
    {
        musicSource.Stop();
        isStop = true;
    }
    public void PlayMusic()
    {
        musicSource.Play();
        isStop = false;
    }
    public void PauseMusic()
    {
        isPause = !isPause;
        if (isPause)
            musicSource.Pause();
        else if(!isPause)
            musicSource.Play();
    }
    public void MuteAll()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            musicSource.mute = true;
            sfxSource.mute = true;
            sfxSource2.mute = true;
            PlayerPrefs.SetInt("Muted", 1);

            buttonSound.image.sprite = imageButtonSound[1];
        }
        else
        {
            musicSource.mute = false;
            sfxSource.mute = false;
            sfxSource2.mute = false;
            PlayerPrefs.SetInt("Muted", 0);

            buttonSound.image.sprite = imageButtonSound[0];
        }
       
    }
}
