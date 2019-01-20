using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Aim Assist Data", fileName = "Aim_Assist_Data_00")]
public class AimAssistSettings : ScriptableObject 
{
	public AnimationCurve distanceAimAssistStrength;
	public AnimationCurve distanceAimAssistLength;

	public AnimationCurve distanceAimAssistRadius;

	[Range(0.001f,10)]
	public float aimAssistPercentage = 1;

	public bool disableAimAssist;

}
