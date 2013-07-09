using UnityEngine;
using System.Collections;

/*
 * Autodestruye el gameobject al que se le adjunte el script
 */ 
public class AutoDestruir : MonoBehaviour 
{
	public float tiempoEspera;
	
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(tiempoEspera);
		GameObject.DestroyImmediate(gameObject);
	}
}
