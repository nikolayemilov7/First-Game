using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private HealthBar _healthBar;

    private int _maxHealth = 100;
    private int _currentHealth;

    private bool _isTrigger = false;
    private bool _attackZone = false;




    void Start()
    {
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is NULL!");
        }
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }
    void Update()
    {
        AttackZoneActive();
        ChasePlayer();
        MovementAnim();

        if (_currentHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }


    private void MovementAnim()
    {
        if (_navMeshAgent.remainingDistance > 0.5f)
        {
            _anim.SetBool("isMoving", true);
        }

        else
        {
            _anim.SetBool("isMoving", false);
        }
    }

    private void ChasePlayer()
    {
        if (_attackZone == true)
        {
            transform.LookAt(Player);
            _navMeshAgent.destination = Player.transform.position;
        }
    }

    private void AttackZoneActive()
    {
        if (_isTrigger == false && Player.transform.position.z < 40)
        {
            _attackZone = true;
        }
        else
        {
            _attackZone = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _isTrigger = true;
        if (other.CompareTag("Player"))
        {
            _anim.SetBool("isMoving", false);
            Player player = GameObject.Find("Player").GetComponent<Player>();
            if (player != null)
            {
                _navMeshAgent.destination = transform.position;
                GameObject.Find("Player").GetComponent<Player>().Damage(20);
                _anim.Play("Punching", 0, 0.25f);
            }
        }
    }


    


    private void OnTriggerExit(Collider other)
    {
        _isTrigger = false;
        _anim.Rebind();
    }
    public void Damage(int damage)
    {
        _healthBar.SetHealth(_currentHealth);
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            _healthBar.SetHealth(_currentHealth);
        }
        else
        {
            Destroy(this.gameObject);
            _currentHealth = 0;
        }
    }
}