using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Singleton Pattern, yay :)
    public static MusicManager Instance;

    [SerializeField]
    private MusicLibrary music_library;
    [SerializeField]
    private AudioSource[] music_sources;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMusic(int sourceIndex, string trackName, float fadeDuration=0f)
    {
        StartCoroutine(AnimateMusicCrossfade(sourceIndex, music_library.GetClipFromName(trackName), fadeDuration));
    }

    IEnumerator AnimateMusicCrossfade(int sourceIndex, AudioClip nextTrack, float fadeDuration = 0.5f)
    {
        AudioSource music_source = music_sources[sourceIndex];
        float percent = 0f;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            music_source.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;
        }

        if (nextTrack != null)
        {
            music_source.clip = nextTrack;
            music_source.Play();
            percent = 0f;
            while (percent < 1)
            {
                percent+= Time.deltaTime * 1 / fadeDuration;
                music_source.volume = Mathf.Lerp(0, 1f, percent);
                yield return null;
            }
        }
    }
}