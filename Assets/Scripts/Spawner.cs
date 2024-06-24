using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] objectsToSpawn;
    public GameObject bomb;
    public Transform[] spawnPlaces;
    public float minWait = .3f;
    public float maxWait = 1f;

    public float minForce = 12f;
    public float maxForce = 16f;

    public float bombProbability = 10f; 
    public float bombProbabilityIncreaseRate = 0.1f; 
    private float maxBombProbability = 50f;


    void Start()
    {
        StartCoroutine(SpawnFruits());   
    }
    void Update() {
        bombProbability += bombProbabilityIncreaseRate * Time.deltaTime;
        
        if (bombProbability > maxBombProbability)
        {
            bombProbability = maxBombProbability;
        }
    }

    private IEnumerator SpawnFruits(){

        while(true){
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject obj = null;
            float p = Random.Range(0, 100);
            if(p<bombProbability){
                obj = bomb;
            }else{
                obj = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            }

            GameObject fruit = Instantiate(obj, t.position, t.rotation);

            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            Destroy(fruit, 5);
        }
    }


}
