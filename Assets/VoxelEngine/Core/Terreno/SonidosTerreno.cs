using UnityEngine;
using System.Collections;

/*
 * Clase adjunta a gameobject para controlar los sonidos que se reproducen
 * y estan relacionados con el terreno
 */ 
public class SonidosTerreno : MonoBehaviour 
{
	#region atributos de configuracion desde el inspector
	public AudioClip sonidoDestruir;
	public AudioClip sonidoConstruir;
	public AudioClip sonidoAgua;
	#endregion
	

	public void playAgua(){
		SoundManager.Instance.PlaySoundEffect(sonidoAgua);
	}
	
	public void playCrearBloque(){
		SoundManager.Instance.PlaySoundEffect(sonidoConstruir, 0.75f);
	}
	
	public void playDestruirBloque(){
		SoundManager.Instance.PlaySoundEffect(sonidoDestruir, 3.5f);
	}
	
}
