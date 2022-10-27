using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource breakingGlass;
    public AudioSource engineSound;

    public void playBreakingGlass(){
        breakingGlass.Play();
    }

    public void playEngineSound(){
        engineSound.Play();
    }
}
