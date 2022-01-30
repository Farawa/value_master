using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nerds.Saves;

public class CompleteWindow : WindowCore
{
	#region Methods
	public void Next()
	{
		Map.Instance.Next();
		Hide();
	}
	#endregion
}
