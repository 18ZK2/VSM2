using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCtr : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] float randompich;
    AudioSource ass;
    // Start is called before the first frame update
    void Start()
    {
        ass = GetComponent<AudioSource>();
    }

    public void Sound(int i)
    {
        if (ass != null)
        {
            ass.pitch = 1f + Random.Range(-randompich, randompich);
            ass.PlayOneShot(clips[i]);
        }
    }
}
