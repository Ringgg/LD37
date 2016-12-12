using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> dieClips = new List<AudioClip>();
    public AudioClip bossDeath;
    public AudioClip bossLaugh;
    public AudioClip bossSpecial;
    public List<AudioClip> bossHitClips = new List<AudioClip>();

    public static AudioManager instance;

    void Awake()
    {
        instance = this;
    }

    public void SetClip(AudioSource source, AudioClip audioClip)
    {
        source.clip = audioClip;
    }

    public void SetRandomClipFromList(AudioSource source, List<AudioClip> audioClips)
    {
        source.clip = audioClips.ElementAt(Random.Range(0, audioClips.Count - 1));
    }
}
