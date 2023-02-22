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
	public static float CrouchHeightDefault = 0.5f;
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

	public static float ZoomMaxDefault = 35f;
	public static int ZoomInSpeedDefault = 6;
	public static int ZoomOutSpeedDefault = 8;
	#endregion
	#region Button Code Defaults
	public static string JumpKeyDefault = "Jump";
	public static string CrouchKeyDefault = "Crouch";
	public static string SprintKeyDefault = "Sprint";
	public static string MenuKeyDefault = "Escape";
	public static string FireKeyDefault = "Fire";
	public static string InteractKeyDefault = "Interact";
	public static string ZoomKeyDefault = "Zoom";
	#endregion

	[Header("Active Movement Settings")]
	#region Active Movement Settings
	[Range(0f, 50)]
	public float MoveSpeed = MoveSpeedDefault;
	[Range(0f, 50)]
	public float CrouchHeight = CrouchHeightDefault;
	[Range(1.5f, 2.5f)]
	public float SprintMod = SprintModDefault;
	[Range(0f, 3f)]
	public int JumpTimes = JumpTimesDefault;
	[Range(0, 10)]
	public float JumpSpeed = JumpSpeedDefault;
	[Range(0, 40f)]
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

	public float ZoomMax = ZoomMaxDefault;
	public int ZoomInSpeed = ZoomInSpeedDefault;
	public int ZoomOutSpeed = ZoomOutSpeedDefault;
	#endregion

	[Header("Active Buttons")]
	#region Button Codes
	public string Button_Jump = JumpKeyDefault;
	public string Button_Crouch = CrouchKeyDefault;
	public string Button_Sprint = SprintKeyDefault;
	public string Button_Menu = MenuKeyDefault;
	public string Button_Fire = FireKeyDefault;
	public string Button_Interact = InteractKeyDefault;
	public string Button_Zoom = ZoomKeyDefault;
    #endregion
}
