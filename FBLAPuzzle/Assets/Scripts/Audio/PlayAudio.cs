using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PlayAudio : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource audioSource;
    public AudioClip movementSound;
    public AudioSource movementAudioSource;
    public AudioClip correctSound;
    public AudioSource correctAudioSource;
    public AudioClip wrongSound;
    public AudioSource wrongAudioSource;
    public AudioMixerGroup audioMixer;
    public AudioSource[] audArry;
    public float sliderValue;
    private GameObject volumePercentageText;
    private int randomNumber;
    private bool audioActive=true;
    void Start()
    {
        randomNumber = Random.Range(0, clip.Length);
        audioSource = audArry[0];
        movementAudioSource = audArry[1];
        correctAudioSource = audArry[2];
        wrongAudioSource = audArry[3];
        audioSource.clip = clip[randomNumber];
        audioSource.playOnAwake = false;
        movementAudioSource.volume = .5f;
        movementAudioSource.clip = movementSound;
        movementAudioSource.playOnAwake = false;
        movementAudioSource.outputAudioMixerGroup = audioMixer;
        correctAudioSource.volume = 5;
        correctAudioSource.clip = correctSound;
        correctAudioSource.playOnAwake = false;
        correctAudioSource.outputAudioMixerGroup = audioMixer;
        wrongAudioSource.volume = .5f;
        wrongAudioSource.clip = wrongSound;
        wrongAudioSource.playOnAwake = false;
        correctAudioSource.outputAudioMixerGroup = audioMixer;
        sliderValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying&&audioActive)
        {
            randomNumber = Random.Range(0, clip.Length);
            audioSource.clip = clip[randomNumber];
            startPlayingAudio();
        }
    }
    public void startPlayingAudio() {
        audioSource.Play();
    }
    public void changeVolume(float vol) {
        audioSource.volume = vol;
    }
    public void playMovementSound()
    {
        movementAudioSource.Play();
    }
    public void playWrongSound() {
        wrongAudioSource.Play();
    }
    public void playCorrectSound() {
        correctAudioSource.Play();
    }
    public void setScrollBarVal(float val) {
        sliderValue = val;
        volumePercentageText = GameObject.Find("AudioVolumeText");
        volumePercentageText.GetComponent<TMPro.TextMeshProUGUI>().text = "Volume: " + (int)((sliderValue * 100))+"%";
    }
}
