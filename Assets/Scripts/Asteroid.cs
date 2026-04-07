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
        if (target != null)
        {
            Vector3 direction = (target.position = transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
        public void DestroyAsteroid()
    {
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
    private void Osable()
    {
        target = null;  
        onAsteroidDestroyed.RemoveAllListeners();
    }
}
