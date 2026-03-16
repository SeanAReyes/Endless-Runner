using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableItem
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }
    public SpawnableItem[] items; 

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var item in items)
        {
            if (spawnChance < item.spawnChance)
            {
                GameObject spawnedItem = Instantiate(item.prefab);
                spawnedItem.transform.position += this.transform.position;
                break;
            }
            spawnChance -= item.spawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
