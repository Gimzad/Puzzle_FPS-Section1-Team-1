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
    [Range(10, 25)][SerializeField] float jumpSpeed;
    [Range(0, 3)][SerializeField] int jumpTimes;
    [Range(15, 45)][SerializeField] float gravity;
    [Header("-----Weapon Stats-----")]
    [SerializeField] float shootRate;
    [SerializeField] int shootDist;
    [SerializeField] int shotDamage;
    int jumpsCurrent;
    Vector3 move;
    Vector3 playerVelocity;
    bool isShooting;
    int hpOriginal;

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
        if (!isShooting && Input.GetButton("Fire"))
        {
            StartCoroutine(Shoot());
        }
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            playerVelocity.y = 0;
            jumpsCurrent = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal") +
            transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * moveSpeed);


        if (Input.GetButtonDown("Jump") && jumpsCurrent <= jumpTimes)
        {
            jumpsCurrent++;
            playerVelocity.y = jumpSpeed;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

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
}
