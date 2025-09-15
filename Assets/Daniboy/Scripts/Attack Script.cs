using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public LayerMask layerMask;   // Layers que o BoxCast pode colidir
    public float distance = 2f;   // Distância do cast
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

        // Direção do cast (para frente do objeto)
        Vector3 direction = transform.forward;

        // Executa o BoxCast com posição, escala e rotação do objeto
        if (Physics.BoxCast(transform.position, halfExtents, direction, out hit, transform.rotation, distance, layerMask))
        {
            Debug.Log("Colidiu com: " + hit.collider.name);
        }
        else
        {
            hit = new RaycastHit(); // limpa quando não colide
        }
    }

    private void OnDrawGizmos()
    {
        // Define a cor
        Gizmos.color = gizmoColor;

        // Caixa inicial (imitando posição/rotação/escala do objeto)
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Caixa final (após a distância do cast)
        Vector3 end = transform.position + transform.forward * distance;
        Gizmos.matrix = Matrix4x4.TRS(end, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        // Linha de ligação entre início e fim
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










