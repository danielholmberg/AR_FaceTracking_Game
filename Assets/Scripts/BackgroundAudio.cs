using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{

    public AudioClip endAudio;
    private AudioSource audioSource;

    #region Singleton
    public static BackgroundAudio Instance;

    private void Awake() {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayEndAudio() {
        audioSource.Stop();
        audioSource.PlayOneShot(endAudio, 0.4f);
    }

    public void PlayBackgroundAudio() {
        audioSource.Stop();
        audioSource.Play();
    }

    public void Pause() 
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }
}
