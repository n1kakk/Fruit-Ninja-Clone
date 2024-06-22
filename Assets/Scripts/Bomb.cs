using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip fuseSound; // звук горения фитиля
    public AudioClip explosionSound; // звук взрыва

    private AudioSource fuseAudioSource; // для звука горения фитиля
    private AudioSource explosionAudioSource; // для звука взрыва
    private bool isFuseSoundPlaying = false;

    private void Awake()
    {
        // Добавляем два AudioSource компонента
        fuseAudioSource = gameObject.AddComponent<AudioSource>();
        explosionAudioSource = gameObject.AddComponent<AudioSource>();

        // Назначаем аудиоклипы
        fuseAudioSource.clip = fuseSound;
        explosionAudioSource.clip = explosionSound;

        // Настраиваем повторяющийся звук горения фитиля
        fuseAudioSource.loop = true;
    }
        private void OnBecameVisible()
    {
        // Начинаем воспроизведение звука горения фитиля, когда объект виден на экране
        if (!isFuseSoundPlaying)
        {
            fuseAudioSource.Play();
            isFuseSoundPlaying = true;
        }
    }
        private void OnBecameInvisible()
    {
        // Останавливаем воспроизведение звука горения фитиля, когда объект не виден на экране
        if (isFuseSoundPlaying)
        {
            fuseAudioSource.Stop();
            isFuseSoundPlaying = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Blade b = collision.GetComponent<Blade>();
        if(!b) return;

                // Останавливаем звук горения фитиля и воспроизводим звук взрыва
        if (isFuseSoundPlaying)
        {
            fuseAudioSource.Stop();
            isFuseSoundPlaying = false;
        }
        explosionAudioSource.Play();
    
        FindObjectOfType<GameManag>().OnBombHit();

    }
    
}
