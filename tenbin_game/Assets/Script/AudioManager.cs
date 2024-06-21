using UnityEngine;
using System.Collections.Generic;

public class AudioManager : SingletonBehavior<AudioManager>
{
    //AudioSourceのコンポーネントを追加してアタッチ
    public AudioSource bgmSource;
    public AudioSource seSource;

    // SEのサウンドクリップを格納するためのDictionary
    private Dictionary<string, AudioClip> seClips = new Dictionary<string, AudioClip>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // SEのサウンドクリップを追加
    public void AddSEClip(string clipName, AudioClip clip)
    {
        if (!seClips.ContainsKey(clipName))
        {
            seClips.Add(clipName, clip);
        }
        else
        {
            Debug.LogWarning("Duplicate entry for SE clip: " + clipName);
        }
    }

    // SEを再生するメソッド
    public void PlaySE(string clipName)
    {
        if (seClips.ContainsKey(clipName))
        {
            seSource.PlayOneShot(seClips[clipName]);
        }
        else
        {
            Debug.LogWarning("SE clip not found: " + clipName);
        }
    }

    // BGMを再生するメソッド
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    // BGMの再生を停止するメソッド
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}