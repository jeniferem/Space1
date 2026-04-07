using UnityEngine;
using UnityEngine.Events;

public class AsteroidManager : MonoBehaviour
{
   [SerializeField]
   private InstantiatePoolObjects asteroidPool;
   [SerializeField]
   private Transform target;
   [SerializeField]
   private float spawnInsterval = 2f;
   [SerializeField]
   private UnityEvent<Transform> onAsteroidDestroyed;
   [SerializeField]
   private int numberOfAsteroids = 10;
    private void Start()
    {
        float initialDelay = 0f;
        for (int i = 0; 1 < numberOfAsteroids; i++)
        {
            Invoke("SpawnAsteroid", initialDelay);
            initialDelay += spawnInsterval;
        }
       
    }
    private void SpawnAsteroid()
    {
        Vector3 randomDistanceFromTarget = Random.onUnitSphere * 200f;
        randomDistanceFromTarget.y = Mathf.Abs(randomDistanceFromTarget.y) + 5f;
        Vector3 spawnPosition = target.position + randomDistanceFromTarget;
        asteroidPool.InstantiateObject(spawnPosition);
        Asteroid asteroid = asteroidPool.GetCurrentObject().GetComponent<Asteroid>();
        asteroid.SetTarget(target);
        asteroid.OnAsteroidDestroyed.AddListener(OnAsteroidDestroyed);
    }
    private void OnAsteroidDestroyed(Transform asteroid)
    {
      onAsteroidDestroyed?.Invoke(asteroid);  
    }
}

