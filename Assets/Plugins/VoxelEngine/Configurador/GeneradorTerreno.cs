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
	
	public Acciones acciones;
	#endregion
	
	public int numChunksEnX = 5;
	public int numChunksEnY = 1;
	public int numChunksEnZ = 5;
	
	#region metodos publicos
	public MaterialChunkRenderizable[] Materiales{
		get {return materiales;}	
	}
	#endregion
	
	#region metodos privados
	private void generar(){		
		terreno.inicializarChunks();//inicializar los chunks del terreno
		
		//generamos los datos del terreno
		for(int x=0; x<terreno.getNumTotalBloquesEnX(); x++){
			for(int y=0; y<terreno.getNumTotalBloquesEnY(); y++){
				for(int z=0; z<terreno.getNumTotalBloquesEnZ(); z++){
					//creamos los bloques en coordenadas de terreno
					terreno.setBloque(new Bloque(TipoBloque.AGUA), x, y, z);
				}
			}
		}
		
		//generamos la malla del terreno
		for (int x = 0; x <  terreno.getNumChunksVisiblesEnX(); x++){
			for (int y = 0; y <  terreno.getNumChunksVisiblesEnY(); y++){
				for (int z = 0; z <  terreno.getNumChunksVisiblesEnZ(); z++){
					crearMallaDelTerreno(x, y, z);
				}
			}
		}
	}
	
	
	private void generarTerrenoAleatorio()
	{
		SimplexNoise3D noise = new SimplexNoise3D();
		terreno = new Terreno(numChunksEnX, numChunksEnY, numChunksEnZ);
		acciones.Init(terreno);
		
		for(int x=0; x<terreno.getNumTotalBloquesEnX(); x++){
			for(int z=0; z<terreno.getNumTotalBloquesEnZ(); z++){
				int height = Mathf.RoundToInt(noise.CoherentNoise(x,0,z) *35.0f +5.0f);
				for(int y=0; y<terreno.getNumTotalBloquesEnY(); y++){
					terreno.setBloque (seleccionarBloque(x,y,z, height),x,y,z);
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
	private Bloque seleccionarBloque(int x, int y, int z, int height)
	{
		int maxHeight = terreno.getNumTotalBloquesEnY();
		Bloque bloque = new Bloque();
		if(y == 0)
				bloque = new Bloque(TipoBloque.SUELO, x,y,z);
		else if (y >= 1  && y <= 2)
				bloque = new Bloque(TipoBloque.TIERRA, x,y,z);
		else if(y >= 3 && y < height)
				bloque = new Bloque(TipoBloque.HIERBA, x,y,z);
		else		
				bloque = new Bloque(TipoBloque.VACIO, x,y,z);
		
		return bloque;
	}
	
	//esto podria ir en el inicializar de terreno.
	private void crearMallaDelTerreno(int x, int y, int z){
		MallaChunk mallaChunk = Instantiate(mallaChunkPrefab) as MallaChunk; //instanciamos nuestro prefab de MallaChunk
		mallaChunk.name = terreno.getChunks()[x,y,z].ToString(); //le damos el nombre al GameObject
		mallaChunk.transform.parent = terreno.transformTerreno; //asignamos el padre de la malla chunk la transform del terreno
		mallaChunk.transform.position = new Vector3(x*Chunk.numBloquesEnX, y*Chunk.numBloquesEnY, z*Chunk.numBloquesEnZ);
		mallaChunk.setChunk(terreno.getChunks()[x,y,z]); //asinamos el chunk a su malla
		terreno.crearMallaChunk(x, y, z, mallaChunk); //crear la malla del chunk 		
	}
	
	#endregion
	
	
	
	#region Unity
	public void Start(){
		//generar(); //generamos el terreno en el inicio
		var a = System.Diagnostics.Stopwatch.StartNew();
		a.Start();
		generarTerrenoAleatorio();
		a.Stop();
		Debug.Log (a.ElapsedMilliseconds);
	}
	
	#endregion
}
