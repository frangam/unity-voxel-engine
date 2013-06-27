using UnityEngine;
using System.Collections;

/* 
* Esta es una clase de configuracion de los materiales que se utilizaran en el chunk.
* 
* Uso:
* Abrir el prefab Chunk dentro de Plugins/VoxelEngine/Resources/Prefabs/Chunk/Chunk
* A continuacion desplegamos el array de materiales de este script MaterialesChunk. 
* Adjuntar en el array de materiales cada material que se desee y un nombre para cada tipo de bloque concreto: AGUA, TIERRA, PIEDRA, HIERBA...
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
	
	#region metodos privados
	private void inicializarMateriales(){
		MeshRenderer mrChunk = GetComponent<MeshRenderer>(); //obtenemos el componente MeshRenderer del prefab Chunk
		
		mrChunk.materials = new Material[materiales.Length]; //instanciamos el array de materiales del meshrenderer del chunk con el numero de materiales que se hayan adjuntado (lo limpiamos)
		
		//le adjuntamos al meshrenderer del chunk los materiales que se hayan indicado
		for(int i=0; i<materiales.Length; i++){
			mrChunk.materials[i] = materiales[0].Material;
		}
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
	/// </summary>
	/// <returns>
	/// The indice del material que ocua en el array de materiales de MeshRenderer
	/// </returns>
	/// <param name='tipoBloque'>
	/// El tipo de bloque
	/// </param>
	public static int getIndiceMaterialSegunTipo(TipoBloque tipoBloque){
		return 0;
	}
	#endregion
}
