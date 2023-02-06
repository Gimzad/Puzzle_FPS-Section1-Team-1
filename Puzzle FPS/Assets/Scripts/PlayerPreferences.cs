using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPreferences
{
	static PlayerPreferences()
	{
	}
	private PlayerPreferences()
	{
	}
	public static PlayerPreferences Instance { get; } = new PlayerPreferences();

	#region Movement Defaults
	public static float MaxSpeedDefault = 10f;

	public static float JumpHeightDefault = 5f;
	public static int MaxAirJumpsDefault = 1;

	public static float MaxGroundAngleDefault = 25f;
    public static float PlayerGravityStrengthDefault = 9.8f;
    #endregion
    #region Camera Defaults
    public static float CameraFocusRadiusDefault = 0.5f;
	public static float CameraRotationSpeedDefault = 180f;
	#endregion
	[Header("Movement Settings")]
	#region Movement Settings
	[Range(0f, 50)]
	public float MaxSpeed = MaxSpeedDefault;
	[Range(0f, 10f)]
	public float JumpHeight = JumpHeightDefault;
	[Range(0, 5)]
	public int MaxAirJumps = MaxAirJumpsDefault;
	[Range(0, 90)]
	public float MaxGroundAngle = MaxGroundAngleDefault;
	#endregion
	//Air control and climbing stuff here but commented out in case they are unused
	/*public static float MaxClimbSpeedDefault = 2f;

	public static bool AirControlDefault = true;
	public static float MaxClimbAngleDefault = 140f;
	public static float GripStrengthDefault = 1f;
	*/
	[Header("Buttons")]
	#region Button Codes
	public KeyCode PLAYERJUMPKEY = KeyCode.Space;
	public KeyCode PLAYERSPRINTKEY = KeyCode.LeftShift;
	public KeyCode PLAYERMENUKEY = KeyCode.M;
	public KeyCode PLAYERCLIMBKEY = KeyCode.Mouse0;
    #endregion
}
