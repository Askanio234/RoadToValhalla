using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Sprite damagedSprite;
    public ParticleSystem leftEngine, rightEngine;
    public ParticleSystem shieldEffect;
    public float rotationSpeed = 100f;
    public float enginePower = 1f;
    public Text speedText;


    private Health health;
    private Shield shield;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float lastHitTime;
    private float oldEmissionRate;
    private float newEmissionRate;
    private float oldStartSpeed;
    private float newStartSpeed;
   
	// Use this for initialization
	void Start () {
        

        oldEmissionRate = leftEngine.emission.rateOverTimeMultiplier;
        oldStartSpeed = 0.2f;
        newEmissionRate = 60f;
        newStartSpeed = 0.4f;

        health = gameObject.GetComponent<Health>();
        shield = gameObject.GetComponent<Shield>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();


    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(new Vector3(0, 0, -h * rotationSpeed * Time.deltaTime));


        if (v > 0)
        {
            rb.AddForce(transform.up * enginePower);
            EngageAfterBurners();
        } else {
            DisengageAfterBurners();
            //rb.AddForce(-transform.up * enginePower);
        }
        speedText.text = "Velocity: " + rb.velocity.ToString("#.0");
    }
    // Update is called once per frame
    void Update ()
    {
        
        HandleShieldRegen();
    }

    private void DisengageAfterBurners()
    {
        var mainRight = rightEngine.main;
        var mainLeft = leftEngine.main;
        mainRight.startSpeed = oldStartSpeed;
        mainLeft.startSpeed = oldStartSpeed;
        var emissionRight = rightEngine.emission;
        var emissionLeft = leftEngine.emission;
        emissionRight.rateOverTimeMultiplier = oldEmissionRate;
        emissionLeft.rateOverTimeMultiplier = oldEmissionRate;
    }

    private void EngageAfterBurners()
    {
        var mainRight = rightEngine.main;
        var mainLeft = leftEngine.main;
        mainRight.startSpeed = newStartSpeed;
        mainLeft.startSpeed = newStartSpeed;
        var emmisionRight = rightEngine.emission;
        var emissionLeft = leftEngine.emission;
        emmisionRight.rateOverTimeMultiplier = newEmissionRate;
        emissionLeft.rateOverTimeMultiplier = newEmissionRate;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        Projectile projectile = col.gameObject.GetComponent<Projectile>();
        EnemyController enemy = col.gameObject.GetComponent<EnemyController>();
        if (projectile)
        {
            float damage = projectile.GetDamage();
            float absorbedDamage = shield.AbsorbDamage(damage);
            hadleShieldEffect(absorbedDamage, damage);
            health.GetDamage(absorbedDamage);
            projectile.Hit();
            lastHitTime = Time.time;
        }
        //Collision damage is not absorbed by shield
        else if(enemy){
            health.GetDamage(enemy.GetCollisionDamage());
            Destroy(enemy.gameObject);
        }
        //Handle sprite change
        if (health.currentHealth/health.maxHealth <= 0.5)
        {
            spriteRenderer.sprite = damagedSprite;
        }
    }

    void HandleShieldRegen()
    {
        if (Time.time >= lastHitTime + shield.regenDelay)
        {
            shield.RegenShield();
        }
    }
    //Shield effect will pop up if any damage was absorbed
    void hadleShieldEffect(float absorbedDamage, float damage)
    {
        if (absorbedDamage != damage)
        {
            shieldEffect.Play();
        }
    }
}
