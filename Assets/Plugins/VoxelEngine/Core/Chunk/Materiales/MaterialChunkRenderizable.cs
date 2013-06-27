using UnityEngine;
using System.Collections;

/*
 * Clase que hereda de MaterialChunk
 * tiene como atributo un Material con el que se renderizara el chunk
 */ 
[System.Serializable]
public class MaterialChunkRenderizable : MaterialChunk {
	/// <summary>
	/// El propio material que sera usado para renderizar el chunk
	/// </summary>
	[SerializeField]
	private Material materialDeRenderizado;
	
	
	#region metodos publicos
	public Material MaterialDeRenderizado{
		get{return materialDeRenderizado;}
	}
	#endregion
}
