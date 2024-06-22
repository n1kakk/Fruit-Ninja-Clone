using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject slicedFruitPrefab;
    private ParticleSystem juiceParticalEffect;

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

    }
}
