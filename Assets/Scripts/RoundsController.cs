using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundsController : MonoBehaviour
{
    private int enemigosSpawneados = 0, enemigosASpawnear = 20, enemigosMuertos = 0, numeroRonda = 1;
    float cooldownSpawn = 5f;
    bool activo = false;
    [SerializeField] SpawnController spawnController;
    [SerializeField] GameObject bloqueo;

    public int EnemigosSpawneados { get => enemigosSpawneados; set => enemigosSpawneados = value; }
    public int EnemigosMuertos { get => enemigosMuertos; set => enemigosMuertos = value; }

    [SerializeField] float radioDeteccionBoton;
    [SerializeField] LayerMask botonMask;

    void Start()
    {
        bloqueo.SetActive(false);
    }

    void Update()
    {
        if (activo == true)
        {
            EmpezarOleada();
            bloqueo.SetActive (true);
        }
        else
        {
            bloqueo.SetActive(false);
            DetectarBoton();
        }
        if (enemigosMuertos == enemigosSpawneados)
        {
            LimpiarEscenario();
        }
    }

    private void DetectarBoton()
    {
        Collider[] botonEmpezar = Physics.OverlapSphere(transform.position, radioDeteccionBoton, botonMask);
        if (botonEmpezar.Length > 0)
        {
            activo = true;
        }
    }

    private void EmpezarOleada()
    {
        Debug.Log(enemigosASpawnear);
        cooldownSpawn -= Time.deltaTime;
        if (cooldownSpawn < 0 && enemigosSpawneados < enemigosASpawnear)
        {
            spawnController.SpawnEnemigos();
            cooldownSpawn = Random.Range(1, 5f);
            Debug.Log(enemigosSpawneados);
        }
        if (enemigosSpawneados == enemigosASpawnear)
        {
            activo = false;
        }
    }

    private void LimpiarEscenario()
    {
        numeroRonda++;
        enemigosSpawneados = 0;
        enemigosASpawnear++;
        for (int i = 0; i < enemigosMuertos; i++)
        {
            Enemigo enemigoABorrar = FindAnyObjectByType<Enemigo>();
            Destroy(enemigoABorrar.gameObject);
        }
        enemigosMuertos = 0;
    }
}
