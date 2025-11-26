using UnityEngine;

public class movimientoPlataforma : MonoBehaviour
{
    public GameObject objeto;
    public Transform puntoInicio, puntoFin;
    public float velocidad;
    private Vector3 movimiento;

    void Start()
    {
        // fijar el punto de inicio del movimiento
        movimiento = puntoInicio.position;
    }

    void Update()
    {
        // inicializar movimiento
        objeto.transform.position = Vector3.MoveTowards(objeto.transform.position, movimiento, velocidad * Time.deltaTime);
        // vuelta hacia el punto inicial y viceverse
        if (objeto.transform.position == puntoFin.position)
        {
            movimiento = puntoInicio.position;
        }
        if (objeto.transform.position == puntoInicio.position)
        {
            movimiento = puntoFin.position;
        }
    }
}
