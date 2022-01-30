using System.Collections;
using System.Collections.Generic;
using Nerds.Saves;
using TMPro;
using UnityEngine;

public class Interface : Singleton<Interface>
{
	#region Variables
	[Header("State Settings")]
	[SerializeField] private GameObject _startState;
	[SerializeField] private GameObject _gameState;
	[SerializeField] private TextMeshProUGUI _levelLabel;
	[Header("Windows Settings")]
	[SerializeField] private WindowsPreset _windows;
	public WindowsPreset Windows => _windows;


	#endregion

	#region UnityMethods
	public override void Awake()
	{
		base.Awake();
		Map.Instance.OnStatusChange += StatusChangeHandler;
		Application.targetFrameRate = 60;
	}

	public void SetLevel()
	{
		if (_levelLabel) _levelLabel.text = $"LEVEL {(Saves.Level + 1)}";
	}
	private void StatusChangeHandler(Map.GameStatus status)
	{
		switch (status)
		{
			case Map.GameStatus.Complete:
				_startState.SetActive(false);
				_windows.Complete.Show();
				_windows.Fail.Hide();
				break;
			case Map.GameStatus.Fail:
				_startState.SetActive(false);
				_windows.Complete.Hide();
				_windows.Fail.Show();
				break;
			case Map.GameStatus.Game:
				_startState.SetActive(false);
				_windows.Complete.Hide();
				_windows.Fail.Hide();
				break;
			case Map.GameStatus.Start:
				//_startState.SetActive(true);
				_windows.Complete.Hide();
				_windows.Fail.Hide();
				break;
		}
	}
	#endregion

	#region Structs
	[System.Serializable]
	public struct WindowsPreset
	{
		public CompleteWindow Complete;
		public FailWindow Fail;
	}
	#endregion

	#region Enums

	#endregion
}
