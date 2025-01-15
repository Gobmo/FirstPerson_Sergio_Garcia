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
    private RoundsController roundsController;
    private float cooldownAtaque = 0f;
    private int vida = 10;
    private float danoAtaque = 1f;
    [SerializeField] Transform attackPoint;
    [SerializeField] float radioAtaque;
    [SerializeField] LayerMask playerLayer;
    private CapsuleCollider coll;

    private bool ventanaAbierta = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Jugador>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        coll = GetComponent<CapsuleCollider>();
        roundsController = FindAnyObjectByType<RoundsController>();
    }

    // Update is called once per frame
    void Update()
    {
        Perseguir();
        MirarAlJugador();

        if (ventanaAbierta)
        {
            DetectarJugador();
        }

        cooldownAtaque -= Time.deltaTime; 
    }

    private void Perseguir()
    {
        agent.SetDestination(player.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
            animator.SetBool("Moving", false);
            if (cooldownAtaque <= 0)
            {
                animator.SetBool("Atack", true); 
            }
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Moving", true);
            animator.SetBool("Atack", false);
        }
    }

    #region eventos de animacion
    private void AbrirVentanaAtaque()
    {
        ventanaAbierta = true;
        cooldownAtaque = 1.5f;
    }
    private void CerrarVentanaAtaque()
    {
        ventanaAbierta = false;
        animator.SetBool("Atack", false);
    }
    #endregion


    private void MirarAlJugador()
    {
        Vector3 direccion = player.transform.position - transform.position;
        direccion.y = 0;

        if (direccion != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 5 * Time.deltaTime);
            transform.rotation = rotacion;
        }
    }

    private void DetectarJugador()
    {
        Collider[] collsDetectados = Physics.OverlapSphere(attackPoint.transform.position, radioAtaque, playerLayer);
        if (collsDetectados.Length > 0 )
        {
            player.RecibirDano(danoAtaque);
            ventanaAbierta = false;
        }
    }

    public void RecibirDano(int danho)
    {
        vida -= danho;
        if (vida <= 0)
        {
            animator.SetBool("Dead", true);
            roundsController.EnemigosMuertos++;
            coll.enabled = false;
            agent.enabled = false;
            this.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.transform.position, radioAtaque);
    }
}
