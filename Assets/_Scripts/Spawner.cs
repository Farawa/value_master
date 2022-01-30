using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 0.1f;
    public Action onSpawnOne;
    public void Spawn(GameObject prefab, int count)
    {
        StartCoroutine(SpawnCoroutine(prefab,count));
    }

    private IEnumerator SpawnCoroutine(GameObject prefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            var item = Instantiate(prefab,transform);
            onSpawnOne?.Invoke();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public GameObject[] GetChildrens()
    {
        List<GameObject> childrens = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
        {
            childrens.Add(transform.GetChild(i).gameObject);
        }
        return childrens.ToArray();
    }
}
