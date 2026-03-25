using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip rsound0;
    public AudioClip rsound1;
    public AudioClip rsound2;
    // Start is called before the first frame update

    public void RobotPlaySound(int soundNum)
    {
        
        switch (soundNum)
        {
            case 0: AudioSource.PlayClipAtPoint(rsound0, transform.position); break;
            case 1: AudioSource.PlayClipAtPoint(rsound1, transform.position); break;
            case 2: AudioSource.PlayClipAtPoint(rsound2, transform.position); break;
                    
        }
    }
}