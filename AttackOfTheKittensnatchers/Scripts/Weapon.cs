using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    [Header("Shot Point")]
    public Transform shotPoint;

    [Header("Bullets")]
    public GameObject projectile;
    public GameObject projectileUpMed;
    public GameObject projectileDownMed;
    public GameObject projectileUpSmall;
    public GameObject projectileDownSmall;
    public GameObject projectileLarge;

    [Header("Boomerang")]
    public GameObject boomerang;
    public GameObject largeBoomerang;
    public bool boomerangObtained = true;

    [Header("Melee")]
    public GameObject meleeProjectile;
    public GameObject shockwaveProjectile;

    [Header("Voltic Post")]
    public GameObject voltProjectile;
    public bool volticPostObtained = false;

    [Header("Gun Power Up")]
    [HideInInspector] public int gunPoweredUp = 0;
    [HideInInspector] public int boomerangPoweredUp = 0;
    [HideInInspector] public int meleePoweredUp = 0;
    [HideInInspector] public int volticPoweredUp = 0;
    private int currentGun = 0;

    [Header("Shot Cooldown")]
    public float startTimeBetweenShotsGun;
    public float startTimeBetweenShotsBoomerang;
    public float startTimeBetweenMelee;
    private float timeBetweenShots;

    [Header("Melee stats")]
    public Transform attackPos;
    public LayerMask whatIsEnemy;
    public float meleeAttackRange;
    public float meleeAttackRangeLarge;
    public int meleeDamage;
    [HideInInspector] public bool meleeObtained = false;

    [Header("Audio")]
    public AudioSource gunSoundSource;
    public AudioClip gunSoundNormal;
    public AudioClip gunSoundPowered;
    public AudioClip meleeNormal;

    [Header("UI")]
    public Slider ammoBar;
    public Image WeaponIcon;
    public Sprite gunSprite;
    public Sprite gunPoweredSprite;
    public Sprite boomerangSprite;
    public Sprite boomerangPoweredSprite;
    public Sprite yoYoSprite;
    public Sprite yoYoPoweredSprite;
    public Sprite volticPostSprite;
    public Sprite volticPostPoweredSprite;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (timeBetweenShots <= 0) {
            if (currentGun == 0)
            { //normal gun

                if (gunPoweredUp == 0)
                {
                    WeaponIcon.sprite = gunSprite;
                }

                if (gunPoweredUp > 0)
                {
                    WeaponIcon.sprite = gunPoweredSprite;
                }

                if (Input.GetButtonDown("Fire1") && gunPoweredUp == 0) //If C is pressed and the Player's Weapon is NOT Powered Up, fire 1 standard projectile.
                {
                    gunSoundSource.clip = gunSoundNormal;
                    gunSoundSource.Play();
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsGun;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownGun());

                    //Debug.Log("Level 0");
                }
                if (Input.GetButtonDown("Fire1") && gunPoweredUp == 1) //If C is pressed and the Player's Weapon is Powered Up, fire 3 projectiles.
                {
                    gunSoundSource.clip = gunSoundPowered;
                    gunSoundSource.Play();
                    Instantiate(projectileLarge, shotPoint.position, transform.rotation);
                    Instantiate(projectileUpMed, shotPoint.position, transform.rotation);
                    Instantiate(projectileDownMed, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsGun;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownGun());
                    //Debug.Log("Level 1");
                }
                if (Input.GetButtonDown("Fire1") && gunPoweredUp == 2) //If C is pressed and the Player's Weapon is Powered Up twice, fire 5 projectiles.
                {
                    Instantiate(projectileLarge, shotPoint.position, transform.rotation);
                    Instantiate(projectileUpMed, shotPoint.position, transform.rotation);
                    Instantiate(projectileDownMed, shotPoint.position, transform.rotation);
                    Instantiate(projectileUpSmall, shotPoint.position, transform.rotation);
                    Instantiate(projectileDownSmall, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsGun;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownGun());
                    //Debug.Log("Level 2");
                }

                if (Input.GetButtonDown("Switch"))
                {
                    if (boomerangObtained == true)
                    {
                        currentGun += 1;
                    }
                    else
                    {
                        currentGun = 0;
                    }
                }
            }
            else if (currentGun == 1)
            { //switch to boomerang

                if (boomerangPoweredUp == 0)
                {
                    WeaponIcon.sprite = boomerangSprite;
                }

                if (boomerangPoweredUp > 0)
                {
                    WeaponIcon.sprite = boomerangPoweredSprite;
                }



                if (Input.GetButtonDown("Fire1") && boomerangPoweredUp == 0)
                {
                    Instantiate(boomerang, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsBoomerang;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownBoomerang());
                }
                if (Input.GetButtonDown("Fire1") && boomerangPoweredUp == 1)
                {
                    Instantiate(largeBoomerang, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangDiagUp, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangDiagDown, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsBoomerang;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownBoomerang());
                }
                if (Input.GetButtonDown("Fire1") && boomerangPoweredUp == 2)
                {
                    Instantiate(largeBoomerang, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangUp, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangDown, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangDiagUp, shotPoint.position, transform.rotation);
                    //Instantiate(boomerangDiagDown, shotPoint.position, transform.rotation);
                    timeBetweenShots = startTimeBetweenShotsBoomerang;
                    ammoBar.value = 0;
                    StartCoroutine(CooldownBoomerang());
                }

                if (Input.GetButtonDown("Switch"))
                {
                    if (meleeObtained == true)
                    {
                        currentGun += 1;
                    }
                    else
                    {
                        currentGun = 0;
                    }
                }
            }
            else if (currentGun == 2)
            { //switch to melee

                if (meleePoweredUp == 0)
                {
                    WeaponIcon.sprite = yoYoSprite;
                }

                if (meleePoweredUp > 0)
                {
                    WeaponIcon.sprite = yoYoSprite;
                }

                //put attack animation here
                if (Input.GetButtonDown("Fire1") && meleePoweredUp == 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Instantiate(meleeProjectile, shotPoint.position, transform.rotation);

                        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, meleeAttackRange, whatIsEnemy);
                        for (int i = 0; i < enemiesToDamage.Length; i++)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(meleeDamage);
                        }
                        //Debug.Log("Melee Attack");
                        timeBetweenShots = startTimeBetweenMelee;
                    }
                }

                if (Input.GetButtonDown("Switch"))
                {
                    currentGun = 0;
                }

                if (Input.GetButtonDown("Fire1") && meleePoweredUp == 1)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Instantiate(meleeProjectile, shotPoint.position, transform.rotation);

                        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, (meleeAttackRangeLarge), whatIsEnemy);
                        for (int i = 0; i < enemiesToDamage.Length; i++)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(meleeDamage);
                        }
                        //Debug.Log("Melee Attack");
                        timeBetweenShots = startTimeBetweenMelee;
                    }
                }
                if (Input.GetButtonDown("Fire1") && meleePoweredUp == 2)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Instantiate(meleeProjectile, shotPoint.position, transform.rotation);
                        Instantiate(shockwaveProjectile, shotPoint.position, transform.rotation);

                        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, (meleeAttackRangeLarge), whatIsEnemy);
                        for (int i = 0; i < enemiesToDamage.Length; i++)
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(meleeDamage);
                        }
                        //Debug.Log("Melee Attack");
                        timeBetweenShots = startTimeBetweenMelee;
                    }
                }
            }
            else if (currentGun == 3)
            { //switch to Voltic Post

                if (volticPoweredUp == 0)
                {
                    WeaponIcon.sprite = volticPostSprite;
                }

                if (volticPoweredUp > 0)
                {
                    WeaponIcon.sprite = volticPostPoweredSprite;
                }

                //put attack animation here
                if (Input.GetButtonDown("Fire1") && volticPoweredUp == 0)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        Instantiate(voltProjectile, shotPoint.position, transform.rotation);
                    }
                }


            }
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

    }

    public void PowerUpCurrentGun() {
        if (currentGun == 0 && gunPoweredUp < 2) {
            gunPoweredUp += 1;
            Debug.Log("Gun Powered Up" + gunPoweredUp);
        }
        else if (currentGun == 1 && boomerangPoweredUp < 2) {
            boomerangPoweredUp += 1;
            Debug.Log("Boomerang Powered Up" + boomerangPoweredUp);
        }
        else if (currentGun == 2 && meleePoweredUp < 2) {
            meleePoweredUp += 1;
            Debug.Log("Melee Powered Up" + meleePoweredUp);
        }
        else {
            Debug.Log("nothing to power up");
        }
    }

    public void WeakenCurrentGun() {
        if (currentGun == 0 && gunPoweredUp > 0) {
            gunPoweredUp -= 1;
            Debug.Log("Gun Powered Up" + gunPoweredUp);
        }
        else if (currentGun == 1 && boomerangPoweredUp > 0) {
            boomerangPoweredUp -= 1;
            Debug.Log("Boomerang Powered Up" + boomerangPoweredUp);
        }
        else if (currentGun == 2 && meleePoweredUp > 0) {
            meleePoweredUp -= 1;
            Debug.Log("Melee Powered Up" + meleePoweredUp);
        }
        else {
            Debug.Log("nothing to weaken");
        }
    }

    //enable use of boomerang once collected
    public void ObtainBoomerang() {
        boomerangObtained = true;
    }

    public void ObtainMelee() {
        meleeObtained = true;
    }

    //public void SetAmmo(float ammo)
    //{
    //    ammoBar.value = timeBetweenShots;
    //}

    IEnumerator CooldownGun()
    {

        while (timeBetweenShots > 0)
        {
            ammoBar.value = 1 - Mathf.Lerp(0, 1, timeBetweenShots);

            yield return null;
        }
        ammoBar.value = 1;
    }

    IEnumerator CooldownBoomerang()
    {

        while (timeBetweenShots > 0)
        {
            ammoBar.value = 1 - Mathf.Lerp(0, startTimeBetweenShotsBoomerang, timeBetweenShots);

            yield return null;
        }
        ammoBar.value = 1;
    }

    IEnumerator CooldownMelee()
    {
        while (timeBetweenShots > 0)
        {
            ammoBar.value = 1 - Mathf.Lerp(0, 1, timeBetweenShots);

            yield return null;
        }
        ammoBar.value = 1;
    }

    IEnumerator CooldownVolticPost()
    {
        while (timeBetweenShots > 0)
        {
            ammoBar.value = 1 - Mathf.Lerp(0, 1, timeBetweenShots);

            yield return null;
        }
        ammoBar.value = 1;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, meleeAttackRange);
    }
}
