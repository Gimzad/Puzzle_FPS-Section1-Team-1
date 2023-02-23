using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("-----Components-----")]
    [SerializeField] CharacterController controller;
    [SerializeField] CapsuleCollider capsule;
    [SerializeField] AudioSource Audio;

    [Header("-----Player Stats-----")]
    [Range(5, 10)][SerializeField] int hp;
    [Range(0, 5)][SerializeField] float moveSpeed;
    [Range(1.5f, 5f)][SerializeField] float sprintMod;
    [Range(10, 25)][SerializeField] float jumpSpeed;
    [Range(0, 3)][SerializeField] int jumpTimes;
    [Range(0, 0.9f)][SerializeField] float crouchHeight;

    [Range(15, 45)][SerializeField] float gravity;
    [Range(1, 5)][SerializeField] float playerForce;

    public float zoomMax;
    public int zoomInSpeed;
    public int zoomOutSpeed;

    [Header("-----Weapon Stats-----")]
    public List<Weapon> weaponList = new();
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shotDamage;
    [SerializeField] GameObject weaponModel;
    [SerializeField] GameObject explosionObject;
    [SerializeField] Transform weaponModelDefaultPos;
    [SerializeField] Transform weaponModelADS;
    public int ADSSpeed;
    public int NotADSSpeed;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] audioSteps;
    [Range(0, 1)][SerializeField] float audioStepsVol;
    [SerializeField] AudioClip[] audioJump;
    [Range(0, 1)][SerializeField] float audioJumpVol;
    [SerializeField] AudioClip[] audioHurt;
    [Range(0, 1)][SerializeField] float audioHurtVol;

    bool isPlayingSteps;

    public bool isDead;

    Platform activePlatform;
    Vector3 lastPlatformVelocity;
    Vector3 activeLocalPlatformPoint;
    Vector3 activeGlobalPlatformPoint;
    Quaternion activeLocalPlatformRotation;
    Quaternion activeGlobalPlatformRotation;

    bool zooming;

    int jumpsCurrent;
    bool isShooting;
    bool isCrouching;
    int hpOriginal;
    float normalHeight;
    float zoomOrig;
    public float moveSpeedOrig;
    Vector3 move;
    Vector3 playerVelocity;
    bool isSprinting;

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
    public int ShootDist
    {
        get { return shootDist; }
        set { shootDist = value; }
    }
    public int ShotDamage
    {
        get { return shotDamage; }
        set { shotDamage = value; }
    }
    public GameObject WeaponModel
    {
        get { return weaponModel; }
        set { weaponModel = value; }
    }
    #endregion
    private void Awake()
    {
        isDead = false;
        activePlatform = null;
        capsule = GetComponent<CapsuleCollider>();
        weaponModel.transform.localPosition = WeaponModel.transform.localPosition;
        //ResetWeaponPos();
    }
    void Start()
    {
        zoomOrig = Camera.main.fieldOfView;
        normalHeight = controller.height;
        moveSpeedOrig = moveSpeed;
        hpOriginal = hp;
        UpdatePlayerHPBar();
    }
    void Update()
    {
        HandlePlatforms();
        Movement();
        Crouch();
        Sprint();
        SelectWeapon();
        ZoomCamera();

        if (!isShooting && Input.GetButton("Fire") && weaponList.Count > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    void Movement()
    {
        activePlatform = null;
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
            Audio.PlayOneShot(audioJump[Random.Range(0, audioJump.Length)], audioJumpVol);
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (controller.isGrounded && move.normalized.magnitude > 0.8f && !isPlayingSteps)
        {
            StartCoroutine(playSteps());
        }
    }

    IEnumerator playSteps()
    {
        isPlayingSteps = true;
        Audio.PlayOneShot(audioSteps[Random.Range(0, audioSteps.Length)], audioStepsVol);
        if (isSprinting)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        isPlayingSteps = false;
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
        Platform tempPlat = hit.collider.GetComponent<Platform>();
        if (tempPlat != null)
        {
            activePlatform = tempPlat;
        }
    }
    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (!isCrouching)
            {
                isCrouching = true;
                controller.height = crouchHeight;
                capsule.height = crouchHeight;

            }
            else
            {
                isCrouching = false;
                controller.height = normalHeight;
                capsule.height = normalHeight;
            }
        }
    }

    void SprintInput()
    {
        if (Input.GetButton(PlayerPreferences.Instance.Button_Sprint) && !zooming)
        {
            isSprinting = true;
        }
        else if (Input.GetButtonUp(PlayerPreferences.Instance.Button_Sprint))
        {
            isSprinting = false;
        }
    }

    void Sprint()
    {
        SprintInput();

        if (isSprinting)
        {
            if (moveSpeed < (moveSpeedOrig * sprintMod))
            {
                moveSpeed *= sprintMod;
            }
        }
        else
        {
            if (moveSpeed != moveSpeedOrig)
            {
                moveSpeed /= sprintMod;
            }
        }
    }
    IEnumerator Shoot()
    {
        isShooting = true;

        if (weaponList[selectedWeapon].ThisIsARocketLauncher)
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit, shootDist))
            {
                if (hit.collider.GetComponent<IDamage>() != null)
                {
                    hit.collider.GetComponent<IDamage>().TakeDamage(shotDamage);
                }
                Instantiate(explosionObject, hit.transform);
            }
        }
        else
        {
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out RaycastHit hit, shootDist))
            {
                if (hit.collider.GetComponent<IDamage>() != null)
                {
                    hit.collider.GetComponent<IDamage>().TakeDamage(shotDamage);
                }
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
        Audio.PlayOneShot(audioHurt[Random.Range(0, audioHurt.Length)], audioHurtVol);

        if (hp <= 0)
        {
            isDead = true;
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

    public void ResetWeaponPos()
    {
        weaponModelDefaultPos.localPosition = new Vector3(0, 0, 0);
        weaponModel.transform.localPosition = new Vector3(0, 0, 0);
        weaponModelADS.localPosition = new Vector3(0, 0, 0);
    }

    public void PickupWeapon(Weapon weapon)
    {
        weaponList.Add(weapon);
        ResetWeaponPos();
        
        weaponModelDefaultPos.localPosition = weapon.weaponModelDefaultPos;
        weaponModel.transform.localPosition = weapon.weaponPosition;
        weaponModelADS.localPosition = weapon.weaponModelADS;
        shootRate += weapon.ShootRate;
        shootDist += weapon.ShootDist;
        shotDamage += weapon.ShotDamage;
        //zoomInSpeed += weapon.ZoomInSpeed;
        //zoomOutSpeed += weapon.ZoomOutSpeed;
        //ADSSpeed += weapon.ADSSpeed;
        //NotADSSpeed += weapon.NotADSSpeed;
        //zoomMax += weapon.zoomMax;

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
        ResetWeaponPos();

        shootRate = weaponList[selectedWeapon].ShootRate;
        shootDist = weaponList[selectedWeapon].ShootDist;
        shotDamage = weaponList[selectedWeapon].ShotDamage;
        weaponModelDefaultPos.localPosition += weaponList[selectedWeapon].weaponModelDefaultPos;
        weaponModel.transform.localPosition += weaponList[selectedWeapon].weaponPosition;
        weaponModelADS.localPosition += weaponList[selectedWeapon].weaponModelADS;

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
    void ZoomInput()
    {
        if (Input.GetButtonDown(PlayerPreferences.Instance.Button_Zoom))
        {
            zooming = true;
        }
        else if (Input.GetButtonUp(PlayerPreferences.Instance.Button_Zoom))
        {
            zooming = false;
        }
    }

    void ZoomCamera()
    {
        Debug.Log(zooming);
        ZoomInput();

        if (zooming)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomMax, Time.deltaTime * zoomInSpeed);

            if (isSprinting)
            {
                isSprinting = false;
            }
            if (weaponList.Count > 0)
            {
                weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, weaponModelADS.localPosition, Time.deltaTime * ADSSpeed);
            }
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomOrig, Time.deltaTime * zoomOutSpeed);

            if (weaponList.Count > 0)
            {
                weaponModel.transform.localPosition = Vector3.Lerp(weaponModel.transform.localPosition, weaponModelDefaultPos.localPosition, Time.deltaTime * NotADSSpeed);
            }
        }
    }

    void HandlePlatforms()
    {
        GetPlatformInformation();
        if (activePlatform != null)
        {
            Vector3 newGlobalPlatformPoint = activePlatform.transform.position;
            Vector3 moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);

            if (moveDistance != Vector3.zero)
                controller.Move(moveDistance);
            //rotation platforms
            //Quaternion newGlobalPlatformRotation = activePlatform.transform. rotation * activeLocalPlatformRotation;
            //Quaternion rotationDiff = newGlobalPlatformRotation * Quaternion.Inverse(activeGlobalPlatformRotation);
            // Prevent rotation of the local up vector
            //rotationDiff = Quaternion.FromToRotation(rotationDiff * transform.up, transform.up) * rotationDiff;
            //transform.rotation = rotationDiff * transform.rotation;
        }
        else
        {
            lastPlatformVelocity = Vector3.zero;
        }
    }
    void GetPlatformInformation()
    {
        if (activePlatform != null)
        {
            activeGlobalPlatformPoint = transform.position;
            activeLocalPlatformPoint = activePlatform.transform.localPosition;
            //activeGlobalPlatformRotation = transform.rotation;
            //activeLocalPlatformRotation = Quaternion.Inverse(activePlatform.transform.rotation) * transform.rotation;
        }
    }
}
