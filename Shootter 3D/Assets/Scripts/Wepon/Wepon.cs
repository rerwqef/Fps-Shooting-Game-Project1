using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wepon : MonoBehaviour
{
    public int damage = 100;
    public float range = 100f;

    public GameObject impacteffect;
    public Animator anim;

    public PlayerController playerController;
    public bool aiming = false;
    public bool isReloading = false;
    private int currentAmmo = 0; // Initialize to 0
    public int maxAmmo = 30;
    public int magazineSize = 7; // Set magazine size
    public float timeBetweenShots = 2f;
    private float shotTimer = 0f;
    AudioSource audioSource;
    public AudioClip shootClip;
    public AudioClip reLoadClip;

    // Reference to the TextMeshPro UI Text
    public TextMeshProUGUI ammoText;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentAmmo = magazineSize; // Start with magazine size
        UpdateAmmoText(); // Initialize UI text
    }

    private void Update()
    {
        float playerMovement = playerController.move.magnitude;
        anim.SetFloat("Move", playerMovement);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isReloading)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Scoped();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            Reload();
        }

        // Update shot timer
        shotTimer += Time.deltaTime;
    }

    void Shoot()
    {
        if (currentAmmo > 0 && shotTimer >= timeBetweenShots)
        {
            if (!aiming)
                anim.Play("Shot");
            else if (aiming)
                anim.Play("Aiming_Shot");

            audioSource.PlayOneShot(shootClip);
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                Instantiate(impacteffect, hit.point, Quaternion.LookRotation(hit.normal));
                if (hit.collider.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamge(damage);
                }
            }

            currentAmmo--;
            shotTimer = 0f;
            Invoke("ResetIdle", 0.2f);
            UpdateAmmoText(); // Update UI text after shooting
        }
        else if (currentAmmo == 0)
        {
            Reload();
        }
    }

    void ResetIdle()
    {
        anim.SetTrigger("Idle");
    }

    void Scoped()
    {
        aiming = !aiming;
        if (aiming)
        {
            playerController.mouseSenstitivity = 15;
            anim.SetBool("Aim", true);
        }
        else
        {
            anim.SetBool("Aim", false);
            playerController.mouseSenstitivity = 30;
        }
    }

    void Reload()
    {
        if (!isReloading && currentAmmo < magazineSize)
        {
            int bulletsToReload = Mathf.Min(maxAmmo - currentAmmo, magazineSize - currentAmmo);
            maxAmmo -= bulletsToReload;
            currentAmmo += bulletsToReload;
            isReloading = true;
            anim.Play("Recharge");
            audioSource.PlayOneShot(reLoadClip);
            Invoke("FinishReload", 2f); // Assuming reload animation takes 2 seconds
        }
    }

    void FinishReload()
    {
        isReloading = false;
        UpdateAmmoText(); // Update UI text after reloading
    }

    void UpdateAmmoText()
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}