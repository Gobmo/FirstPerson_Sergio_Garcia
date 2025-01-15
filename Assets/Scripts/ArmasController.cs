using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;

public class ArmasController : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    private float cooldownDisparar = 0f;
    private float tiempoEntreDisparosPistola = 1f, tiempoEntreDisparosRifle = 0.2f, tiempoEntreDisparosEscopeta = 2f;
    private int municionPistola = 0, municionRifle = 0, municionEscopeta = 0;
    private int municionMaxPistola = 15, municionMaxRifle = 30, municionMaxEscopeta = 8;
    private int danhoPistola = 3, danhoRifle = 2, danhoEscopeta = 1;
    private bool recargando = false;
    private float cooldownRecarga = 0f;
    [SerializeField] LayerMask enemigoMask, armasMask;
    [SerializeField] Jugador jugador;
    [SerializeField] RoundsController roundsController;
    public int armaActiva = 0;
    GameObject[] armas = new GameObject[3];
    Animator[] animatorArmas = new Animator[3];
    [SerializeField] float distanciaRayoCogerArmas = 0f;
    [SerializeField] TextMeshProUGUI textoVida, textoMunicion, textoRondas;
    [SerializeField] ParticleSystem muzzlePistola, muzzleRifle, muzzleEscopeta;

    void Start()
    {
        armas[0] = GameObject.Find("Pistola");
        armas[1] = GameObject.Find("Rifle");
        armas[2] = GameObject.Find("Escopeta");

        armas[1].SetActive(false);
        armas[2].SetActive(false);

        animatorArmas[0] = armas[0].GetComponent<Animator>();
        animatorArmas[1] = armas[1].GetComponent<Animator>();
        animatorArmas[2] = armas[2].GetComponent<Animator>();

        municionPistola = municionMaxPistola;
        municionRifle = municionMaxRifle;
        municionEscopeta = municionMaxEscopeta;

        ActualizarHUD();
    }

    void Update()
    {
        cooldownDisparar -= Time.deltaTime;

        if (Input.GetMouseButton(0) && recargando == false)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && recargando == false)
        {
            if (armaActiva == 1 && armas[1].activeSelf == false)
            {
                CambiarARifle();
            }
            else if (armaActiva == 2 && armas[2].activeSelf == false)
            {
                CambiarAEscopeta();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && armas[0].activeSelf == false && recargando == false)
        {
            CambiarAPistola();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CogerArma();
        }

        if (Input.GetKeyDown(KeyCode.R) && recargando == false)
        {
            Recargar();
        }

        if (recargando == true)
        {
            cooldownRecarga -= Time.deltaTime;
            if (cooldownRecarga < 0)
            {
                if (armas[0].activeSelf)
                {
                    municionPistola = municionMaxPistola;
                }
                else if (armas[1].activeSelf)
                {
                    municionRifle = municionMaxRifle;
                }
                else if (armas[2].activeSelf)
                {
                    municionEscopeta = municionMaxEscopeta;
                }
                ActualizarHUD();
                recargando = false;
            }
        }
    }

    private void CambiarARifle()
    { 
        armas[0].SetActive(false); // Ocultar pistola
        armas[2].SetActive(false); // Ocultar escopeta
        armas[1].SetActive(true); // Sacar rifle
        ActualizarHUD();
    }

    private void CambiarAEscopeta()
    {
        armas[0].SetActive(false); // Ocultar pistola
        armas[1].SetActive(false); // Ocultar rifle
        armas[2].SetActive(true); // Sacar escopeta
        ActualizarHUD();
    }

    private void CambiarAPistola()
    {
        armas[1].SetActive(false); // Ocultar rifle
        armas[2].SetActive(false); // Ocultar escopeta
        armas[0].SetActive(true); // Sacar pistola
        ActualizarHUD();
    }
    
    private void Shoot()
    {
        if (armas[0].activeSelf) // Disparar pistola
        {
            if (cooldownDisparar < 0 && municionPistola > 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, 100f, enemigoMask))
                {
                    Enemigo enemigoImpactado = hit.collider.gameObject.GetComponent<Enemigo>();
                    enemigoImpactado.RecibirDano(danhoPistola);
                }
                cooldownDisparar = tiempoEntreDisparosPistola;
                municionPistola--;
                muzzlePistola.Play();
                animatorArmas[0].SetTrigger("Shoot");
            }
        }
        else if (armas[1].activeSelf) // Disparar rifle
        {
            if (cooldownDisparar < 0 && municionRifle > 0)
            {
                RaycastHit hit;
                if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, 100f, enemigoMask))
                {
                    Enemigo enemigoImpactado = hit.collider.gameObject.GetComponent<Enemigo>();
                    enemigoImpactado.RecibirDano(danhoRifle);
                }
                cooldownDisparar = tiempoEntreDisparosRifle;
                municionRifle--;
                muzzleRifle.Play();
                animatorArmas[1].SetTrigger("Shoot");
            }
        }
        else if (armas[2].activeSelf) // Disparar escopeta
        {
            if (cooldownDisparar < 0 && municionEscopeta > 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector3 direccionPerdigon = puntoDisparo.transform.forward;
                    direccionPerdigon += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
                    
                    RaycastHit hit;
                    if (Physics.Raycast(puntoDisparo.position, direccionPerdigon.normalized, out hit, 100f, enemigoMask))
                    {
                        Enemigo enemigoImpactado = hit.collider.gameObject.GetComponent<Enemigo>();
                        enemigoImpactado.RecibirDano(danhoEscopeta);
                    }
                }
                cooldownDisparar = tiempoEntreDisparosEscopeta;
                municionEscopeta--;
                muzzleEscopeta.Play();
                animatorArmas[2].SetTrigger("Shoot");
            }
        }
        ActualizarHUD();
    }

    private void Recargar()
    {
        if (armas[0].activeSelf && municionPistola < municionMaxPistola)
        {
            cooldownRecarga = 2f;
            recargando = true;
            animatorArmas[0].SetTrigger("Reload");
        }
        else if (armas[1].activeSelf && municionRifle < municionMaxRifle)
        {
            cooldownRecarga = 3f;
            recargando = true;
            animatorArmas[1].SetTrigger("Reload");
        }
        else if (armas[2].activeSelf && municionEscopeta < municionMaxEscopeta)
        {
            cooldownRecarga = 4f;
            recargando = true;
            animatorArmas[2].SetTrigger("Reload");
        }
    }

    private void CogerArma()
    {
        Ray rayCogerArma = new Ray(puntoDisparo.transform.position, puntoDisparo.transform.forward);
        RaycastHit hitCogerArma;

        Debug.DrawRay(rayCogerArma.origin, rayCogerArma.direction * distanciaRayoCogerArmas, Color.red);

        if (Physics.Raycast(rayCogerArma, out hitCogerArma, distanciaRayoCogerArmas, armasMask))
        {
            if (hitCogerArma.collider.CompareTag("Rifle"))
            {
                armaActiva = 1;
                CambiarARifle();
            }
            else if (hitCogerArma.collider.CompareTag("Escopeta"))
            {
                armaActiva = 2;
                CambiarAEscopeta();
            }
        }
    }

    public void ActualizarHUD()
    {
        textoVida.text = jugador.Vida + " / 10";
        textoRondas.text = roundsController.NumeroRonda + "";
        if (armas[0].activeSelf)
        {
            textoMunicion.text = municionPistola + " / " + municionMaxPistola;
        }
        else if (armas[1].activeSelf)
        {
            textoMunicion.text = municionRifle + " / " + municionMaxRifle;
        }
        else if (armas[2].activeSelf)
        {
            textoMunicion.text = municionEscopeta + " / " + municionMaxEscopeta;
        }
    }
}
