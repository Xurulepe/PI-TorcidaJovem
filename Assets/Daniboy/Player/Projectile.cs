using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f; // Tempo antes de ser destruído

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
