using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] LayerMask ataudesMask;
    [SerializeField] float radioDeteccionAtaudes;
    [SerializeField] GameObject prefabEnemigo;
    [SerializeField] RoundsController roundsController;
    private Collider[] puntosSpawn;
    private int random = 0;

    void Update()
    {
        
    }

    public void SpawnEnemigos()
    {
        Collider[] puntosSpawn = Physics.OverlapSphere(transform.position, radioDeteccionAtaudes, ataudesMask);
        random = Random.Range(0, puntosSpawn.Length);
        GameObject ataudSpawn = puntosSpawn[random].gameObject;
        GameObject enemigo = Instantiate(prefabEnemigo, ataudSpawn.transform.position, Quaternion.identity);
        roundsController.EnemigosSpawneados++;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radioDeteccionAtaudes);
    }
}
