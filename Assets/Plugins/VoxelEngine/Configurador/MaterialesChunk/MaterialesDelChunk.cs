using UnityEngine;
using System.Collections;

/* 
* Esta es una clase de configuracion de los materiales que se utilizaran en el chunk.
* 
* Uso:
* Abrir el prefab Chunk dentro de Plugins/VoxelEngine/Resources/Prefabs/Chunk/Chunk
* A continuacion desplegamos el array de materiales de este script MaterialesChunk. 
* Adjuntar en el array de materiales cada material que se desee y un nombre para cada tipo de bloque concreto: AGUA, TIERRA, PIEDRA, HIERBA...
* 
* 
* >> Precondiciones: El tipo de bloque de MaterialChunk debe ser unico
*/
public class MaterialesDelChunk : MonoBehaviour {
	#region atributos publicos
	public MaterialChunk[] materiales;
	#endregion
	
	
	#region Unity
	// inicializacion de los materiales
	void Start () {
		inicializarMateriales();
	}
	#endregion
	
	
		#region metodos publicos estaticos
	/// <summary>
	/// Este metodo sirve obtener el valor adecuado de una submalla de una Mesh cuando se utiliza el metodo Mesh.SetTriangles(int[] arrayTriangulosSubmalla, int indiceSubmalla).
	/// Dicho indice sirve para indicar cual sera el material del MeshRenderer que se debe utilizar para renderizar esa submalla.
	/// Hay que comprender cómo asigna Unity los materiales a las submallas de MeshRenderer:
	/// >> La asignacion de materiales a una submalla de MeshRender se realiza teniendo en cuenta el indice de submalla que se le indica
	/// >> y segun este, se accede al array de materiales de MeshRenderer orden inverso, es decir, desde el ultimo elemento hasta el primero.
	/// >> Por ejemplo: indice de submalla 2 (indica que es la submalla numero 2). Suponiendo que el array de materiales tiene 4 elementos que están en las posiciones 0, 1, 2 y 3.
	/// >> Pues bien, el material que se le asigna a la submalla es el material que ocupa la posicion 2 del array, porque la subllama 1 tendria asignado el ultimo elemento del array
	/// >> de materiales el numero 3. La submalla 3 tendria asignado el material de la posicion del array 1, y la ultima cuarta submalla el elemento 0 del array.
	/// 
	/// 
	/// -- Precondiciones: El tipo de bloque de MaterialChunk debe ser unico
	/// </summary>
	/// <returns>
	/// The indice del material que ocua en el array de materiales de MeshRenderer
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque
	/// </param>
	/// <param name='mallaChunk'>
	/// malla del chunk
	/// </param>
	public static int getIndiceMaterialSegunTipo(MallaChunk mallaChunk, TipoBloque tipoBloque){
		int indice = 0;
		int posicion = getPosicionMaterialEnArrayMateriales(tipoBloque); //obtenemos la posicion que ocupa el material de chunk con un tipo de bloque indicado
		
		return indice;
	}
	
	#endregion
	
	
	#region metodos privados
	private void inicializarMateriales(){
		MeshRenderer mrChunk = GetComponent<MeshRenderer>(); //obtenemos el componente MeshRenderer del prefab Chunk
		
		mrChunk.materials = new Material[materiales.Length]; //instanciamos el array de materiales del meshrenderer del chunk con el numero de materiales que se hayan adjuntado (lo limpiamos)
		
		//le adjuntamos al meshrenderer del chunk los materiales que se hayan indicado
		for(int i=0; i<materiales.Length; i++){
			mrChunk.materials[i] = materiales[0].Material;
		}
	}
	
	/// <summary>
	/// Devuelve la posicion que ocupa un material de un tipo concreto en el array de materiales
	/// 
	/// >> Precondiciones: el tipo de bloque debe ser unico en dicho array de materiales de chunk
	/// </summary>
	/// <returns>
	/// la posicion del material del tipo de bloque especifico en el array de materiales
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque al que le correspondo el material
	/// </param>
	private static int getPosicionMaterialEnArrayMateriales(TipoBloque tipoBloque){
		int posicion = 0;
		bool tipoBloqueEncontrado = false;
		
		for(int i=0; i<materiales.Length && !tipoBloqueEncontrado;i++){
			tipoBloqueEncontrado = materiales[i].TipoDelBloque == tipoBloque;
				
			if(tipoBloqueEncontrado){
				posicion = i; //guardamos la posicion que ocupa en el array materiales ese MaterialChunk con el tipo de bloque indicado
			}
		}
		
		return posicion;
	}
	#endregion
	

}
