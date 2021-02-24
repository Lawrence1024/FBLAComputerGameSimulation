using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int randomNumber;
    private bool audioActive=true;
    void Start()
    {
        randomNumber = Random.Range(0, clip.Length);
        //Debug.Log(randomNumber);
        audioSource = gameObject.AddComponent<AudioSource>();
        movementAudioSource=gameObject.AddComponent<AudioSource>();
        correctAudioSource=gameObject.AddComponent<AudioSource>();
        wrongAudioSource= gameObject.AddComponent<AudioSource>(); ;
        audioSource.clip = clip[randomNumber];
        audioSource.playOnAwake = false;
        movementAudioSource.volume = .5f;
        movementAudioSource.clip = movementSound;
        movementAudioSource.playOnAwake = false;
        correctAudioSource.volume = 5;
        correctAudioSource.clip = correctSound;
        correctAudioSource.playOnAwake = false;
        wrongAudioSource.volume = .5f;
        wrongAudioSource.clip = wrongSound;
        wrongAudioSource.playOnAwake = false;
        //movementAudioSource.Play();

        //startPlayingAudio();
        //pauseAudio();
        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.clip = clip;
        //randomStartPoint();
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
    public void pauseAudio(){
        //audioActive = false;
        //audioSource.Pause();
    }
    public void startPlayingAudio() {
        //audioActive = true;
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
}
