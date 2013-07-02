using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Events;

/*
 * Gestion de toques sobre el Chunk
 */ 
public class AccionSobreChunk : MonoBehaviour {
	/// <summary>
	/// las acciones que se pueden realizar sobre el chunk
	/// </summary>
	private Acciones acciones;
	
	void Start () {
		acciones = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().acciones;
		GetComponent<TapGesture>().StateChanged += tapSobreTile; //le adjunto el nombre del metodo para el evento del gesto tap
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	#region gestion del gesto tap
	/// <summary>
	/// Gestion del gesto Tap sobre el chunk
	/// </summary>
	/// <param name='sender'>
	/// El gesto
	/// </param>
	/// <param name='e'>
	/// Estado del gesto
	/// </param>
	private void tapSobreTile (object sender, GestureStateChangeEventArgs e){
		var gesto = sender as TapGesture; //el gesto
		Vector2 posicionToque = Vector2.zero; //posicion de toque en pantalla
		
		//segun el estado del gesto
		switch(e.State){
			case  Gesture.GestureState.Began: //fin del gesto
				posicionToque = gesto.PreviousScreenPosition;
				Debug.Log("tocando chunk en posicion screen: "+posicionToque);
			
				//segun el tipo de accion a realizar sobre el terreno
				switch(UIPantalla.accionARealizarEnTerreno){
					case TipoAccionTerreno.CAVAR:
						acciones.DestroyBlock(posicionToque); //destruimos el bloque
					break;
					case TipoAccionTerreno.CONSTRUIR:
						acciones.CreateBlock(posicionToque); //construimos el bloque
					break;
				}
			break;
			case  Gesture.GestureState.Ended: //cuando empieza el gesto de tocar el tile
						
			break;
		}
	}
	#endregion
}
