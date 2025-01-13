using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ArmasController : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    private float cooldownDisparar = 0;
    private int municionPistola = 0, municionRifle = 0, municionEscopeta = 0;
    private int municionMaxPistola = 15, municionMaxRifle = 30, municionMaxEscopeta = 8;
    [SerializeField] LayerMask enemigoMask;
    int armaActiva = 0;
    GameObject[] armas = new GameObject[3];
    Animator[] animatorArmas = new Animator[3];

    void Start()
    {
        armas[0] = GameObject.Find("Pistola");
        armas[1] = GameObject.Find("Rifle");
        armas[2] = GameObject.Find("Escopeta");

        animatorArmas[0] = armas[0].GetComponent<Animator>();
        animatorArmas[1] = armas[1].GetComponent<Animator>();
        animatorArmas[2] = armas[2].GetComponent<Animator>();
    }

    void Update()
    {
        cooldownDisparar -= Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
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
        if (Input.GetKeyDown(KeyCode.Alpha2) && armas[0].activeSelf == false)
        {
            CambiarAPistola();
        }
    }

    private void CambiarARifle()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && armaActiva != 0)
        {
            armas[0].SetActive(false); // Ocultar pistola
            armas[2].SetActive(false); // Ocultar escopeta
            armas[1].SetActive(false); // Sacar rifle
        }
    }

    private void CambiarAEscopeta()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && armaActiva != 0)
        {
            armas[0].SetActive(true); // Ocultar pistola
            armas[1].SetActive(false); // Ocultar rifle
            armas[2].SetActive(false); // Sacar escopeta
        }
    }

    private void CambiarAPistola()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && armas[0].activeSelf == false)
        {
            armas[1].SetActive(false); // Ocultar rifle
            armas[2].SetActive(false); // Ocultar escopeta
            armas[0].SetActive(true); // Sacar pistola
        }
    }
    
    private void Shoot()
    {
        //if (cooldownDisparar < 0 && municion > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, 100f, enemigoMask))
            {
                //APLICAR DAÑO AL ENEMIGO
                Enemigo enemigoImpactado = hit.collider.gameObject.GetComponent<Enemigo>();
                //enemigoImpactado.RecibirDano();
            }
        }
    }
}
