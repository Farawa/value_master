using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSpawner : Singleton<TableSpawner>
{
    [SerializeField] private GameObject tablePrefab;
    public void SpawnTable(GameObject itemPrefab, int sampleCount, int answerCount)
    {
        var table = Instantiate(tablePrefab, transform);
        table.GetComponent<Table>().SpawnItems(Level.Instance.prefab, sampleCount, answerCount);
    }
}
