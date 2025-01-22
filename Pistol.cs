using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pistol : MonoBehaviour
{
    
    public float range = 100f;
    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public int maxAmmo = 15;
    public int currentAmmo = 15;
    public int TotalAmmo = 90;
    public int MaxTotalAmmo = 300;
    public float reloadTime = 1f;

    public bool isReloading = false;
    public GameObject fpsCam;
   

    
    public Animator PlayerAnim;
    public ThirdPersonController TheThirdPersonController;
    public float MinDamage = 5;
    public float MaxDamage = 10;
    public float MinForce = 1000;
    public float MaxForce = 4000;
    public ParticleSystem TheMuzzleFlash;
    public GameObject SparkParticle;
    public GameObject SparkParticle2;
    public GameObject GunLightGameObject;
    public Light TheGunLight;
    public ParticleSystem TheReloadSmoke1;
    public ParticleSystem TheReloadSmoke2;
    public AudioSource PistolShotSound;
    public AudioSource BarrelHitSound;
    public AudioSource EnemyHitSound;
    public AudioSource GroundHitSound;
    public AudioSource ReloadSound;
    public AudioSource EmptyBulletFallSound;

    void Start()
    {
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
      TheGunLight.intensity -= Time.deltaTime * 100;
       
        if (isReloading == true)
        {

            //GunLight.SetActive(false);
            PlayerAnim.SetBool("IsStartPistolShooting", false);
            PlayerAnim.SetBool("IsPistolShoot", false);
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
       

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
           

            if (!TheThirdPersonController.IsGunMode) 
            {
                PlayerAnim.SetBool("IsStartPistolShooting", true);
                TheThirdPersonController.GunModeTimer = 10;
                TheThirdPersonController.IsAiming = true;
            }
            else
            {
                PlayerAnim.SetBool("IsPistolShoot", true);
                PlayerAnim.SetBool("IsStartPistolShooting", false);
                TheThirdPersonController.GunModeTimer = 10;
                Shoot();
                //Debug.Log("Shoot");
            }
           
            // PlayerAnim.SetBool("IsIdle", false);
        }
        else
        {
            //CamAnim.SetBool("ShootingCam", false);
            //CrossHair.SetActive(false);
            //GunLightGameObject.SetActive(false);
            PlayerAnim.SetBool("IsStartPistolShooting", false);
            PlayerAnim.SetBool("IsPistolShoot", false);

        }

        if (TheThirdPersonController.GunModeTimer > 0)
        {
            TheThirdPersonController.IsGunMode = true;
            PlayerAnim.SetBool("IsPistolMode", true);
        }
        else 
        {
            TheThirdPersonController.IsGunMode = false;
            PlayerAnim.SetBool("IsStartPistolShooting", false);
            PlayerAnim.SetBool("IsPistolShoot", false);
            PlayerAnim.SetBool("IsPistolMode", false);
        }


        if (TotalAmmo > MaxTotalAmmo)
        {
            TotalAmmo = MaxTotalAmmo;
        }
    }



    private IEnumerator  Reload()
    {

        isReloading = true;
        Debug.Log("Reloading...");
        TheReloadSmoke1.Play();
        TheReloadSmoke2.Play();
        EmptyBulletFallSound.Play();

        yield return new WaitForSeconds(reloadTime);
        PlayerAnim.SetFloat("ReloadSpeed", 1);
        
        TheReloadSmoke2.Stop();
        ReloadSound.Play();

        yield return new WaitForSeconds(0.4f);
        TheReloadSmoke1.Stop();

        isReloading = false;
        if (TotalAmmo > 0)
        {
            currentAmmo = maxAmmo;
            TotalAmmo -= maxAmmo;
        }

        

    }




   public void Shoot()
    {

        if (currentAmmo > 0)
        {

            TheGunLight.intensity = 50;
            TheMuzzleFlash.Play();

            PistolShotSound.pitch = Random.Range(0.95f, 1.05f);
            PistolShotSound.volume = Random.Range(0.95f, 1.05f);
            PistolShotSound.Play();

            currentAmmo--;
            RaycastHit hit;
            


            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("EnemyLeg"))
                {
                   EnemyHitSound.PlayDelayed(0.1f);
                   GameObject impactGo = Instantiate(SparkParticle, hit.point, Quaternion.LookRotation(hit.normal));
                   Destroy(impactGo, 0.8f);
                   Enemy _enemy = hit.collider.GetComponentInParent<Enemy>();
                    _enemy.Health -= Random.Range(MinDamage, MaxDamage);
                    Rigidbody enemyRigidBody = hit.collider.GetComponent<Rigidbody>();

                    if (hit.collider.CompareTag("EnemyLeg"))
                    {
                        _enemy.LastShotWasLeg = true;
                    }
                    else
                    {
                        _enemy.LastShotWasLeg = false;
                    }

                    enemyRigidBody.AddForce(-hit.normal * Random.Range(MinForce, MaxForce));

                    _enemy.HitReact();
                }

                else if (hit.collider.CompareTag("Ground"))
                {
                    GroundHitSound.PlayDelayed(0.1f);
                    GameObject impactGo = Instantiate(SparkParticle, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 0.8f);
                   
                }
                else if (hit.collider.CompareTag("Barrel"))
                {
                    PistolShotSound.pitch = Random.Range(0.95f, 1.1f);
                    BarrelHitSound.PlayDelayed(0.1f);

                    GameObject impactGo = Instantiate(SparkParticle2, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGo, 0.8f);
                    Barrel barrel = hit.collider.GetComponent<Barrel>();
                    barrel.Health -= Random.Range(15, 30);

                }
            }

            StartCoroutine(GunSmokeTime());

        }


    }

    private IEnumerator GunSmokeTime()
    {

        TheReloadSmoke1.Play();
        TheReloadSmoke2.Play();
        yield return new WaitForSeconds(0.4f);
        if (!isReloading)
        {
            TheReloadSmoke1.Stop();
            TheReloadSmoke2.Stop();
        }
        

    }



}
