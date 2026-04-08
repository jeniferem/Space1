using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour
{
    [SerializeField]
    private Handheld playerHealth;
    [SerializeField]
    private int numberOfSpaceships = 5;
    [SerializeField]
    private InstantiatePoolObjects spaceshipPool;
    [SerializeField]
    private InstantiatePoolObjects bulletPool;
    [SerializeField]
    private float timeToSpawn =15f;
    [SerializeField]
    private UnityEvent<Transform> onShipDestoyed;
    public void OnDestroyShip(Transform transform)
    {
        onShipDestoyed.Invoke(transform);
    }
    private void Start()
    {
        StartCoroutine(SpawnSpaceships());
    }
    private IEnumerator SpawnSpaceships()
    {
        numberOfSpaceships--;
        yield return new WaitForSeconds(timeToSpawn);
        spaceshipPool.InstantiateObject(transform);
        EnemySpaceship spaceship = spaceshipPool.GetCurrentObject().GetComponent<EnemySpaceship>();
        spaceship.TargetHealth = playerHealth;
        spaceship.BulletPool = bulletPool;
        spaceship.OnDestroyed.AddListener(OnDestroyShip);
        if (numberOfSpaceships >0)
        {
            StartCoroutine(SpawnSpaceships());
        }
    }

}
