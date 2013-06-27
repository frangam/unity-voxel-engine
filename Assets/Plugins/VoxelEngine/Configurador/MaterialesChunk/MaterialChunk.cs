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
	
	
	/// <summary>
	/// El material
	/// </summary>
	[SerializeField]
	private Material material;
	
	
	#region metodos publicos
	public TipoBloque TipoDelBloque{
		get{return tipoDelBloque;}
	}
	
	public Material Material{
		get{return material;}
	}
	#endregion
}
