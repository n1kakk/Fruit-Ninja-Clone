using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private ParticleSystem juiceParticalEffect;
    public GameObject juiceSpotPrefab;


    private void Awake(){
        juiceParticalEffect = GetComponentInChildren<ParticleSystem>();
            if (juiceParticalEffect == null) {
        //Debug.LogError("ParticleSystem не найден! Убедитесь, что он является дочерним объектом.");
    }
    }
    public void CreateSlicedFruit(){
        GameObject inst = (GameObject)Instantiate(slicedFruitPrefab, transform.position, transform.rotation);

        FindObjectOfType<GameManag>().PlayRandomSliceSound();

        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody r in rbsOnSliced){
            r.transform.rotation = Random.rotation;
            r.AddExplosionForce(Random.Range(500, 1000), transform.position, 5f);
        }

        FindObjectOfType<GameManag>().IncreaseScore(3);
        Destroy(inst.gameObject, 5);
    }

    public void CreateJuiceSpot(){
        GameObject juiceSpot = (GameObject)Instantiate(juiceSpotPrefab, transform.position, Quaternion.identity);
        SpriteRenderer sr = juiceSpot.GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut(sr, 2f));
        Destroy(juiceSpot.gameObject, 5);

    }

    private IEnumerator FadeOut(SpriteRenderer spriteRenderer, float duration)
    {
        float alphaVal = spriteRenderer.color.a;
        Color newColor = spriteRenderer.color;

        while (alphaVal > 0)
        {
            alphaVal -= Time.deltaTime / duration;
            newColor.a = Mathf.Clamp(alphaVal, 0, 1);
            spriteRenderer.color = newColor;

            //Debug.Log($"Current alpha: {spriteRenderer.color.a}");

            yield return null;
        }

        //Debug.Log("Object destroyed");
        Destroy(spriteRenderer.gameObject);
        Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        Blade b = other.GetComponent<Blade>();
        if(!b) return;

        //Debug.Log("Фрукт разрезан. Попытка воспроизвести ParticleSystem.");

        if (juiceParticalEffect != null) {
            juiceParticalEffect.transform.parent = null;
            juiceParticalEffect.transform.position = transform.position; 
            juiceParticalEffect.Play();

            StartCoroutine(DisableParticlesAfterPlay(juiceParticalEffect));

           // Destroy(juiceParticalEffect.gameObject, juiceParticalEffect.main.duration + juiceParticalEffect.main.startLifetime.constantMax);
            //Debug.Log("ParticleSystem воспроизведен.");
        }     
        CreateJuiceSpot();   
        CreateSlicedFruit();
    }


    private IEnumerator DisableParticlesAfterPlay(ParticleSystem particles) {
        yield return new WaitForSeconds(particles.main.duration + particles.main.startLifetime.constantMax);
        particles.Stop();
        particles.gameObject.SetActive(false);
    }
}
