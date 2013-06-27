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
public class GeneradorTerreno : MonoBehaviour {
	
	#region atributos publicos
	/// <summary>
	/// Los materiales que podran tener los chunks
	/// </summary>
	public MaterialChunkRenderizable[] materiales;
	
	/// <summary>
	/// El terreno que va a generar
	/// </summary>
	public Terreno terreno;
	
	/// <summary>
	/// Un prefab con nuestra malla chunk
	/// </summary>
	public MallaChunk mallaChunkPrefab;
	#endregion
	
	
	#region metodos publicos
	public MaterialChunkRenderizable[] Materiales{
		get {return materiales;}	
	}
	#endregion
	
	#region metodos privados
	private void generar(){	
		terreno.inicializarTerreno();
		
		//creamos las mallas
		for(int x=0; x<terreno.getNumChunksVisiblesEnX(); x++){
			for(int y=0; y<terreno.getNumChunksVisiblesEnY(); y++){
				for(int z=0; z<terreno.getNumChunksVisiblesEnZ(); z++){
					terreno.inicializarChunks(x, y, z); //inicializamos los chunks
					crearMallaDelTerreno(x, y, z);
					ChunkRenderer.renderizar (terreno.getChunks()[x,y,z]);
				}
			}
		}
	}
	
	private void crearMallaDelTerreno(int x, int y, int z){
		MallaChunk mallaChunk = Instantiate(mallaChunkPrefab) as MallaChunk; //instanciamos nuestro prefab de MallaChunk
		mallaChunk.name = "Chunk ("+x+","+y+","+z+")"; //le damos el nombre al GameObject
		mallaChunk.transform.parent = terreno.transformTerreno; //asignamos el padre de la malla chunk la transform del terreno
		mallaChunk.transform.position = new Vector3(x*Chunk.numBloquesEnX, y*Chunk.numBloquesEnY, z*Chunk.numBloquesEnZ);
		terreno.crearMallaChunk(x, y, z, mallaChunk); //crear la malla del chunk 		
	}
	
	#endregion
	
	
	
	#region Unity
	public void Start(){
		generar(); //generamos el terreno en el inicio
	}
	
	#endregion
	

}
