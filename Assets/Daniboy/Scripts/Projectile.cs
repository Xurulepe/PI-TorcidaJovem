using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f; // Tempo antes de ser destru�do

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
