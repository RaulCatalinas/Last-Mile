using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Crash Clip")]
    [SerializeField] private AudioClip crashClip;

    [Header("Impact Settings")]
    [SerializeField] private float damagePitch = 1.3f;
    [SerializeField] private float deathPitch = 0.8f;
    [SerializeField] private float pitchVariation = 0.05f;
    [SerializeField] private float damageVolume = 0.7f;
    [SerializeField] private float deathVolume = 1f;

    [Header("Dynamic Speed Effect")]
    [SerializeField] private float maxSpeedForEffect = 20f;
    [SerializeField] private float maxPitchReduction = 0.2f;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrash(bool isDeath, float currentSpeed)
    {
        var basePitch = isDeath ? deathPitch : damagePitch;
        var volume = isDeath ? deathVolume : damageVolume;

        var normalizedSpeed = Mathf.Clamp01(currentSpeed / maxSpeedForEffect);
        var dynamicReduction = normalizedSpeed * maxPitchReduction;

        var finalPitch = basePitch - dynamicReduction;

        finalPitch += Random.Range(-pitchVariation, pitchVariation);

        audioSource.pitch = finalPitch;
        audioSource.volume = volume;

        audioSource.PlayOneShot(crashClip);

        audioSource.pitch = 1f;
        audioSource.volume = 1f;
    }
}