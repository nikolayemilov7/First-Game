using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private GameObject _firePrefab;
    [SerializeField]
    private HealthBar _healthBar;
    [SerializeField]
    private Spell spell;
    [SerializeField]
    public Quest quest; // IF MORE QUESTS MAKE IT LIST

    private int _maxHealth = 100;
    private int _currentHealth;

    private bool _isEnemy = false;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent is NULL!");
        }

        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL!");
        }

        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
        }

    void Update()
    {
        if (!PointerOverUI())
        {
            PlayerBehaviour();
            MovementAnim();
            spell.ActivateSpell();
        }
        DoQuest();
    }

    private void DoQuest()
    {
        if (quest.isActive)
        {
            //quest.goal.EnemyKill();
            //quest.goal.EnemyCollected();
            if (quest.goal.isReached())
            {
                quest.Complete();
            }
        }
    }

    private void PlayerBehaviour()
    {
        if (Input.GetMouseButtonDown(0) & !spell.spell)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(ray, out hitPoint))
            {
                if (hitPoint.transform.CompareTag("Map"))
                {
                    _navMeshAgent.SetDestination(hitPoint.point);
                }
                else if (hitPoint.transform.tag == "Enemy")
                {
                    _navMeshAgent.SetDestination(hitPoint.point);
                    if (_isEnemy)
                    {
                        _navMeshAgent.destination = transform.position;
                        GameObject.Find("Enemy").GetComponent<Enemy>().Damage(20);
                        _anim.Play("Punching", 0, 0.25f);
                    }
                }
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _anim.SetBool("isMoving", false);
            _isEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _isEnemy = false;
            _anim.Rebind();
        }
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
        }

    }
    public bool PointerOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}