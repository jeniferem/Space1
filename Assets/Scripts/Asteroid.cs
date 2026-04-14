using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Asteroid : MonoBehaviour
{
    private Animator animator;
    private Transform target;
    private Health health;
    [SerializeField]
    private float bulletDamaga= 25f;
    [SerializeField]
    private float speed =5f;
    [SerializeField]
    private float asteroidDanager = 10f;
    [SerializeField]
    private float distanceToTarget = 2f;
    [SerializeField]
    private UnityEvent<Transform> onAsteroidDestroyed;
    public UnityEvent<Transform> OnAsteroidDestroyed => onAsteroidDestroyed;
    private Collider asteroidCollider;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        asteroidCollider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        animator.Play("Idle", 0, 0f);
        asteroidCollider.enabled = true;
        health.InitializeHealth();
    }
     public void OnPointerClick()
    {
        health.TakeDamage(bulletDamaga);
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void Update()
    {
        if (target != null && gameObject.activeInHierarchy)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            transform.LookAt(target);
            if(Vector3.Distance(transform.position, target.position) <= distanceToTarget)
            {
                target.GetComponent<Health>().TakeDamage(asteroidDanager);
                DestroyAsteroid();
            }
        }
    }
        public void DestroyAsteroid()
    {
        if (!gameObject.activeInHierarchy) return;
        target = null;
        asteroidCollider.enabled = false;
        animator.Play("Explode", 0, 0f);
        onAsteroidDestroyed?.Invoke(transform);
        StartCoroutine(DestroyCoroutine());
    }
    private IEnumerator DestroyCoroutine()
    {
        yield return null;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        target = null;  
        onAsteroidDestroyed.RemoveAllListeners();
    }
}
