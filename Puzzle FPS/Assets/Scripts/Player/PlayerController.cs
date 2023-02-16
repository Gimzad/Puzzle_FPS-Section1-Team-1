using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("-----Components-----")]
    [SerializeField] CharacterController controller;

    [Header("-----Player Stats-----")]
    [Range(5, 10)][SerializeField] int hp;
    [Range(0, 5)][SerializeField] float moveSpeed;
    [Range(1.5f, 5f)][SerializeField] float sprintMod;
    [Range(10, 25)][SerializeField] float jumpSpeed;
    [Range(0, 3)][SerializeField] int jumpTimes;

    [Range(15, 45)][SerializeField] float gravity;
    [Range(1, 5)][SerializeField] float playerForce;

    [Header("-----Weapon Stats-----")]
    [SerializeField] List<Weapon> weaponList = new List<Weapon>();
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shotDamage;
    [SerializeField] GameObject weaponModel;


    int jumpsCurrent;
    Vector3 move;
    Vector3 playerVelocity;
    bool isShooting;
    int hpOriginal;

    public int selectedWeapon;

    #region Public Access Methods
    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float SprintMod
    {
        get { return sprintMod; }
        set { sprintMod = value; }
    }
    public int JumpTimes
    {
        get { return jumpTimes; }
        set { jumpTimes = value; }
    }
    public float JumpSpeed
    {
        get { return jumpSpeed; }
        set { jumpSpeed = value; }
    }
    public float PlayerGravity
    {
        get { return gravity; }
        set { gravity = value; }
    }
    public float PlayerForce
    {
        get { return playerForce; }
        set { playerForce = value; }
    }
    public float ShootRate
    {
        get { return shootRate; }
        set { shootRate = value; }
    }
    public int ShootDistance
    {
        get { return shootDist; }
        set { shootDist = value; }
    }
    public int ShotDamage
    {
        get { return shotDamage; }
        set { shotDamage = value; }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        hpOriginal = hp;
        UpdatePlayerHPBar();
    }
    void Update()
    {
        Movement();
        Sprint();
        SelectWeapon();

        if (!isShooting && Input.GetButton("Fire") && weaponList.Count > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    void Movement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpsCurrent = 0;
        }
        move = (transform.right * Input.GetAxis("Horizontal") +
               (transform.forward * Input.GetAxis("Vertical")));
        move = move.normalized;

        controller.Move(move * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && jumpsCurrent < jumpTimes)
        {
            jumpsCurrent++;
            playerVelocity.y = jumpSpeed;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * playerForce, transform.position, ForceMode.Impulse);
        }
    }
        void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            moveSpeed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            moveSpeed /= sprintMod;
        }
    }
    IEnumerator Shoot()
    {
        isShooting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDist))
        {
            if (hit.collider.GetComponent<IDamage>() != null)
            {
                hit.collider.GetComponent<IDamage>().TakeDamage(shotDamage);
            }
        }

        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        UpdatePlayerHPBar();
        StartCoroutine(FlashDamage());

        if (hp <= 0)
        {
            GameManager.Instance.LoseGame();
        }
    }
    IEnumerator FlashDamage()
    {
        HUDManager.Instance.PlayerDamageFlashScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        HUDManager.Instance.PlayerDamageFlashScreen.SetActive(false);
    }

    public void UpdatePlayerHPBar()
    {
        HUDManager.Instance.UpdateHPBarFill((float)hp / (float)hpOriginal);
    }
    public void PickupWeapon(Weapon weapon)
    {
        weaponList.Add(weapon);

        shootRate = weapon.ShootRate;
        shootDist = weapon.ShootDist;
        shotDamage = weapon.ShotDamage;

        weaponModel.GetComponent<MeshFilter>().sharedMesh = weapon.WeaponModel.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weapon.WeaponModel.GetComponent<MeshRenderer>().sharedMaterial;

        selectedWeapon = weaponList.Count - 1;
    }
    void SelectWeapon()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedWeapon < weaponList.Count - 1)
        {
            selectedWeapon++;
            ChangeWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedWeapon > 0)
        {
            selectedWeapon--;
            ChangeWeapon();
        }
    }

    void ChangeWeapon()
    {
        shootRate = weaponList[selectedWeapon].ShootRate;
        shootDist = weaponList[selectedWeapon].ShootDist;
        shotDamage = weaponList[selectedWeapon].ShotDamage;

        weaponModel.GetComponent<MeshFilter>().sharedMesh = weaponList[selectedWeapon].WeaponModel.GetComponent<MeshFilter>().sharedMesh;
        weaponModel.GetComponent<MeshRenderer>().sharedMaterial = weaponList[selectedWeapon].WeaponModel.GetComponent<MeshRenderer>().sharedMaterial;
    }
    public void PlayerRespawn()
    {
        controller.enabled = false;
        transform.position = GameManager.Instance.PlayerSpawnPos.transform.position;

        hp = hpOriginal;
        UpdatePlayerHPBar();

        controller.enabled = true;
    }
}
