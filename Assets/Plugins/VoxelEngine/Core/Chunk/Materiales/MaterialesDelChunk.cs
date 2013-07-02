using UnityEngine;
using System.Collections.Generic;

/**
 * Clase de utilidad para asignar materiales a un chunk de forma dinamica
 * en su renderizado
 * 
 */
public class MaterialesDelChunk {	
	#region metodos publicos estaticos	
	/// <summary>
	/// Inicializa los materiales del MeshRenderer del GameObject que tiene el nombre pasado como parametro
	/// </summary>
	/// <param name='nombreChunkGO'>
	/// Nombre del Chunk GameObject
	/// </param>
	/// <param name='tiposMateriales'>
	/// Los tipos de materiales se que le van a adjuntar
	/// </param>
	public static void inicializarMateriales(string nombreChunkGO, List<TipoBloque> tiposMateriales){
		//obtenemos el GameObject chunk segun su nombre
		GameObject chunkGO = GameObject.Find(nombreChunkGO); 
		//obtenemos los materiales de su mesh renderer
		Material[] materialesMeshRenderer = chunkGO.GetComponent<MeshRenderer>().materials;
		//inicializamos el array de materiales al numero de materiales que necesita.
		materialesMeshRenderer = new Material[tiposMateriales.Count];
		//le adjuntmos el material correspondiente al array de materiales de la MeshRender del chunk GameObject, segun la lista de tipos de materiales
		for(int i=0; i<tiposMateriales.Count; i++){
			materialesMeshRenderer[i] = getMaterialAdjuntarAMesh(tiposMateriales[i]);
		}
		//adjuntamos un array de materiales al Mesh Renderer
		chunkGO.GetComponent<MeshRenderer>().materials = materialesMeshRenderer;
	}
	
	#endregion
	
	
	#region metodos privados estaticos
	/// <summary>
	/// Obtiene el material que se tiene que adjuntar a la Mesh del chunk segun el tipo de bloque que se indica
	/// </summary>
	/// <returns>
	/// El material a adjuntar a la Mesh
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque correspondiente al material a adjuntar
	/// </param>
	private static Material getMaterialAdjuntarAMesh(TipoBloque tipoBloque){
		Material mat = null;
		//obtenemos los materiales configurados por el usuario que se usaran en el renderizado del terreno
		MaterialChunkRenderizable[] materialesRenderizables = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().Materiales; 
		
		foreach(MaterialChunkRenderizable m in materialesRenderizables){
			if(m.TipoDelBloque == tipoBloque){
				mat = m.MaterialDeRenderizado; //obtenemos el material que tenemos que devolver
				break; //terminamos de iterar
			}
		}
		
		return mat;
	}
	



	#endregion
	

}
