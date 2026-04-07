using UnityEngine;

public class Gun : MonoBehaviour
{
    private Camera cameraUsed;
    public Camera Camera
    {
        set{cameraUsed = value;}
    }
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform bulletPivot;
    [SerializeField]
    private InstantiatePoolObjects bulletpool;
    private float rayDistance = 1000f;
    public void Shoot()
    {
        Ray ray = cameraUsed.ViewportPointToRay(new Vector3(0.5f, 0.5f,0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit, rayDistance))
    {
        targetPoint = hit.point;
    }
    else
    {
        targetPoint = ray.origin + ray.direction * rayDistance;
    }
    Vector3 direction = (targetPoint - transform.position).normalized;
    bulletPivot.forward = direction;
    bulletpool.InstantiateObject(bulletPivot);
    Transform bullet = bulletpool.GetCurrentObject().transform;
    bullet.transform.LookAt(targetPoint);
    animator.Play("Shoot",0,0f);
    SoundManager.instance.Play("PUM");
    }
}
