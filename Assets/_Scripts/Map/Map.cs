using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nerds.Saves;
using UnityEngine.Events;

public class Map : Singleton<Map>
{
    #region Variables
    [SerializeField] private bool _isTest;
    [SerializeField] private bool isRandomLevelsOrder = true;
    [SerializeField] private int _length = -1;
    [Space]
    [SerializeField] private int maxSafe = 40;
    [SerializeField] private int minSafe = 10;
    [SerializeField] private int reduceSafe = 2;
    public int currentSafeRange
    {
        get
        {
            int currentValue = maxSafe - (reduceSafe * Saves.Level);
            return Mathf.Clamp(currentValue, minSafe, maxSafe);
        }
    }
    public Level CurrentLevel { get; private set; }


    private GameStatus _status;
    public GameStatus Status
    {
        get
        {
            return _status;
        }
        set
        {
            _status = value;
            OnStatusChange?.Invoke(_status);
        }
    }

    public UnityAction<GameStatus> OnStatusChange { get; set; }

    #endregion

    #region UnityMethods
    public override void Awake()
    {
        base.Awake();

        LoadLevel();
    }
    #endregion

    #region Generator
    public void LoadLevel()
    {
        CurrentLevel = GetComponentInChildren<Level>();
        if (Table.Instance)
            Table.Instance.DestroyTable();
        // return; // Закомментить это

        if (!_isTest)
        {
            if (CurrentLevel)
                DestroyImmediate(CurrentLevel.gameObject);

            CurrentLevel = Instantiate(Resources.Load<GameObject>($"Levels/Level_{Saves.Location}"), transform).GetComponent<Level>();
            CurrentLevel.transform.localPosition = Vector3.zero;
            CurrentLevel.transform.localEulerAngles = Vector3.zero;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Status = (GameStatus)((((int)Status) + 1) % 4);
        }
    }
    public void Next()
    {
        Saves.Level++;
        if (isRandomLevelsOrder)
        {
            while (true)
            {
                var random = Random.Range(0, _length);
                if (random != Saves.Location)
                {
                    Saves.Location = random;
                    break;
                }
            }
        }
        else
        {
            Saves.Location++;
            if (Saves.Location >= _length)
                Saves.Location = 0;
        }
        if (_length < 0)
        {
            _length = Resources.LoadAll<GameObject>("Levels").Length;
        }
        LoadLevel();
    }

    public enum GameStatus
    {
        Start,
        Game,
        Fail,
        Complete
    }
    #endregion
}
