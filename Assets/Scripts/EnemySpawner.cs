using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public EnemyBase[] EnemyPrefabs;
    [Range(1.0f, 0.01f)]
    public float TimeBetweenSpawns = 1;
    private float CurrentTime = 0;
    private Collider2D AreaCollider;

    private void Start()
    {
        AreaCollider = GetComponent<Collider2D>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentTime > TimeBetweenSpawns)
        {
            SpawnEnemy();
            CurrentTime = 0;
        }

        CurrentTime += Time.deltaTime;

    }

    private void SpawnEnemy()
    {
        EnemyBase newSpawn = Instantiate<EnemyBase>(EnemyPrefabs[Random.Range(0,EnemyPrefabs.Length)]);
        Vector2 extents = AreaCollider.bounds.extents;
        Vector2 spawnPosition = transform.position + new Vector3(Random.Range(-extents.x, extents.x), Random.Range(-extents.y , extents.y) , 0);
        newSpawn.transform.position = spawnPosition;
    }
}
