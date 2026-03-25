using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] GameObject player;
    private GameObject fireball;
    private float fireballCooldown;
    private float fireballCooldownTimer;
    
    public float speed = 3f;
    public float obstacleRange = 5f;

    private float trackingDistance;

    private bool isAlive;


    public const float _baseSpeed = 3f;

    private void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        speed = _baseSpeed * value;
    }


    private float distanceToPlayer;

    PlayerCharacter pc;

    private void Start()
    {
        isAlive = true;
        trackingDistance = 20f;
        distanceToPlayer = 0;
        fireballCooldown = 2f;
        fireballCooldownTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);   
        }

        Vector3 playerpos = new (player.transform.position.x, this.transform.position.y, player.transform.position.z);

        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        
        if (fireballCooldownTimer > 0) fireballCooldownTimer -= Time.deltaTime;
        
        /// MODIFICATION!!!!
        /// instead of only shooting a projectile forward if the player is directly in front
        /// of the enemy, it now shoots one towards the player if they are within a set
        /// distance and are not behind a wall
        if (distanceToPlayer <= trackingDistance && fireballCooldownTimer <= 0 && isAlive)
        {
            if (CanSeePlayer())
            {
                
                Vector3 currentForward = new (this.transform.position.x, this.transform.position.y, this.transform.position.z);;
            
                transform.LookAt(playerpos);

                fireball = Instantiate(fireballPrefab);
                fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                fireball.transform.rotation = transform.rotation;
                fireballCooldownTimer = fireballCooldown;

                transform.LookAt(currentForward);

            }
        }

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit, obstacleRange))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<PlayerCharacter>() != null)
            {
                
            }
            else
            {
                transform.Rotate(0, Random.Range(-110, 110), 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    /// MODIFICATION!!!!
    /// A new script was needed for the enemy to mimic line of sight
    private bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;

        Ray ray = new Ray(transform.position, direction.normalized);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.GetComponent<PlayerCharacter>() != null)
            {
                return true; 
            }
        }

        return false; 
    }
}
