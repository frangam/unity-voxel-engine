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
	
	private Chunk[,,] chunks;
	
	public Terreno()
	{
		chunks = new Chunk[numChunksVisiblesEnX,numChunksVisiblesEnY,numChunksVisiblesEnZ];
	}
	
	public Terreno(int numChunkX, int numChunkY, int numChunkZ)
	{
		numChunksVisiblesEnX = numChunkX;
		numChunksVisiblesEnY = numChunkY;
		numChunksVisiblesEnZ = numChunkZ;
		chunks = new Chunk[numChunkX, numChunkY, numChunkZ];
	}
	
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
	
	public Chunk[,,] getChunks()
	{
		return chunks;
	}
	
	public void inicializarTerreno(){
		chunks = new Chunk[numChunksVisiblesEnX,numChunksVisiblesEnY,numChunksVisiblesEnZ];
	}
	
	
	public void inicializarChunks(int xTerreno, int yTerreno, int zTerreno){
		for(int x=0; x<Chunk.numBloquesEnX; x++){
			for(int y=0; y<Chunk.numBloquesEnY; y++){
				for(int z=0; z<Chunk.numBloquesEnZ; z++){
					chunks[x,y,z].setBloque(new Bloque(TipoBloque.AGUA, xTerreno, yTerreno, zTerreno), x, y, z);
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
