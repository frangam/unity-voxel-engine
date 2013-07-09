using UnityEngine;
using System.Collections;

public class UIPantalla : MonoBehaviour {
	
	/// <summary>
	/// Las acciones que se pueden realizar sobre el terreno
	/// </summary>
	private AccionesTerreno acciones; 
	
	public Boton botonDestruir;
	
	public Boton botonCrearHierba;
	
	/// <summary>
	/// a accion que se realiza: cavar el terreno, colocar, no hacer nada.
	/// </summary>
	public static TipoAccionTerreno accionARealizarEnTerreno = TipoAccionTerreno.NO_ACCION;
	
	#region Unity
	// Use this for initialization
	void Start () {
		botonDestruir.EventPressed += destruirBloque;
		botonCrearHierba.EventPressed += crearBloqueHierba;
		
		acciones = GameObject.Find("AccionesTerreno").GetComponent<AccionesTerreno>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	#endregion
	
	#region control de evento click en botones
	void destruirBloque(){
		if(accionARealizarEnTerreno == TipoAccionTerreno.CAVAR){
			accionARealizarEnTerreno = TipoAccionTerreno.NO_ACCION;
		}
		else{
			accionARealizarEnTerreno = TipoAccionTerreno.CAVAR;
			acciones.setTipoBloqueSeleccionado(TipoBloque.VACIO);
		}
	}
	
	void crearBloqueHierba(){
		if(accionARealizarEnTerreno == TipoAccionTerreno.CONSTRUIR){
			accionARealizarEnTerreno = TipoAccionTerreno.NO_ACCION;
		}
		else{
			accionARealizarEnTerreno = TipoAccionTerreno.CONSTRUIR;
			acciones.setTipoBloqueSeleccionado(TipoBloque.HIERBA);
		}
	}
	#endregion
}
