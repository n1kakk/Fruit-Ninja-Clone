using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private ParticleSystem juiceParticalEffect;
    public GameObject juiceSpotPrefab;


    private void Awake(){
        juiceParticalEffect = GetComponentInChildren<ParticleSystem>();
            if (juiceParticalEffect == null) {
        Debug.LogError("ParticleSystem не найден! Убедитесь, что он является дочерним объектом.");
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
        Destroy(gameObject);
    }

    public void CreateJuiceSpot(){
        GameObject juiceSpot = (GameObject)Instantiate(juiceSpotPrefab, transform.position, Quaternion.identity);
        SpriteRenderer sr = juiceSpot.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            StartCoroutine(FadeOut(sr, 1f));
        }

    }
    

    private IEnumerator FadeOut(SpriteRenderer yourSpriteRenderer, float duration)
    {
        float alphaVal = yourSpriteRenderer.color.a;
        Color tmp = yourSpriteRenderer.color;

        while (yourSpriteRenderer.color.a > 0)
        {
            alphaVal -= Time.deltaTime / duration;
            tmp.a = alphaVal;
            yourSpriteRenderer.color = tmp;

            yield return null;
        }

        Destroy(yourSpriteRenderer.gameObject); // Уничтожаем объект после исчезновения
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Blade b = other.GetComponent<Blade>();
        if(!b) return;

        Debug.Log("Фрукт разрезан. Попытка воспроизвести ParticleSystem.");

        if (juiceParticalEffect != null) {
            juiceParticalEffect.transform.parent = null;
            juiceParticalEffect.transform.position = transform.position; 
            juiceParticalEffect.Play();
            Destroy(juiceParticalEffect.gameObject, juiceParticalEffect.main.duration + juiceParticalEffect.main.startLifetime.constantMax);
            Debug.Log("ParticleSystem воспроизведен.");
        } else {
            Debug.LogError("juiceParticalEffect не инициализирован.");
        }        
        CreateSlicedFruit();
        CreateJuiceSpot();

    }
}
