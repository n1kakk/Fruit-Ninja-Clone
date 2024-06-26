using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Header("Sounds")]
    public AudioClip fuseSound; 
    public AudioClip explosionSound; 

    private AudioSource fuseAudioSource; 
    private AudioSource explosionAudioSource; 
    private bool isFuseSoundPlaying = false;

    private void Awake()
    {
        // Add AudioSource components to the game object
        fuseAudioSource = gameObject.AddComponent<AudioSource>();
        explosionAudioSource = gameObject.AddComponent<AudioSource>();

        // Set the audio clips for the audio sources
        fuseAudioSource.clip = fuseSound;
        explosionAudioSource.clip = explosionSound;

        // Set the fuse audio source to loop
        fuseAudioSource.loop = true;
    }


    // Called when the bomb becomes visible on the screen
    private void OnBecameVisible()
    {
        if (!isFuseSoundPlaying)
        {
            fuseAudioSource.Play();
            isFuseSoundPlaying = true;
        }
    }


    // Called when the bomb becomes invisible on the screen
    private void OnBecameInvisible()
    {
        if (isFuseSoundPlaying)
        {
            fuseAudioSource.Stop();
            isFuseSoundPlaying = false;
        }
    }


    // Called when another collider enters the bomb's trigger collider
    private void OnTriggerEnter2D(Collider2D collision) {

        Blade b = collision.GetComponent<Blade>(); // Check if the collision is with a Blade
        if(!b) return;

        if (isFuseSoundPlaying)
        {
            fuseAudioSource.Stop();
            isFuseSoundPlaying = false;
        }
        explosionAudioSource.Play();
    
        FindObjectOfType<GameManag>().OnBombHit(); // Call the OnBombHit method from GameManag

    }
    
}
