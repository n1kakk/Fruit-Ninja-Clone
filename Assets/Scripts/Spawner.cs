using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    [Header("Play")]
    public GameObject playPanel;


    void Start()
    {
        // Check if the current scene is the game scene (build index 1)
        if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            StartCoroutine(SpawnFruits());
            playPanel.SetActive(true);
        }  
    }

    void Update() 
    {
        // Increase the bomb probability over time
        bombProbability += bombProbabilityIncreaseRate * Time.deltaTime;

        // Clamp the bomb probability to the maximum value
        if (bombProbability > maxBombProbability)
        {
            bombProbability = maxBombProbability;
        }
    }


    // Coroutine to spawn fruits and bombs
    private IEnumerator SpawnFruits(){

        while(true)
        {
            // Wait for a random time between minWait and maxWait
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            // Select a random spawn point
            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject obj = null;

            // Randomly determine whether to spawn a bomb or a fruit
            float p = Random.Range(0, 100);
            if(p<bombProbability){
                obj = bomb;
            }else{
                obj = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
            }

            // Instantiate the selected object at the spawn point
            GameObject fruit = Instantiate(obj, t.position, t.rotation);

            // Apply a random force to the spawned object
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            // Destroy the spawned object after 5 seconds
            Destroy(fruit, 5);
        }
    }
    
}
