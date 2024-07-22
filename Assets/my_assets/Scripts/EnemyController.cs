using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	public float lookRadius = 10f;
	public float waitTime = 1f;   //time to wait till next attack

	private float currentTime = 0f;   //current time for waitTime
	private bool shot = false;

	public GameObject laser;
	public GameObject laserSpawnPoint;

	private Transform target;
	private NavMeshAgent agent;
	private Transform laserSpawned;
	private Target targetScript;

	//for animator:
	private bool forward = false;
	private bool attack = false;
	private bool death = false;

	public AudioClip shootAudio;

	private AudioSource audioSource;

	private Animator enemyAnimator;
	public ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		enemyAnimator = GetComponent<Animator>();
		targetScript = GetComponent<Target>();
		audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {	
		float distance = Vector3.Distance(target.position, transform.position);
		if (distance <= lookRadius)
		{
			forward = true;
			if (!attack && forward && !death)
				agent.SetDestination(target.position);

			if (distance <= agent.stoppingDistance && !death)
			{
				forward = false;
				FaceTarget();

				//attack the target
				if (currentTime == 0f)
				{
					Shoot();
				}
				if (shot && currentTime < waitTime)
					currentTime += 1 * Time.deltaTime;
				if (currentTime >= waitTime)
				{
					currentTime = 0f;
					shot = false;
				}
			}
			else
			{
				attack = false;
				forward = true;
			}
		}
		else
			forward = false;
		if (targetScript.health <= 0)
			death = true;
		UpdateAnimator();
    }

	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}

	void Shoot()
	{
		audioSource.PlayOneShot(shootAudio, 1f);
		attack = true;
		shot = true;
		muzzleFlash.Play();
		Debug.Log("Enemy Shooting");
		laserSpawned = Instantiate(laser.transform, laserSpawnPoint.transform.position, Quaternion.identity);
		laserSpawned.rotation = transform.rotation;
	}

	void UpdateAnimator()
	{
		enemyAnimator.SetBool("Forward",forward);
		enemyAnimator.SetBool("Attack",attack);
		enemyAnimator.SetBool("Death",death);
	}
}
