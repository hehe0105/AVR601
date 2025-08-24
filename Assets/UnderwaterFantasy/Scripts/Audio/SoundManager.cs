using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager I;

    public AudioSource bgmSource;
    public AudioSource sfxPrefab;
    public int poolSize = 8;

    private readonly System.Collections.Generic.Queue<AudioSource> pool = new System.Collections.Generic.Queue<AudioSource>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (I != null) {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i< poolSize; i++) {
            var src = Instantiate(sfxPrefab,transform);
            src.playOnAwake = false;
            pool.Enqueue(src);
        }
    }
    public void PlayBGM(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (clip == null) return;
        if (bgmSource == null) return;
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.volume = volume;
        bgmSource.spatialBlend = 0f; // 2D
        bgmSource.Play();
    }
    public void StopBGM() { if (bgmSource) bgmSource.Stop(); }
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        var src = GetSource2D();
        src.PlayOneShot(clip, volume);
        ReturnAfter(src, clip.length);
    }

    public void PlaySFXRandom(AudioClip[] clips, float volume = 1f, float pitchMin = 0.95f, float pitchMax = 1.05f)
    {
        if (clips == null || clips.Length == 0) return;
        var clip = clips[Random.Range(0, clips.Length)];
        var src = GetSource2D();
        src.pitch = Random.Range(pitchMin, pitchMax);
        src.PlayOneShot(clip, volume);
        ReturnAfter(src, clip.length);
        src.pitch = 1f; // 复位
    }
    public void PlaySFXAt(AudioClip clip, Vector3 pos, float volume = 1f, float minDistance = 1f, float maxDistance = 20f)
    {
        if (clip == null) return;
        var src = GetSource3D(pos, minDistance, maxDistance);
        src.PlayOneShot(clip, volume);
        ReturnAfter(src, clip.length);
    }

    // ===== 内部工具 =====
    private AudioSource GetSource2D()
    {
        var src = pool.Count > 0 ? pool.Dequeue() : gameObject.AddComponent<AudioSource>();
        src.transform.localPosition = Vector3.zero;
        src.spatialBlend = 0f; // 2D
        return src;
    }

    private AudioSource GetSource3D(Vector3 pos, float minDistance, float maxDistance)
    {
        var src = pool.Count > 0 ? pool.Dequeue() : gameObject.AddComponent<AudioSource>();
        src.transform.position = pos;
        src.spatialBlend = 1f; // 3D
        src.minDistance = minDistance;
        src.maxDistance = maxDistance;
        return src;
    }

    private async void ReturnAfter(AudioSource src, float t)
    {
        await System.Threading.Tasks.Task.Delay(Mathf.Max(1, (int)(t * 1000)));
        src.pitch = 1f;
        pool.Enqueue(src);
    }
}
