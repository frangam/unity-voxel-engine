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
	/// La posicion que ocupa el material asignado al tipo de bloque en el array
	/// Materials de su MeshRenderer
	/// </summary>
	private int posicionEnMaterialsDelMeshRenderer;
	
	
	#region metodos publicos
	public TipoBloque TipoDelBloque{
		get{return tipoDelBloque;}
	}
	
	public int PosicionEnMaterialsDelMeshRenderer{
		get{return posicionEnMaterialsDelMeshRenderer;}	
	}
	#endregion
}
