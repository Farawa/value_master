using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickEffect : MonoBehaviour
{
	#region Settings
	[SerializeField] private InternalSettings _internalSettings;
	[System.Serializable]
	protected class InternalSettings
	{
		
	}
	#endregion
	#region Variables
	#endregion
	#region UnityMethods
	private void Update()
	{
		transform.localScale = Vector3.one * PingPong1To0(Time.time/4, 0.9f, 1f);
	}
	#endregion
	#region TickEffectLogic
	float PingPong1To0(float aValue, float aMin, float aMax)
	{
		float offset = 0.707f * (aMax - aMin); // sqrt(2)/2
		return Mathf.PingPong(aValue + offset, aMax - aMin) + aMin;
	}
	#endregion
}
