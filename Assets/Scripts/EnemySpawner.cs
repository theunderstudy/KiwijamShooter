using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public EnemyBase[] EnemyPrefabs;
    public float LongestSpawnTime = 2, ShortestSpawnTime = 0.1f;
    [Range(2.0f, 0.1f)]
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

        TimeBetweenSpawns = Mathf.Lerp(LongestSpawnTime , ShortestSpawnTime , GameManager.instance.GameStagePercent());

        //Debug.Log(GameManager.instance.GameStagePercent());
    }

    private void SpawnEnemy()
    {
        EnemyBase newSpawn = Instantiate<EnemyBase>(EnemyPrefabs[Random.Range(0,EnemyPrefabs.Length)]);
        Vector2 extents = AreaCollider.bounds.extents;
        Vector2 spawnPosition = AreaCollider.bounds.center + new Vector3(Random.Range(-extents.x, extents.x), Random.Range(-extents.y , extents.y) , 0);
        newSpawn.transform.position = spawnPosition;
    }
}
