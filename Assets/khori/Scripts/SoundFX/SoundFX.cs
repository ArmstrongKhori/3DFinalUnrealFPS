using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A "Sound Effect" clip used by the Sounder.
/// </summary>
public class SoundFX : MonoBehaviour
{
    public string soundName;
    public AudioSource aus;

    // Use this for initialization
    void Start()
    {
        aus = gameObject.GetComponent<AudioSource>();
        if (aus == null) { aus = gameObject.AddComponent<AudioSource>(); }
    }


    /// <summary>
    /// Loads a clip and plays it.
    /// </summary>
    /// <param name="name"></param>
    public void LoadAndPlay(string clipname)
    {
        soundName = clipname;
        //
        aus.clip = (AudioClip)Resources.Load(soundName);
        //
        aus.Play(0);
    }
}
