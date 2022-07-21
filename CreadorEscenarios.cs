using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreadorEscenarios : MonoBehaviour
{
    public enum Posiciones
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha

    };

    [SerializeField] private int siguienteMapa;
    private BoxCollider col;
    public bool borrarEsteMapa = true;


    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    public void BuclePosiciones()
    {
        string[] posiciones = System.Enum.GetNames(typeof(Posiciones));
        foreach (var posicion in posiciones)
        {
            GenerarEscenario(posicion);
        }
    }


    private void GenerarEscenario(string posicion)
    {

        GameManager.instancia.MapaActualPosicion(this.transform.parent.gameObject);

        switch (posicion)
        {
            case "Arriba":
                GameManager.instancia.CrearEscenario(0, 50);
                break;
            case "Abajo":
                GameManager.instancia.CrearEscenario(0, -50);
                break;
            case "Izquierda":
                GameManager.instancia.CrearEscenario(-50, 0);
                break;
            case "Derecha":
                GameManager.instancia.CrearEscenario(50, 0);
                break;
        }

        col.enabled = false;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            borrarEsteMapa = false;
            GameManager.instancia.CambiaIndiceMapa(siguienteMapa);
            GameManager.instancia.BorrarMapasAlrededor();
            BuclePosiciones();
        }
    }
}
