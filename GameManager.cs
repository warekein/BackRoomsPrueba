using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] arrayMapas;
    [SerializeField] private List<GameObject> listaMapasAlrededor;
    [SerializeField] private GameObject mapaActual;
    [SerializeField] private Vector3 posicionMapaActual;
    private int mapaQueTocaPoner;

    public static GameManager instancia;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        MapaActualPosicion(mapaActual);
        listaMapasAlrededor.Add(mapaActual);
    }

    public void MapaActualPosicion(GameObject mapa)
    {

        mapaActual = mapa;
        posicionMapaActual = mapaActual.transform.position;
    }

    public void CambiaIndiceMapa(int mapa)
    {
        mapaQueTocaPoner = mapa;
    }

    public void CrearEscenario(float posX, float posZ)
    {
      
        if (mapaQueTocaPoner < arrayMapas.Length -1)
        {
            GameObject esteMapa = Instantiate(arrayMapas[mapaQueTocaPoner], new Vector3(posicionMapaActual.x + posX, posicionMapaActual.y, posicionMapaActual.z + posZ), Quaternion.identity);
            listaMapasAlrededor.Add(esteMapa);
        }
    }

    public void BorrarMapasAlrededor()
    {
      
        foreach (var mapa in listaMapasAlrededor)
        {
            CreadorEscenarios creadorEscenarios = mapa.GetComponentInChildren<CreadorEscenarios>();
            if (creadorEscenarios.borrarEsteMapa == true)
            {
                Destroy(mapa);
            }
            else
            {
                
                creadorEscenarios.borrarEsteMapa = true;
                mapaActual = creadorEscenarios.transform.parent.gameObject;
            }
        }
        listaMapasAlrededor.Clear();
        listaMapasAlrededor.Add(mapaActual);
        
    }

}
