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
		GameObject chunkGO = GameObject.Find(nombreChunkGO); //obtenemos el GameObject chunk segun su nombre
		Material[] materialesMeshRenderer = chunkGO.GetComponent<MeshRenderer>().materials; //obtenemos los materiales de su mesh renderer
		MaterialChunk[] matAdjuntados = chunkGO.GetComponent<MaterialesAdjuntadosAlChunk>().materiales; //obtenemos su array de materiales de chunk
		
		//instanciamos dicho array con el numero de tipos de materiales que tiene que renderizar
		matAdjuntados = new MaterialChunk[tiposMateriales.Count]; 
		
		//instanciamos el array de materiales del MeshRenderer del GameObject Chunk que tiene el nombre "nombreChunkGO"
		materialesMeshRenderer = new Material[tiposMateriales.Count];
	
		//le adjuntmos el material correspondiente a al array de materiales de la MeshRender del chunk GameObject, segun la lista de tipos de materiales
		for(int i=0; i<tiposMateriales.Count; i++){
			materialesMeshRenderer[i] = getMaterialAdjuntarAMesh(tiposMateriales[i]);	
		}
	}
	
	/// <summary>
	/// Devuelve la posicion que ocupa un material de un tipo concreto en el array de materiales
	/// 
	/// >> Precondiciones: el tipo de bloque debe ser unico en dicho array de materiales de chunk
	/// </summary>
	/// <returns>
	/// The posicion material en array materiales.
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque asociado al material
	/// </param>
	public static int getPosicionMaterialEnArrayMateriales(string nombreChunkGO, TipoBloque tipoBloque){
		int posicion = 0;
		GameObject chunkGO = GameObject.Find(nombreChunkGO); //obtenemos el GameObject chunk segun su nombre
		MaterialChunk[] matAdjuntados = chunkGO.GetComponent<MaterialesAdjuntadosAlChunk>().materiales; //obtenemos su array de materiales de chunk
		bool tipoBloqueEncontrado = false;
		
		for(int i=0; i<matAdjuntados.Length && !tipoBloqueEncontrado;i++){
			tipoBloqueEncontrado = matAdjuntados[i].TipoDelBloque == tipoBloque;
				
			if(tipoBloqueEncontrado){
				posicion = i; //guardamos la posicion que ocupa en el array materiales ese MaterialChunk con el tipo de bloque indicado
			}
		}
		
		return posicion;
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
		MaterialChunkRenderizable[] materialesRenderizables = GameObject.Find("ConfigTerreno").GetComponent<ConfigTerreno>().Materiales; //obtenemos los materiales configurados por el usuario que se usaran en el renderizado del terreno
		
		
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
