using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("SpawnPoints")]
    [SerializeField] List<GameObject> SpawnPoints;

    [Header("Target Type Prefabs")]
    [SerializeField] List<GameObject> TargetTypes;

    private int SpawnPointChoice;
    private int SpawnTargetChoice;
    private float SpawnCooldown;

    
    void Start()
    {
        SpawnPointChoice = Random.Range(0, 3);
        SpawnTargetChoice = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(SpawnCooldown >= 1.5)
        {
            Instantiate(TargetTypes[SpawnTargetChoice], SpawnPoints[SpawnPointChoice].transform.position, Quaternion.identity);
            SpawnCooldown = 0;
            SpawnPointChoice = Random.Range(0, 3);
            SpawnTargetChoice = Random.Range(0, 3);
        }
        SpawnCooldown = SpawnCooldown + Time.deltaTime;
    }
}
