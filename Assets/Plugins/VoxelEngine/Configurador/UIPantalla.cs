using UnityEngine;
using System.Collections;

public class UIPantalla : MonoBehaviour {
	
	/// <summary>
	/// Las acciones que se pueden realizar sobre el terreno
	/// </summary>
	private Acciones acciones; 
	
	/// <summary>
	/// a accion que se realiza: cavar el terreno, colocar, no hacer nada.
	/// </summary>
	public static TipoAccionTerreno accionARealizarEnTerreno = TipoAccionTerreno.CAVAR; //TODO: cambiar //TipoAccionTerreno.NO_ACCION;

	// Use this for initialization
	void Start () {
		acciones = GameObject.Find("Acciones").GetComponent<Acciones>();
		
		//TODO: cambiar aacion a un boton
		acciones.setTipoBloqueSeleccionado(TipoBloque.VACIO);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
