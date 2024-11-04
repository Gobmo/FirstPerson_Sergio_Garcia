using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private Jugador player;
    private Animator animator;
    [SerializeField] float danoAtaque;
    [SerializeField] Transform attackPoint;
    [SerializeField] float radioAtaque;
    [SerializeField] LayerMask playerLayer;

    private bool ventanaAbierta = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Jugador>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();

        if (ventanaAbierta)
        {
            DetectarJugador();
        }
    }

    private void Perseguir()
    {
        agent.SetDestination(player.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("attacking", true);
        }
        else
        {
            animator.SetBool("attacking", false);
        }
    }

    #region eventos de animacion
    private void FinAtaque()
    {
        animator.SetBool("attacking", false);
    }
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
    }
    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;
    }
    #endregion


    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.transform.position, radioAtaque, playerLayer);
    }
}
