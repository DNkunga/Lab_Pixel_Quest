using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer _mixer;
    
    public GameObject coinSFX;
    public GameObject heartSFX;
    public GameObject deathSFX;
    public GameObject checkpointSFX;

    public void PlaySFX(string audioName)
    { 
        StartCoroutine(CreateSFX(audioName));
    }
    
    public IEnumerator CreateSFX(string audioName)
    {
        GameObject newAudio = Instantiate(GetSFX(audioName), Vector3.zero, Quaternion.identity);
        newAudio.GetComponent<AudioSource>().Play();
        while (newAudio.GetComponent<AudioSource>().isPlaying)
        {
            yield return null;
        }
        Destroy(newAudio);
    }
    
    public GameObject GetSFX(string audioName) 
    {
        switch (audioName)
        {
            case Structs.SoundEffects.coin:
                {
                    return coinSFX;
                }
            case Structs.SoundEffects.heart:
                {
                    return heartSFX;
                }
            case Structs.SoundEffects.death:
                {
                    return deathSFX;
                }
            case Structs.SoundEffects.checkpoint:
                {
                    return checkpointSFX;
                }
        }
        return null;
    }

    public void UpdateSFXGroup(float newVolume) 
    {
        _mixer.SetFloat((Structs.Mixers.sfxVolume), Mathf.Log10(newVolume) * 20 );
    }

    public void UpdateMusicGroup(float newVolume)
    {
        _mixer.SetFloat((Structs.Mixers.musicVolume), Mathf.Log10(newVolume) * 20);
    }
}
