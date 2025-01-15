using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Jugador : MonoBehaviour
{
    [SerializeField] float tiempoSuavizado, velocidadRotacion, escalaGravedad, radioDeteccion, alturaSalto;
    [SerializeField] Transform pies;
    [SerializeField] LayerMask queEsSuelo;
    [SerializeField] ArmasController armasController;
    [SerializeField] CanvasManager canvasManager;
    [SerializeField] Slider slider;
    CharacterController cC;
    Vector2 input = Vector2.zero;
    float vida = 10f;
    float coolDownRegenVida = 3f;
    float velocidadMov = 0, velocidadAndar = 4f, velocidadCorrer = 8f;
    private Vector3 movimientoVertical;

    public float Vida { get => vida; }

    void Start()
    {
        cC = GetComponent<CharacterController>();
        velocidadMov = velocidadAndar;

        slider.value = 0f;
        slider.onValueChanged.AddListener(OnSliderValueChanged);
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

        coolDownRegenVida-= Time.deltaTime;
        if (vida < 10 && coolDownRegenVida < 0)
        {
            vida += 1;
            coolDownRegenVida = 2;
            armasController.ActualizarHUD();
        }
        else if (vida > 10)
        {
            vida = 10f;
        }

        if (vida <= 0) // MUERTE
        {
            vida = 0;
            canvasManager.MostrarPanelMuerte();
            Time.timeScale = 0;
        }
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
            //movimientoVertical.y = 0;
            Salto();
        }
    }

    private void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * escalaGravedad * alturaSalto);
            //movimientoVertical.y =  escalaGravedad * alturaSalto;
        }
    }

    public void RecibirDano(float danho)
    {
        vida -= danho;
        coolDownRegenVida = 10;
        armasController.ActualizarHUD();
    }

    private void OnSliderValueChanged(float value)
    {
        velocidadRotacion = value;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pies.transform.position, radioDeteccion);
    }
}
