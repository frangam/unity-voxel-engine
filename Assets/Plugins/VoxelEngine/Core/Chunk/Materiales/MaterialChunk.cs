using UnityEngine;
using System.Collections;

/* 
* Material terreno 
*/
[System.Serializable]
public class MaterialChunk {
	/// <summary>
	/// El nombre identificativo que se le da al material
	/// </summary>
	[SerializeField]
	private string nombre;
	
	/// <summary>
	/// El tipo de bloque al que se le aplicara el material
	/// </summary>
	[SerializeField]
	private TipoBloque tipoDelBloque;
	
	
	#region metodos publicos
	public TipoBloque TipoDelBloque{
		get{return tipoDelBloque;}
	}
	
	#endregion
}
