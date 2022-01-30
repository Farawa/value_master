using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Nerds.Analytics;
using Nerds.Saves;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level : Singleton<Level>
{
    #region Variables
    public GameObject prefab;
    [SerializeField] private Vector2Int samplesRange;
    public Vector2Int itemsRange;
    [HideInInspector] public int samplesCount;
    [HideInInspector] public int itemsCount;
    #endregion

    #region UnityMethods
    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GetComponentInParent<Map>().Status = Map.GameStatus.Start;
        Interface.Instance.SetLevel();
        StartCoroutine(Begin());
    }
    #endregion
    #region LevelLogic
    public IEnumerator Begin()
    {
        GetComponentInParent<Map>().Status = Map.GameStatus.Game;
        Analytics.OnLevelStart?.Invoke(Saves.Level);

        samplesCount = Random.Range(samplesRange.x, samplesRange.y);
        itemsCount = Random.Range(itemsRange.x, itemsRange.y);

        yield return new WaitForSeconds(0.5f);
        TableSpawner.Instance.SpawnTable(prefab, samplesCount, itemsCount);
        SliderValue.Instance.SetActive(true, true);
    }
    public void Complete()
    {

        Analytics.OnLevelComplete?.Invoke(Saves.Level);
        GetComponentInParent<Map>().Status = Map.GameStatus.Complete;
        Taptic.Success();
        Saves.Level++;
        Saves.Location++;
    }

    public void Fail()
    {
        Taptic.Failure();
        Analytics.OnLevelFail?.Invoke(Saves.Level);
        GetComponentInParent<Map>().Status = Map.GameStatus.Fail;
    }
    #endregion

}