using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float tiempoSuavizado, velocidadRotacion, escalaGravedad, radioDeteccion, alturaSalto;
    [SerializeField] Transform pies;
    [SerializeField] LayerMask queEsSuelo;
    CharacterController cC;
    Vector2 input = Vector2.zero;
    float vida = 100f;
    float velocidadMov = 0, velocidadAndar = 10f, velocidadCorrer = 15f;
    private Vector3 movimientoVertical;

    
    void Start()
    {
        cC = GetComponent<CharacterController>();
        velocidadMov = velocidadAndar;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector2(h, v).normalized;

        if (input.magnitude != 0)
        {
            Movimiento();
        }

        

        DetectarSuelo();
        AplicarGravedad();
    }

    void Movimiento()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            velocidadMov = velocidadCorrer;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        { 
            velocidadMov = velocidadAndar;
        }

        float angulorotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, angulorotacion, ref velocidadRotacion, tiempoSuavizado);
        transform.eulerAngles = new Vector3(0, anguloSuave, 0);
        Vector3 movimiento = Quaternion.Euler(0, angulorotacion, 0) * Vector3.forward;
        cC.Move(movimiento * velocidadMov * Time.deltaTime);
    }

    private void AplicarGravedad()
    {
        movimientoVertical.y += escalaGravedad * Time.deltaTime;
        cC.Move(movimientoVertical * Time.deltaTime);
    }

    private void DetectarSuelo()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(pies.transform.position, radioDeteccion, queEsSuelo);
        if (collsDetectados.Length > 0)
        {
            movimientoVertical.y = 0;
            Salto();
        }
    }

    private void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * escalaGravedad * alturaSalto);
        }
    }

    

    public void RecibirDano(float danho)
    {
        vida -= danho;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pies.transform.position, radioDeteccion);
    }
}
