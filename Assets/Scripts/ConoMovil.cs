using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConoMovil : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

    private Vector3 destino;
	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        NuevoDestino();

    }
	
	// Update is called once per frame
	void Update () {
        //Si la distancia entre nuestra posicion y el destino es inferior a 0.3f, quiere decir que hemos llegado casi a nuestro destino, y buscamos uno nuevo
        if (Vector3.Distance(transform.position, destino) < 0.3f) NuevoDestino();
	}

    void NuevoDestino()
    {
        // Asignamos que vaya hasta la posicion 8 en el eje X, si el cono esta ya en la derecha, el destino estara en -8 en el eje X
        float posicionX = 8;
        if (transform.position.x > 0) posicionX = -8;

        destino = new Vector3(posicionX, transform.position.y, transform.position.z);

        navMeshAgent.destination = destino;
    }
}
