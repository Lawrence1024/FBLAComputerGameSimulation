using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SetVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void modifyVolume(float sliderValue) {
        GameObject.Find("AudioPlayer").GetComponent<PlayAudio>().setScrollBarVal(sliderValue);
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
