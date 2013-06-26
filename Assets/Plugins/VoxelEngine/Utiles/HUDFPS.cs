using UnityEngine;
using System.Collections;
 

/// <summary>
/// Adjuntarlo a un GUIText para crear un indicador de frames/segundo
/// 
/// Calcula los frames/segundo en cada intervalo de actualizacion
/// </summary>
public class HUDFPS : MonoBehaviour {
 
	public  float intervaloActualizacion = 0.5F; //el intervalo de actualizacion
	public Color colorTexto = Color.black; //color del GUIText
	 
	private float fpsAcumulados   = 0; // FPS acumulados en el intervalo de actualizacion
	private int   frames  = 0; // Frames que se dibujan en el intervalo de actualizacion
	private float tiempoRestante; // el tiempo que le queda al intervalo de actualizacion
	 
	#region Unity
	void Start(){
	    if(!guiText){ //si no se ha adjuntado a un GUIText se avisa y se deshabilita
	        Debug.Log("HUDFPS necesita un componente GUIText!");
	        enabled = false; //lo deshabilitamos
	        return;
	    }
	    tiempoRestante = intervaloActualizacion;  
		guiText.material.color = colorTexto;
	}
	 
	/// <summary>
	/// Se realiza el calculo de los frames/segundo en cada intervalo del metodo update
	/// </summary>
	void Update(){
	    tiempoRestante -= Time.deltaTime;
	    fpsAcumulados += Time.timeScale/Time.deltaTime;
	    ++frames;
	 
	    if( tiempoRestante <= 0.0 ){
			float fps = fpsAcumulados/frames;
			string format = System.String.Format("{0:F2} FPS",fps); //creamos el texto con los fps dibujados
			guiText.text = format; //asignamos el texto al GUIText
	
	        tiempoRestante = intervaloActualizacion;
	        fpsAcumulados = 0.0F;
	        frames = 0;
	    }
	}
	#endregion
}