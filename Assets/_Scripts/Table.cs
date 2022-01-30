using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Table : Singleton<Table>
{
    [SerializeField] private Transform samplesSpace;
    [SerializeField] private Transform answersSpace;
    [SerializeField] private Vector2 itemsAreaSize;
    [SerializeField] private TextMeshPro sampleText;
    [SerializeField] private TextMeshPro answerText;
    [SerializeField] private GameObject[] borders;
    [SerializeField] private Transform itemsParent;

    [Space]
    [SerializeField] private Vector2 spacing;
    private Vector3 itemSize;
    [Space]
    [SerializeField] private bool isShowArea;
    [SerializeField] private bool isShowItemPositions;

    private List<Vector3> itemSamplePositions = new List<Vector3>();
    private List<Vector3> itemAnswerPositions = new List<Vector3>();

    private void CalculatePlaces()
    {
        var itemPosition = new Vector3(-itemsAreaSize.x / 2, 0, itemsAreaSize.y / 2 + spacing.y);
        for (int i = 0; i < 200; i++)
        {
            itemSamplePositions.Add(samplesSpace.position + itemPosition);
            itemAnswerPositions.Add(answersSpace.position + itemPosition);
            itemPosition.x += itemSize.x + spacing.x;
            if (itemPosition.x > itemsAreaSize.x / 2)
            {
                itemPosition.x = -itemsAreaSize.x / 2;
                itemPosition.z -= itemSize.z + spacing.y;
            }
        }
    }

    public void SpawnItems(GameObject itemPrefab, int samples, int answers)
    {
        print(itemPrefab.name);
        var item = itemPrefab.GetComponent<Item>();
        if (item.isUseCustomSize)
        {
            itemSize = item.customSize;
        }
        else
        {
            var sizeItem = itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size;
            var scaleItem = itemPrefab.transform.localScale;
            itemSize = new Vector3(sizeItem.x * scaleItem.x, sizeItem.y * scaleItem.y, sizeItem.z * scaleItem.z);
        }
        var positionS = samplesSpace.position;
        var positionA = answersSpace.position;
        var additionalYSpace = itemSize.y / 2;
        positionS.y += additionalYSpace;
        positionA.y += additionalYSpace;
        samplesSpace.position = positionS;
        answersSpace.position = positionA;
        CalculatePlaces();
        for (int i = 0; i < samples; i++)
        {
            //var xPos = Random.Range(-itemsAreaSize.x / 2, itemsAreaSize.x / 2) + samplesSpace.position.x;
            //var zPos = Random.Range(-itemsAreaSize.y / 2, itemsAreaSize.y / 2) + samplesSpace.position.z;
            Instantiate(itemPrefab, itemSamplePositions[i], itemPrefab.transform.rotation, itemsParent);
        }
        for (int i = 0; i < answers; i++)
        {
            //var xPos = Random.Range(-itemsAreaSize.x / 2, itemsAreaSize.x / 2) + answersSpace.position.x;
            //var zPos = Random.Range(-itemsAreaSize.y / 2, itemsAreaSize.y / 2) + answersSpace.position.z;
            Instantiate(itemPrefab, itemAnswerPositions[i], itemPrefab.transform.rotation, itemsParent);
        }
        sampleText.text = samples.ToString();
    }

    public TextMeshPro GetAnswerText()
    {
        return answerText;
    }

    private void OnDrawGizmos()
    {
        if (isShowArea)
        {
            Gizmos.DrawCube(samplesSpace.position, new Vector3(itemsAreaSize.x, 0.1f, itemsAreaSize.y));
            Gizmos.DrawCube(answersSpace.position, new Vector3(itemsAreaSize.x, 0.1f, itemsAreaSize.y));
        }
        if (isShowItemPositions)
        {
            var itemPosition = new Vector3(-itemsAreaSize.x / 2 + spacing.x, 0, itemsAreaSize.y / 2 + spacing.y);
            for (int i = 0; i < 200; i++)
            {
                Gizmos.DrawCube(samplesSpace.position + itemPosition, itemSize);
                Gizmos.DrawCube(answersSpace.position + itemPosition, itemSize);
                itemPosition.x += itemSize.x + spacing.x;
                if (itemPosition.x > itemsAreaSize.x / 2)
                {
                    itemPosition.x = -itemsAreaSize.x / 2 + spacing.x;
                    itemPosition.z -= itemSize.z + spacing.y;
                }
            }
        }
    }

    public void DestroyTable()
    {
        Luke.Instance.Open();
        foreach (var border in borders)
            border.SetActive(false);
        StartCoroutine(DestroyTableCoroutine());
    }

    private IEnumerator DestroyTableCoroutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
