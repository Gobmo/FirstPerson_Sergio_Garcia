using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour
{
    /*
    [SerializeField] float velocidadMov, tiempoSuavizado, velocidadRotacion, escalaGravedad, radioDeteccion, alturaSalto;
    [SerializeField] private Transform pies;
    [SerializeField] LayerMask queEsSuelo;
    CharacterController cC;
    Vector2 input = Vector2.zero;
    Animator animator;
    private Vector3 movimientoVertical;
    // Start is called before the first frame update 
    void Start()
    {
        cC = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector2(h, v).normalized;

        if (input.magnitude != 0)
        {
            Movimiento();
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        DetectarSuelo();
        AplicarGravedad();
    }

    void Movimiento()
    {
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
        if (collsDetectados.Length > 0 )
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pies.transform.position, radioDeteccion);
    }
    */
}
