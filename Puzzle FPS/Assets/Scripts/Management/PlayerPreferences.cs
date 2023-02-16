using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerPreferences: MonoBehaviour
{
	public static PlayerPreferences Instance;

    public void Awake()
    {
		Instance = this;
    }

	#region Movement Defaults
	public static float MoveSpeedDefault = 6f;
	public static float SprintModDefault = 1.5f;
	public static int HPDefault = 10;

	public static int JumpTimesDefault = 1;
	public static float JumpSpeedDefault = 2.5f;

    public static float PlayerGravityStrengthDefault = 9.8f;
    public static float PlayerForceStrengthDefault = 2f;
	#endregion
	#region Camera Defaults
	public static int SensitivityHorizontalDefault = 400;
	public static int SensitivityVerticalDefault = 300;
	public static int VerticalLockMinDefault = -60;
	public static int VerticalLockMaxDefault = 75;

	public static bool InvertXDefault = false;
	#endregion
	#region Weapon Defaults
	public static float ShootRateDefault = 0.25f;
	public static int ShootDistanceDefault;
	public static int ShotDamageDefault;
	#endregion
	#region Button Code Defaults
	public static KeyCode JumpKeyDefault = KeyCode.Space;
	public static KeyCode SprintKeyDefault = KeyCode.LeftShift;
	public static KeyCode MenuKeyDefault = KeyCode.Escape;
	public static KeyCode FireKeyDefault = KeyCode.Mouse0;
	#endregion

	[Header("Active Movement Settings")]
	#region Active Movement Settings
	[Range(0f, 50)]
	public float MoveSpeed = MoveSpeedDefault;
	[Range(1.5f, 2.5f)]
	public float SprintMod = SprintModDefault;
	[Range(0f, 5f)]
	public int JumpTimes = JumpTimesDefault;
	[Range(0, 10)]
	public float JumpSpeed = JumpSpeedDefault;
	[Range(0, 20f)]
	public float PlayerGravityStrength = PlayerGravityStrengthDefault;
	[Range(0.01f, 0.1f)]
	public float PlayerForceStrength = PlayerForceStrengthDefault;
	#endregion
	[Header("Active Player Settings")]
	#region Weapon Settings
	[Range(0.1f, 10f)]
	public int HP = HPDefault;
	#endregion

	[Header("Active Camera Settings")]
	#region Active Camera Settings
	[Range(100, 500)]
	public int SensitivityHorizontal = SensitivityHorizontalDefault;
	[Range(100, 500)]
	public int SensitivityVertical = SensitivityVerticalDefault;
	[Range(-90f, 0f)]
	public int VerticalLockMin = VerticalLockMinDefault;
	[Range(0f, 90)]
	public int VerticalLockMax = VerticalLockMaxDefault;

	public bool InvertX = InvertXDefault;
	#endregion

	[Header("Active Weapon Settings")]
	#region Weapon Settings
	[Range(0.1f, 10f)]
	public float ShootRate = ShootRateDefault;
	[Range(1, 25)]
	public int ShootDistance = ShootDistanceDefault;
	[Range(1, 10)]
	public int ShotDamage = ShotDamageDefault;
	#endregion

	[Header("Active Buttons")]
	#region Button Codes
	public string Button_Jump = "Jump";
	public string Button_Sprint = "Sprint";
	public string Button_Menu = "Cancel";
	public string Button_Fire = "Fire";
	public string Button_Interact = "Interact";
    #endregion
}
