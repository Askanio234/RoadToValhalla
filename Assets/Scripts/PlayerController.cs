using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Sprite damagedSprite;
    public ParticleSystem damageSparks;
    public ParticleSystem leftEngine, rightEngine;
    public ParticleSystem shieldEffect;
    public ParticleSystem thrusterRightUp, thrusrerRightDown,
                          thrusterLeftUp, thrusterLeftDown;
    public float rotationSpeed = 100f;
    public float enginePower = 1f;
    public float thrusterPower = 10f;
    public Text speedText;

    private LevelController levelController;
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

        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
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
        if (Input.GetButton("Dodge Left"))
        {
            rb.AddForce(-transform.right * thrusterPower);
            SwitchRightThrusters(true);
        } else
        {
            SwitchRightThrusters(false);
        }
        if (Input.GetButton("Dodge Right"))
        {
            rb.AddForce(transform.right * thrusterPower);
            SwitchLeftThrusters(true);
        } else
        {
            SwitchLeftThrusters(false);
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
        if (health.currentHealth <= 0)
        {
            //play destroy animation, sound
            levelController.Lose();
        }
        //Handle sprite change
        if (health.currentHealth/health.maxHealth <= 0.5)
        {
            spriteRenderer.sprite = damagedSprite;
            damageSparks.Play();
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

    private void SwitchThrusters(ParticleSystem thruster_up, ParticleSystem thruster_down, bool switchOn)
    {
        ParticleSystem[] thrusters = { thruster_up, thruster_down };
        foreach (ParticleSystem thruster in thrusters) {
            if (switchOn)
            {
                thruster.Play();
            } else
            {
                thruster.Stop();
            }
        }
    }

    private void SwitchLeftThrusters(bool switchOn)
    {
        SwitchThrusters(thrusterLeftUp, thrusterLeftDown, switchOn);
    }

    private void SwitchRightThrusters(bool switchOn)
    {
        SwitchThrusters(thrusterRightUp, thrusrerRightDown, switchOn);
    }
}
