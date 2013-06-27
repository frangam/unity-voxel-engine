using UnityEngine;
using System.Collections;

/* 
* Esta es una clase de configuracion del terreno.
* 
* Adjuntar materiales para renderizado de chunk:
* ------------------------------------------------
* Abrir el prefab Chunk dentro de Plugins/VoxelEngine/Resources/Prefabs/Chunk/Chunk
* A continuacion desplegamos el array de materiales de este script MaterialesChunk. 
* Adjuntar en el array de materiales cada material que se desee y un nombre (opcional) para cada tipo de bloque concreto: AGUA, TIERRA, PIEDRA, HIERBA...
* Así le indicamos a que tipo de bloque se le asignara ese material.
*
* >> Precondiciones: El tipo de bloque de MaterialChunk debe ser unico
*/
public class ConfigTerreno : MonoBehaviour {
	
	#region atributos publicos
	/// <summary>
	/// Los materiales que podran tener los chunks
	/// </summary>
	public MaterialChunkRenderizable[] materiales;
	#endregion
	
	
	
	public MaterialChunkRenderizable[] Materiales{
		get {return materiales;}	
	}
	

}
