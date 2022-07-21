using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ControladorBacteria : MonoBehaviour
{
    private float velocidad = 0;
    private Animator anim;
    private NavMeshAgent agente;
    private bool personajeVisto;
    private bool bacteriaEstaGritando = true;
    private AudioSource sonidoBacteria;

    [SerializeField] private Transform objetivo;
    [SerializeField] private Transform ojosBacteria;
 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        agente = GetComponent<NavMeshAgent>();
        sonidoBacteria = GetComponent<AudioSource>();
    }

    private void Update()
    {
        agente.SetDestination(objetivo.position);
        VistaBacteria();
        if (personajeVisto)
        {
            CazarAlPersonaje();
            if (bacteriaEstaGritando)
            {
                GritoBacteria();
            }
            bacteriaEstaGritando = false;
        }

    }


    private void VistaBacteria()
    {
        RaycastHit hit;
        if (Physics.Raycast(ojosBacteria.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(ojosBacteria.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                personajeVisto = true;
            }
        }

    }

    private void CazarAlPersonaje()
    {
        if (velocidad <= 1)
        {
            velocidad += Time.deltaTime;
            anim.SetFloat("Correr", velocidad);
            agente.speed += velocidad;
        }
        else
        {
            personajeVisto = false;
        }
    }

    private void GritoBacteria()
    {
        sonidoBacteria.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
