using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public LayerMask layerMask;   // Layers que o BoxCast pode colidir
    public float distance = 2f;   // Dist�ncia do cast
    public Color gizmoColor = Color.green;

    private RaycastHit hit;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Metade da escala do GameObject
        Vector3 halfExtents = transform.localScale * 0.5f;

        // Dire��o do cast (para frente do objeto)
        Vector3 direction = transform.forward;

        // Executa o BoxCast com posi��o, escala e rota��o do objeto
        if (Physics.BoxCast(transform.position, halfExtents, direction, out hit, transform.rotation, distance, layerMask))
        {
            Debug.Log("Colidiu com: " + hit.collider.name);
        }
        else
        {
            hit = new RaycastHit(); // limpa quando n�o colide
        }
    }

    private void OnDrawGizmos()
    {
        // Define a cor
        Gizmos.color = gizmoColor;

        // Caixa inicial (imitando posi��o/rota��o/escala do objeto)
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Caixa final (ap�s a dist�ncia do cast)
        Vector3 end = transform.position + transform.forward * distance;
        Gizmos.matrix = Matrix4x4.TRS(end, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Linha de liga��o entre in�cio e fim
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.DrawLine(transform.position, end);

        // Se colidiu, desenha o ponto de impacto
        if (hit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
    }
}










