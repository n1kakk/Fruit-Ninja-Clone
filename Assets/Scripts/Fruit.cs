using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private ParticleSystem juiceParticalEffect;
    public GameObject juiceSpotPrefab;
    [SerializeField] private SkinManager skinManager;


    private void Awake(){

        // Get the particle system component from the children of the fruit
        juiceParticalEffect = GetComponentInChildren<ParticleSystem>();
    }


    // Method to create sliced fruit effect
    public void CreateSlicedFruit(){

        // Instantiate the sliced fruit prefab
        GameObject inst = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        // Get the slice sound from the selected skin
        AudioClip sliceSound = skinManager.GetSelectedSkin().sound;
        
        // Play the slice sound
        FindObjectOfType<GameManag>().PlaySliceSound(sliceSound);

        // Add explosion force to the sliced fruit pieces
        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody r in rbsOnSliced){
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

        // Increase score and coins
        FindObjectOfType<GameManag>().IncreaseScore(3);
        FindObjectOfType<GameManag>().IncreaseCoins(2);

        // Destroy the sliced fruit after 5 seconds
        Destroy(inst.gameObject, 5);
    }


    // Method to create juice spot effect
    public void CreateJuiceSpot(){

        // Instantiate the juice spot prefab
        GameObject juiceSpot = (GameObject)Instantiate(juiceSpotPrefab, transform.position, Quaternion.identity);

        // Get the sprite renderer of the juice spot
        SpriteRenderer sr = juiceSpot.GetComponent<SpriteRenderer>();

        // Start the fade-out effect
        StartCoroutine(FadeOut(sr, 2f));

        // Destroy the juice spot after 5 seconds
        Destroy(juiceSpot.gameObject, 5);
    }


    // Coroutine to fade out the juice spot
    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float duration)
    {
        float alphaVal = spriteRenderer.color.a;
        Color newColor = spriteRenderer.color;

        while (alphaVal > 0)
        {
            alphaVal -= Time.deltaTime / duration;
            newColor.a = Mathf.Clamp(alphaVal, 0, 1);
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Destroy the sprite renderer and the fruit game object
        Destroy(spriteRenderer.gameObject);
        Destroy(gameObject);
    }

    
    // Method called when another collider enters the fruit's trigger collider
    private void OnTriggerEnter2D(Collider2D other) {
        Blade b = other.GetComponent<Blade>();
        if(!b) return;

        // Play juice particle effect
        if (juiceParticalEffect != null) {
            juiceParticalEffect.transform.parent = null;
            juiceParticalEffect.transform.position = transform.position; 
            juiceParticalEffect.Play();

            // Start coroutine to disable particles after they play
            StartCoroutine(DisableParticlesAfterPlay(juiceParticalEffect));
        }     

        // Create juice spot and sliced fruit effects
        CreateJuiceSpot();   
        CreateSlicedFruit();
    }


    // Coroutine to disable particle system after it finishes playing
    private IEnumerator DisableParticlesAfterPlay(ParticleSystem particles) {
        yield return new WaitForSeconds(particles.main.duration + particles.main.startLifetime.constantMax);
        particles.Stop();
        particles.gameObject.SetActive(false);
    }
}
