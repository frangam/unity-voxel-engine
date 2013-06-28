using UnityEngine;
using System.Collections;

[System.Serializable]
public class Terreno {
	
	/// <summary>
	/// La Transform del GameObject Terreno
	/// </summary>
	public Transform transformTerreno;
	
	[SerializeField] private int numChunksVisiblesEnX = 5;
	[SerializeField] private int numChunksVisiblesEnY = 1;
	[SerializeField] private int numChunksVisiblesEnZ = 5;
	
	private int numTotalBloquesEnX;
	private int numTotalBloquesEnY;
	private int numTotalBloquesEnZ;
	
	
	#region estaticas
	public static int totalChunksX;
	public static int totalChunksY;
	public static int totalChunksZ;
	
	public static int totalBloquesX;
	public static int totalBloquesY;
	public static int totalBloquesZ;
	#endregion
	
	
	private static Chunk[,,] chunks;
	
	public Terreno()
	{
		chunks = new Chunk[numChunksVisiblesEnX,numChunksVisiblesEnY,numChunksVisiblesEnZ];
		inicializarChunks();
	}
	
	public Terreno(int numChunkX, int numChunkY, int numChunkZ)
	{
		numChunksVisiblesEnX = numChunkX;
		numChunksVisiblesEnY = numChunkY;
		numChunksVisiblesEnZ = numChunkZ;
		chunks = new Chunk[numChunkX, numChunkY, numChunkZ];
		inicializarChunks();
	}
	
	
	#region getters
	public int getNumChunksVisiblesEnX()
	{
		return numChunksVisiblesEnX;
	}
	
	public int getNumChunksVisiblesEnY()
	{
		return numChunksVisiblesEnY;
	}
	
	public int getNumChunksVisiblesEnZ()
	{
		return numChunksVisiblesEnZ;
	}
	public int getNumTotalBloquesEnX(){
		return numTotalBloquesEnX;
	}
	public int getNumTotalBloquesEnY(){
		return numTotalBloquesEnY;
	}
	public int getNumTotalBloquesEnZ(){
		return numTotalBloquesEnZ;
	}
	/// <summary>
	/// Obtiene un bloque en coordenadas del terreno
	/// </summary>
	/// <returns>
	/// el bloque.
	/// </returns>
	/// <param name='xTerreno'>
	/// X terreno.
	/// </param>
	/// <param name='yTerreno'>
	/// Y terreno.
	/// </param>
	/// <param name='zTerreno'>
	/// Z terreno.
	/// </param>
	public static Bloque getBloque(int xTerreno,int yTerreno, int zTerreno){
		Bloque bloque = new Bloque(TipoBloque.DESCONOCIDO);
		
		//comprobamos si las coordenadas del terreno no se salgan de rango
		//por lo que hay que comprobar que las coordenadas no sean inferiores o mayores/iguales que el numero total de bloques por lado
		bool coordTerrenoFueraRango = (xTerreno < 0 || yTerreno < 0 || zTerreno < 0) 
									|| (xTerreno >= totalBloquesX || yTerreno >= totalBloquesZ || zTerreno >= totalBloquesZ);
		
		//si las coordenadas del terreno se salen de rango devolvemos un bloque de limite de terreno
		if(coordTerrenoFueraRango){ 
			bloque = new Bloque(TipoBloque.LIMITE_TERRENO);
		}
		else{
	        // primero calculamos las coordenadas del chunk segun las coordenadas del Terreno
			int xChunk = (xTerreno / Chunk.numBloquesEnX);
			int yChunk = (yTerreno / Chunk.numBloquesEnY);
			int zChunk = (zTerreno / Chunk.numBloquesEnZ);
			
	        // comprobamos que las coordenadas del chunk no se salgan del rango, si se salen devolvemos un bloque desconocido
	        bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= totalChunksX
										|| yChunk >= totalChunksY || zChunk >= totalChunksZ);
	        //si las coordenadas del chunk no se salen de rango
			if(!coordChunkFueraRango){
		        Chunk chunk = chunks[xChunk,yChunk,zChunk]; //ahora si obtenemos el chunk segun las coordenas de chunk
		
				//calcula las coordenadas de bloque relativas al origen del chunk
		        int xBloque = xTerreno % Chunk.numBloquesEnX;
		        int yBloque = yTerreno % Chunk.numBloquesEnY;
		        int zBloque = zTerreno % Chunk.numBloquesEnZ;
		
				//por ultimo, obtenemos el bloque del chunk al que pertenece
		        bloque = chunk.getBloque(xBloque, yBloque, zBloque);
			}
		}
		
