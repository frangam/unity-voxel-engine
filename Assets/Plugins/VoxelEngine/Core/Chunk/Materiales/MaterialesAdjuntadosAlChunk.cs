using UnityEngine;
using System.Collections;

/*
 * Clase adjunta al GameObject Chunk
 * 
 * Reprensenta un array de MaterialChunk en el cual se conoce el tipo de bloque 
 * y el indice que ocupaa el material para ese tipo de bloque en el array
 * materiales del MeshRenderer del chunk.
 * 
 * Esta clase sirve para calcular en funcion a este indice, el subindice de submesh (submalla)
 * en la que se tiene que aplicar dicho material.
 * 
 * En general, el subindice de la submesh se correspone en el orden inverso del array de materiales
 * de la MeshRenderer
 */ 
public class MaterialesAdjuntadosAlChunk : MonoBehaviour {
	/// <summary>
	/// los materiales que se le adjuntan al chunk para que se renderice con ellos
	/// </summary>
	public MaterialChunk[] materiales;
}
