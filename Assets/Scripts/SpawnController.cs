using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] LayerMask ataudesMask;
    [SerializeField] float radioDeteccionAtaudes;
    [SerializeField] GameObject prefabEnemigo;
    Collider[] puntosSpawn;
    private int random = 0;

    void Update()
    {
        DetectarAtaudes();
    }

    private void DetectarAtaudes()
    {
        Collider[] puntosSpawn = Physics.OverlapSphere(transform.position, radioDeteccionAtaudes, ataudesMask);
    }

    private void SpawnEnemigos()
    {
        random = Random.Range(0, puntosSpawn.Length);
        GameObject ataudSpawn = puntosSpawn[random].gameObject;
        GameObject enemigo = Instantiate(prefabEnemigo, ataudSpawn.transform.position, Quaternion.identity);
        //enemigosSpawneados++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radioDeteccionAtaudes);
    }
}
