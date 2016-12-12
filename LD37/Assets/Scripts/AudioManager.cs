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
    public AudioClip warriorAttack;
    public AudioClip healSound;
    public List<AudioClip> bossHitClips = new List<AudioClip>();

    private AudioSource myPrivateAudio;
    public static AudioManager instance;

    void Awake()
    {
        instance = this;
        myPrivateAudio = GetComponent<AudioSource>();
    }

    public void SetClip(AudioSource source, AudioClip audioClip)
    {
        source.clip = audioClip;
    }

    public void SetRandomClipFromList(AudioSource source, List<AudioClip> audioClips)
    {
        source.clip = audioClips.ElementAt(Random.Range(0, audioClips.Count - 1));
    }

    public void PlaySomeAudio(AudioSource audio)
    {
        myPrivateAudio.clip = audio.clip;
        myPrivateAudio.Play();
    }
}
