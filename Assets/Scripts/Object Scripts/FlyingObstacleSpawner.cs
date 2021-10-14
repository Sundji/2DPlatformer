using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObstacleSpawner : MonoBehaviour
{

    public Transform ObstacleParent;

    public Vector2 SpawnInterval;
    public List<Vector2> SpawnPoints;

    public List<FlyingObstacle> ObjectsToSpawn;

    public Vector2 DirectionOffset;

    private Transform self;

    private float timer;

    private void Awake()
    {
        self = transform;
        timer = SpawnInterval.RandomValue();
    }

    private void Update()
    {

        if (GameManager.GM == null || GameManager.GM.InGame == false)
            return;

        if (ObjectsToSpawn.Count == 0 || SpawnPoints.Count == 0)
            return;

        timer = timer - Time.deltaTime;

        if (timer <= 0.0f)
        {

            Vector2 startingPoint = RandomSpawnPoint();
            Vector2 endingPoint = RandomSpawnPoint();

            while (endingPoint == startingPoint || endingPoint.x == startingPoint.x || endingPoint.y == startingPoint.y)
                endingPoint = RandomSpawnPoint();

            Vector2 directionOffset = new Vector2(new Vector2(0, DirectionOffset.x).RandomValue(), new Vector2(0, DirectionOffset.y).RandomValue());
            Vector2 direction = (endingPoint - startingPoint- directionOffset).normalized;

            int objectToSpawnIndex = (int)new Vector2(0, ObjectsToSpawn.Count).RandomValue();

            Transform parent = ObstacleParent == null ? self : ObstacleParent;

            FlyingObstacle obstacle = Instantiate(ObjectsToSpawn[objectToSpawnIndex], startingPoint, Quaternion.identity, parent);
            obstacle.SetInMotion(direction);

            timer = SpawnInterval.RandomValue();

        }

    }

    private Vector2 RandomSpawnPoint()
    {
        return SpawnPoints[(int)new Vector2(0, SpawnPoints.Count).RandomValue()];
    }

}
