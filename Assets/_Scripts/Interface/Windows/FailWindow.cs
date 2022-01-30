using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nerds.Saves;

public class FailWindow : WindowCore
{
	#region Methods
	public void Restart()
	{
		Map.Instance.LoadLevel();
		Hide();
	}
	#endregion
}
