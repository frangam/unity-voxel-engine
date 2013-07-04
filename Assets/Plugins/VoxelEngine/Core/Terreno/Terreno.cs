using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Terreno {
	
	/// <summary>
	/// La Transform del GameObject Terreno
	/// </summary>
	public Transform transformTerreno;
	
	private int numChunksVisiblesEnX;
 	private int numChunksVisiblesEnY;
 	private int numChunksVisiblesEnZ;
	
	private int numTotalBloquesEnX;
	private int numTotalBloquesEnY;
	private int numTotalBloquesEnZ;
	
	private int nivelAgua;
	
	
	#region estaticas
	public static int totalChunksX;
	public static int totalChunksY;
	public static int totalChunksZ;
	
	public static int totalBloquesX;
	public static int totalBloquesY;
	public static int totalBloquesZ;
	
	public static int nivelDelAgua = 3;
	#endregion
	
	/// <summary>
	/// Las porciones del terreno
	/// </summary>
	public static Chunk[,,] chunks;
	
	/// <summary>
	/// camino que se va rellenando con agua (true: contiene agua, false: no la contiene)
	/// </summary>
	public static bool[,,] caminoAgua;

	
//	public Terreno()
//	{
//		chunks = new Chunk[numChunksVisiblesEnX,numChunksVisiblesEnY,numChunksVisiblesEnZ];
//		inicializarChunks();
//	}
	
	public Terreno(int numChunkX, int numChunkY, int numChunkZ, int _nivelDelAgua)
	{
		numChunksVisiblesEnX = numChunkX;
		numChunksVisiblesEnY = numChunkY;
		numChunksVisiblesEnZ = numChunkZ;
		
		chunks = new Chunk[numChunkX, numChunkY, numChunkZ];
		inicializarChunks();
		
		//inicializamos el nivel del agua
		this.nivelAgua = _nivelDelAgua;
		nivelDelAgua = Mathf.Clamp(_nivelDelAgua, 0, numTotalBloquesEnY);
		
		caminoAgua = new bool[totalBloquesX, totalBloquesY, totalBloquesZ]; //inicializo camino de agua; 
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
	
	public Chunk[,,] getChunks()
	{
		return chunks;
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
									|| (xTerreno >= totalBloquesX || yTerreno >= totalBloquesY || zTerreno >= totalBloquesZ);
		
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
	
//	/// <summary>
//	/// Modifica un bloque en coordenadas del terreno, para ello debe obtener primero el bloque
//	/// en coordenadas del terreno tambien.
//	/// </summary>
//	/// <param name='bloque'>
//	/// El nuevo bloque
//	/// </param>
//	/// <param name='xTerreno'>
//	/// X terreno.
//	/// </param>
//	/// <param name='yTerreno'>
//	/// Y terreno.
//	/// </param>
//	/// <param name='zTerreno'>
//	/// Z terreno.
//	/// </param>
//	public void setBloque(Bloque bloque, int xTerreno,int yTerreno, int zTerreno){
//		// primero calculamos las coordenadas del chunk segun las coordenadas del Terreno
//		int xChunk = (xTerreno / Chunk.numBloquesEnX);
//		int yChunk = (yTerreno / Chunk.numBloquesEnY);
//		int zChunk = (zTerreno / Chunk.numBloquesEnZ);
//		
//        // comprobamos que las coordenadas del chunk no se salgan del rango
//        bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= totalChunksX
//									|| yChunk >= totalChunksY || zChunk >= totalChunksZ);
//        //si las coordenadas del chunk no se salen de rango
//		if(!coordChunkFueraRango){
//			//calcula las coordenadas de bloque relativas al origen del chunk
//	        int xBloque = xTerreno % Chunk.numBloquesEnX;
//	        int yBloque = yTerreno % Chunk.numBloquesEnY;
//	        int zBloque = zTerreno % Chunk.numBloquesEnZ;
//			
//			chunks[xChunk, yChunk, zChunk].setBloque(bloque, xBloque, yBloque, zBloque);
////			Debug.Log ("Bloque seleccionado: "+xBloque+","+yBloque+","+zBloque);
//		}
//		
//	}
	
//	/// <summary>
//	/// Devuelve el Chunk de una coordenada del terreno.
//	/// </summary>
//	/// <returns>
//	/// El Chunk
//	/// </returns>
//	/// <param name='xTerreno'>
//	/// X terreno.
//	/// </param>
//	/// <param name='yTerreno'>
//	/// Y terreno.
//	/// </param>
//	/// <param name='zTerreno'>
//	/// Z terreno.
//	/// </param>
//	public Chunk getChunkPos(Bloque bloque)
//	{
//		Chunk chunk = null;
//		int xChunk = bloque.getXTerreno() / Chunk.numBloquesEnX;
//		int yChunk = bloque.getYTerreno() / Chunk.numBloquesEnY;
//		int zChunk = bloque.getZTerreno() / Chunk.numBloquesEnZ;
//		
//	  	bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= totalChunksX
//									|| yChunk >= totalChunksY || zChunk >= totalChunksZ);
//		if(!coordChunkFueraRango){
//			chunk = chunks[xChunk, yChunk, zChunk];	
//		}
//		
//		return chunk;
//	}
	
	public IEnumerator ActualizarChunk(Bloque bloque, bool async = false)
	{
		
		
//		Chunk ChunkDelBloque = getChunkPos(bloque);
		Chunk ChunkDelBloque = Chunks.getChunkQueContieneAlBloque(bloque);
		ChunkDelBloque.seHaModificadoElChunk();
		refrescarChunkVecinos(ChunkDelBloque, bloque);
		
		yield return null;
		
	}

	public void refrescarChunkVecinos (Chunk chunk, Bloque bloque)
	{	
		List<Bloque> vecinos = new List<Bloque>();

		int xBloque = bloque.getXTerreno() % Chunk.numBloquesEnX;
       	int yBloque = bloque.getYTerreno() % Chunk.numBloquesEnY;
        int zBloque = bloque.getZTerreno() % Chunk.numBloquesEnZ;
		
		Bloque izquierda = chunk.getBloque(xBloque + 1, yBloque, zBloque);
		Bloque arriba = chunk.getBloque(xBloque, yBloque + 1, zBloque);
		Bloque abajo = chunk.getBloque(xBloque, yBloque - 1, zBloque);
		Bloque frente = chunk.getBloque(xBloque, yBloque, zBloque +1);
		Bloque atras = chunk.getBloque(xBloque, yBloque, zBloque - 1);
		Bloque derecha = chunk.getBloque(xBloque - 1, yBloque, zBloque);
		
		vecinos.Add(izquierda);
		vecinos.Add(arriba);
		vecinos.Add(abajo);
		vecinos.Add(frente);
		vecinos.Add(atras);
		vecinos.Add(derecha);
		
		foreach(Bloque b in vecinos)
		{
			if(b.esDibujable())
			{
				Chunk c = null;
//				c = getChunkPos(b);
				c = Chunks.getChunkQueContieneAlBloque(b); //obtenemos el chunk al que pertenece el bloque
				if ( !chunk.Equals(c))
					c.seHaModificadoElChunk();
			}
		}

	}
	
//	public void ActualizarChunkPorPosicion(Vector3 chunkPos)
//	{
//		int x = (int)chunkPos.x;
//		int y = (int)chunkPos.y;
//		int z = (int)chunkPos.z;
//		
//		if ( (x >= 0 && x < numChunksVisiblesEnX) && (y >= 0 && y < numChunksVisiblesEnY) && (z >= 0 && z < numChunksVisiblesEnZ) )
//		{
//			chunks[x,y,z].seHaModificadoElChunk();
//		}
//	}
	
//	/// <summary>
//	/// Obtiene la posicion del bloque respecto al chunk, segun las coordenadas del terreno que tiene el bloque
//	/// </summary>
//	/// <returns>
//	/// Devuelve la posicion del bloque respecto a coordenadas del chunk
//	/// </returns>
//	/// <param name='bloque'>
//	/// El bloque
//	/// </param>
//	public Vector3 posicionBloqueRespectoChunk(Bloque bloque)
//	{
//		Vector3 res = Vector3.zero;
//		
//		res.x = bloque.getXTerreno() % numChunksVisiblesEnX;
//		res.y = bloque.getYTerreno() % numChunksVisiblesEnY;
//		res.z = bloque.getZTerreno() % numChunksVisiblesEnZ;
//	
//		return res;
//	}
	#endregion
	
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
