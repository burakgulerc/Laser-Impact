
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5;
    [SerializeField] float baseFringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float enemyProjectileFireVariance = 0.2f;
    [SerializeField] float minimumEnemyFireTime = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    
    AudioPlayer audioPlayer;
    // with singleton no longer neccesary
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring &&  firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;             // need to assign null in order to start fire coroutine again
        }
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            // create lasers variable to be able to destroy later 
            GameObject laser = Instantiate(projectilePrefab,transform.position,Quaternion.identity);

            // Add Velocity to the projectiles, grab RBody reference first

            Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(laser, projectileLifeTime);

            // audioPlayer.PlayShootingClip(); with Singleton no longer neccesary
            audioPlayer.GetInstance().PlayShootingClip();

            yield return new WaitForSeconds(RandomEnemyFireTime());
        }
        
    }

    float RandomEnemyFireTime()
    {
        float enemyShootTime = Random.Range(baseFringRate - enemyProjectileFireVariance, baseFringRate + enemyProjectileFireVariance);

        return Mathf.Clamp(enemyShootTime,minimumEnemyFireTime,float.MaxValue);
    }
}
