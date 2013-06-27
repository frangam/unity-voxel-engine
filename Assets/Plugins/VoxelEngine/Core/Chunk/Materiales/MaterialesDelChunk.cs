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
		
		MaterialChunk[] matAdjuntados = chunkGO.GetComponent<MaterialesAdjuntadosAlChunk>().materiales; //obtenemos su array de materiales de chunk
		
		
		
		//instanciamos el array de materiales del MeshRenderer del GameObject Chunk que tiene el nombre "nombreChunkGO"
		
		
//		for(int i=0; i<tiposMateriales.Length; i++){
//			//le adjuntmos el material correspondiente a la MeshRender del chunk GameObject, segun la lista de tipos de materiales
//			GameObject.Find(nombreChunkGO).GetComponent<MeshRenderer>().materials[i] = getMaterialAdjuntarAMesh(tiposMateriales[i]);	
//		}
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
	public static int getPosicionMaterialEnArrayMateriales(TipoBloque tipoBloque){
		int posicion = 0;
		bool tipoBloqueEncontrado = false;
		
//		for(int i=0; i<materiales.Length && !tipoBloqueEncontrado;i++){
//			tipoBloqueEncontrado = materiales[i].TipoDelBloque == tipoBloque;
//				
//			if(tipoBloqueEncontrado){
//				posicion = i; //guardamos la posicion que ocupa en el array materiales ese MaterialChunk con el tipo de bloque indicado
//			}
//		}
		
		return posicion;
	}
	
	#endregion
	
	
	#region metodos privados
	/// <summary>
	/// Obtiene el material que se tiene que adjuntar a la Mesh del chunk segun el tipo de bloque que se indica
	/// </summary>
	/// <returns>
	/// El material a adjuntar a la Mesh
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque correspondiente al material a adjuntar
	/// </param>
	private Material getMaterialAdjuntarAMesh(TipoBloque tipoBloque){
		Material mat = null;
//		
//		foreach(MaterialChunk mc in materiales){
//			if(mc.TipoDelBloque == tipoBloque){
//				mat = mc.Material; //obtenemos el material que tenemos que devolver
//				break; //terminamos de iterar
//			}
//		}
		
		return mat;
	}
	



	#endregion
	

}
