using UnityEngine;
using System.Collections;

public class Terreno {

	[SerializeField] private const int numChunksVisiblesEnX = 5;
	[SerializeField] private const int numChunksVisiblesEnY = 1;
	[SerializeField] private const int numChunksVisiblesEnZ = 5;
	
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
		chunks = new Chunks[numChunkX, numChunkY, numChunkZ];
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
	
}
