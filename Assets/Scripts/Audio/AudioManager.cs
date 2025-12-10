using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Database")]
    [SerializeField] private AudioDatabase database;

    [Header("Pool Settings")]
    [SerializeField] private int poolSize = 50;

    private Queue<AudioEvent> eventQueue = new Queue<AudioEvent>();
    private List<AudioSource> sourcePool = new List<AudioSource>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        CreatePool();
    }

    /// <summary>
    /// 초기 AudioSource 풀 생성
    /// </summary>
    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = new GameObject("AudioSource_" + i);
            obj.transform.parent = transform;

            AudioSource source = obj.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 1f;      // default: 3D

            sourcePool.Add(source);
        }
    }

    /// <summary>
    /// 외부에서 사운드 요청
    /// </summary>
    public void EnqueueEvent(AudioEvent audioEvent)
    {
        eventQueue.Enqueue(audioEvent);
    }

    private void Update()
    {
        while (eventQueue.Count > 0)
        {
            var e = eventQueue.Dequeue();
            PlayInternal(e);
        }
    }

    /// <summary>
    /// 실제 재생 처리
    /// </summary>
    private void PlayInternal(AudioEvent e)
    {
        AudioData data = database.Get(e.soundId);
        if (data == null)
        {
            Debug.LogWarning($"Audio ID '{e.soundId}' not found in database.");
            return;
        }

        AudioSource src = GetAvailableSource();
        if (src == null) return;

        src.transform.position = e.position;

        src.clip = data.clip;
        src.outputAudioMixerGroup = data.mixerGroup;

        src.volume = data.defaultVolume * e.volume;
        src.spatialBlend = data.is3D ? 1f : 0f;

        src.Play();
    }

    /// <summary>
    /// 사용 가능한 AudioSource 반환
    /// </summary>
    private AudioSource GetAvailableSource()
    {
        foreach (var src in sourcePool)
        {
            if (!src.isPlaying)
                return src;
        }

        // 모든 소스가 재생 중이면 우선순위 낮은 소스 끊기
        return sourcePool[0];
    }
}
