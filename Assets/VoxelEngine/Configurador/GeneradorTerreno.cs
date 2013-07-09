using UnityEngine;
using System.Collections;
using Pathfinding;

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
public class GeneradorTerreno : MonoBehaviour 
{
	
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
	
	public AccionesTerreno acciones;
	#endregion
	
	#region atributos de configuracion del terreno
	public int numChunksEnX = 5;
	public int numChunksEnY = 1;
	public int numChunksEnZ = 5;
	public int nivelDelAgua = 3;
	#endregion
	
	#region metodos publicos
	public MaterialChunkRenderizable[] Materiales{
		get {return materiales;}	
	}
	#endregion
	
	#region metodos privados
	
	/// <summary>
	/// Genera el terreno de forma aleatoria, aplicando un algoritmo de ruido 3D
	/// </summary>
	private void generarTerrenoAleatorio()
	{
		SimplexNoise3D noise = new SimplexNoise3D(); //el algoritmo de ruido para generar montanias
		terreno = new Terreno(numChunksEnX, numChunksEnY, numChunksEnZ, nivelDelAgua);
		
		if(nivelDelAgua > Terreno.totalBloquesY || nivelDelAgua < 0){
			Debug.Log("Nivel del Agua fuera de los limites");
		}
		else{
			Transform aguaAlrededor = GameObject.Find("AguaAlrededor").transform; //obtenemos los hijos de AguaAlrededor
			
			//por cada parte del agua, le cambiamos la altura a su posicion segun el nivel del agua indicado
			foreach(Transform t in aguaAlrededor){
				t.position = new Vector3(t.position.x, nivelDelAgua, t.position.z);
			}
		}

		//vamos adjuntandole al terreno los bloques		
		for(int x=0; x<terreno.getNumTotalBloquesEnX(); x++){
			for(int z=0; z<terreno.getNumTotalBloquesEnZ(); z++){
				int altura = Mathf.RoundToInt(noise.CoherentNoise(x,0,z)*10.0f+ 2f);
				
				for(int y=0; y<terreno.getNumTotalBloquesEnY(); y++){
					Terreno.caminoAgua[x, y, z] = false; //inicializamos el camino de agua sobre el terreno a false
					Bloques.setBloqueEnCoordsTerreno(seleccionarBloque(x,y,z, altura),x,y,z); 
				}
			}
		}
		//esto se puede meter en el inicializar de terreno.
		for (int x = 0; x <  terreno.getNumChunksVisiblesEnX(); x++){
			for (int y = 0; y <  terreno.getNumChunksVisiblesEnY(); y++){
				for (int z = 0; z <  terreno.getNumChunksVisiblesEnZ(); z++){
					crearMallaDelTerreno(x, y, z);
				}
			}
		}
	}
	
	/// <summary>
	/// Se selecciona el bloque que se desea crear segun la y y la altura
	/// </summary>
	/// <returns>
	/// El bloque seleccionado
	/// </returns>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>
	/// <param name='z'>
	/// Z.
	/// </param>
	/// <param name='altura'>
	/// Altura
	/// </param>
	private Bloque seleccionarBloque(int x, int y, int z, int altura)
	{
		Bloque bloque = new Bloque();
		TipoBloque tipoElegido = TipoBloque.SUELO;
		
		if(y == 0)
			tipoElegido = TipoBloque.SUELO;
		else if( y == 1)
			tipoElegido = TipoBloque.PROXIMO_A_SUELO;
		else if (y==2)
			tipoElegido = TipoBloque.TIERRA;
		else if(y >= 3 && y < altura)
			tipoElegido = TipoBloque.HIERBA;
		else		
			tipoElegido = TipoBloque.VACIO;
		
		bloque = new Bloque(tipoElegido, x,y,z);
	
		return bloque;
	}
	
	//esto podria ir en el inicializar de terreno.
	private void crearMallaDelTerreno(int x, int y, int z){
		MallaChunk mallaChunk = Instantiate(mallaChunkPrefab) as MallaChunk; //instanciamos nuestro prefab de MallaChunk
		mallaChunk.name = terreno.getChunks()[x,y,z].ToString(); //le damos el nombre al GameObject
		mallaChunk.transform.parent = GameObject.Find("Terreno").transform; //asignamos el padre de la malla chunk la transform del terreno
		mallaChunk.transform.position = new Vector3(x*Chunk.numBloquesEnX, y*Chunk.numBloquesEnY, z*Chunk.numBloquesEnZ);
		mallaChunk.setChunk(terreno.getChunks()[x,y,z]); //asinamos el chunk a su malla
		terreno.crearMallaChunk(x, y, z, mallaChunk); //crear la malla del chunk 
	}
	
	#endregion
	
	
	
	#region Unity
	public void Start(){
		generarTerrenoAleatorio();
		AstarPath originalPath = GameObject.Find("GeneradorTerreno").GetComponent<AstarPath>();
		originalPath.Scan();
	}
	
	#endregion
}