		return bloque;
	}
	/// <summary>
	/// Modifica un bloque en coordenadas del terreno, para ello debe obtener primero el bloque
	/// en coordenadas del terreno tambien.
	/// </summary>
	/// <param name='bloque'>
	/// El nuevo bloque
	/// </param>
	/// <param name='xTerreno'>
	/// X terreno.
	/// </param>
	/// <param name='yTerreno'>
	/// Y terreno.
	/// </param>
	/// <param name='zTerreno'>
	/// Z terreno.
	/// </param>
	public void setBloque(Bloque bloque, int xTerreno,int yTerreno, int zTerreno){
		// primero calculamos las coordenadas del chunk segun las coordenadas del Terreno
		int xChunk = (xTerreno / Chunk.numBloquesEnX);
		int yChunk = (yTerreno / Chunk.numBloquesEnY);
		int zChunk = (zTerreno / Chunk.numBloquesEnZ);
		
        // comprobamos que las coordenadas del chunk no se salgan del rango
        bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= totalChunksX
									|| yChunk >= totalChunksY || zChunk >= totalChunksZ);
        //si las coordenadas del chunk no se salen de rango
		if(!coordChunkFueraRango){
			//calcula las coordenadas de bloque relativas al origen del chunk
	        int xBloque = xTerreno % Chunk.numBloquesEnX;
	        int yBloque = yTerreno % Chunk.numBloquesEnY;
	        int zBloque = zTerreno % Chunk.numBloquesEnZ;
			
			chunks[xChunk, yChunk, zChunk].setBloque(bloque, xBloque, yBloque, zBloque);
		}
		
	}
	#endregion
	
	
	
	public Chunk[,,] getChunks()
	{
		return chunks;
	}
	
	/// <summary>
	/// Instancia todos los chunks del terreno	
	/// </summary>
	public void inicializarChunks(){	
		numTotalBloquesEnX = numChunksVisiblesEnX * Chunk.numBloquesEnX;
		numTotalBloquesEnY = numChunksVisiblesEnY * Chunk.numBloquesEnY;
		numTotalBloquesEnZ = numChunksVisiblesEnZ * Chunk.numBloquesEnZ;
		
		//inicializamos las variables estaticas
		totalChunksX = numChunksVisiblesEnX;
		totalChunksY = numChunksVisiblesEnY;
		totalChunksZ = numChunksVisiblesEnZ;
		totalBloquesX = numTotalBloquesEnX;
		totalBloquesY = numTotalBloquesEnY;
		totalBloquesZ = numTotalBloquesEnZ;
		
		for(int x = 0; x < numChunksVisiblesEnX; x++){
			for(int y = 0; y < numChunksVisiblesEnY; y++){
				for(int z = 0; z < numChunksVisiblesEnZ; z++){
					chunks[x,y,z] = new Chunk(x, y, z);
				}
			}
		}
	}
	
	public void crearMallaChunk(int xTerreno, int yTerreno, int zTerreno, MallaChunk mallaChunk){
		mallaChunk.getMalla().mesh = ChunkRenderer.renderizar(chunks[xTerreno, yTerreno, zTerreno]);
		mallaChunk.getCollider().sharedMesh = mallaChunk.getMalla().mesh; //asignamos al collider la malla del chunk para que la ocupe
		chunks[xTerreno, yTerreno, zTerreno].setMalla(mallaChunk); //creamos la malla chunk
	}

}
