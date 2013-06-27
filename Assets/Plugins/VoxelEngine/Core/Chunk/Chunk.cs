using UnityEngine;
using System.Collections;

public class Chunk {
	
	/// <summary>
	/// 	Numero de Bloques en el Eje X.
	/// </summary>
	public const int numBloquesEnX = 10;
	/// <summary>
	/// 	Numero de Bloques en el Eje Y.
	/// </summary>
	public const int numBloquesEnY = 4;
	/// <summary>
	/// 	Numero de Bloques en el Eje Z.
	/// </summary>
	public const int numBloquesEnZ = 10;
	
	private int xTerreno = 0;
	private int yTerreno = 0;
	private int zTerreno = 0;
	
	private Bloque[,,] _bloques;
	private MallaChunk _malla;
	/// <summary>
	/// 	Inicializamos una nueva instancia de la clase <see cref="Chunk"/>.
	/// 	Se crea un nuevo Chunk en las coordenadas pasadas por parametros 
	/// 	y se crea un array[, ,] para contener a todos los bloques de ese
	/// 	Chunk.
	/// </summary>
	/// <param name='xTerreno'>
	/// 	Coordenada X donde estara el Chunk.
	/// </param>
	/// <param name='yTerreno'>
	/// 	Coordenada Y donde estara el Chunk.
	/// </param>
	/// <param name='zTerreno'>
	/// 	Coordenada Z donde estara el Chunk.
	/// </param>
	public Chunk(int xTerreno, int yTerreno, int zTerreno)
	{
		this.xTerreno = xTerreno * numBloquesEnX;
		this.yTerreno = yTerreno * numBloquesEnY;
		this.zTerreno = zTerreno * numBloquesEnZ;
		_bloques = new Bloque[numBloquesEnX, numBloquesEnY, numBloquesEnZ];
	}
	/// <summary>
	/// 	Devuelve el numero de bloques que hay en el eje X.
	/// </summary>
	/// <returns>
	/// The number bloques en x.
	/// </returns>
	public int getNumBloquesEnX(){ return numBloquesEnX; }
	public int getNumBloquesEnY(){ return numBloquesEnY; }
	public int getNumBloquesEnZ(){ return numBloquesEnZ; }
	
	public MallaChunk getMalla()
	{
		return _malla; 
	}
	public void setmalla(MallaChunk malla) 
	{ 
		_malla = malla; 
	}
	
	public void actualizarMalla()
	{
		_malla.getMalla().mesh.Clear();
		//_malla.Malla.mesh = ChunkRender.Render(this);
		_malla.getCollider().sharedMesh = _malla.getMalla().mesh;
	}
	
	public Bloque getBloque(int xBloque, int yBloque, int zBloque)
	{
		return _bloques[xBloque, yBloque, zBloque];
	} 
	public void setBloque(Bloque bloque, int xBloque, int yBloque, int zBloque)
	{
		_bloques[xBloque, yBloque, zBloque] = bloque;
	}
	
	public void seHaModificadoElChunk()
	{
		if(_malla != null)
			_malla.seHaModificadoElChunk();
	}
}
