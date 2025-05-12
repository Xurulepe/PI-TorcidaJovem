using UnityEngine;

public class Virus : MonoBehaviour
{
    public GameObject _spriteVirus;
    public Transform cameraTrans;
    public Transform shadowPosition;

    public Transform player;  // Referência ao jogador
    public float speed = 3f;  // Velocidade do inimigo
    public float stopDistance = 1.5f; // Distância mínima antes de parar

    void Update()
    {
        

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stopDistance)
            {
                // Move o inimigo em direção ao jogador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Rotaciona o inimigo para olhar para o jogador
                transform.LookAt(player);
            }
        }
    }

    void LateUpdate()
    {
        _spriteVirus.transform.LookAt(_spriteVirus.transform.localPosition + cameraTrans.position);
        shadowPosition.transform.LookAt(shadowPosition.transform.localPosition + cameraTrans.position);
    }
}
