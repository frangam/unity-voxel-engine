using UnityEngine;
using System.Collections;

// <summary>
// 	Representa un trozo de terreno que tiene un numero concreto de bloques en (x,y,z)
// </summary>
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
	
	private int _xTerreno = 0;
	private int _yTerreno = 0;
	private int _zTerreno = 0;
	
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
		this._xTerreno = xTerreno * numBloquesEnX;
		this._yTerreno = yTerreno * numBloquesEnY;
		this._zTerreno = zTerreno * numBloquesEnZ;
		_bloques = new Bloque[numBloquesEnX, numBloquesEnY, numBloquesEnZ];
	}
	
	/// <summary>
	/// 	Devuelve el numero de bloques que hay en el eje X.
	/// </summary>
	/// <returns>
	/// 	int.
	/// </returns>
	public int getNumBloquesEnX(){ return numBloquesEnX; }
	
	/// <summary>
	/// 	Devuelve el numero de bloques que hay en el eje Y.
	/// </summary>
	/// <returns>
	/// 	int.
	/// </returns>
	public int getNumBloquesEnY(){ return numBloquesEnY; }
	
	/// <summary>
	/// 	Devuelve el numero de bloques que hay en el eje Z.
	/// </summary>
	/// <returns>
	/// 	int.
	/// </returns>
	public int getNumBloquesEnZ(){ return numBloquesEnZ; }
	
	/// <summary>
	/// Devuelve la MallaChunk del Chunk.
	/// </summary>
	/// <returns>
	/// 	MallaChunk.
	/// </returns>
	public MallaChunk getMalla()
	{
		return _malla; 
	}
	
	/// <summary>
	/// 	Cambia la MallaChunk del Chunk.
	/// </summary>
	/// <param name='malla'>
	/// 	MallaChunk nueva.
	/// </param>
	public void setMalla(MallaChunk malla) 
	{ 
		_malla = malla; 
	}
	
	/// <summary>
	/// 	Metodo para actualizar el MeshFilter y MeshCollider del Chunk.
	/// </summary>
	public void actualizarMalla()
	{
		_malla.getMalla().mesh.Clear();
		//_malla.Malla.mesh = ChunkRender.Render(this);
		_malla.getCollider().sharedMesh = _malla.getMalla().mesh;
	}
	
	/// <summary>
	/// 	Devuelve un Bloque determinado del Chunk segun las coordenadoas pasadas.
	/// </summary>
	/// <returns>
	/// 	Bloque.
	/// </returns>
	/// <param name='xBloque'>
	/// 	Coordenada X del Bloque.
	/// </param>
	/// <param name='yBloque'>
	/// 	Coordenada Y del Bloque.
	/// </param>
	/// <param name='zBloque'>
	/// 	Coordenada Z del Bloque.
	/// </param>
	public Bloque getBloque(int xBloque, int yBloque, int zBloque)
	{
		if (xBloque >= 0 && xBloque < numBloquesEnX && yBloque >= 0 && yBloque < numBloquesEnY && zBloque >= 0 && zBloque < numBloquesEnX)
		{
			if (_bloques[xBloque,yBloque,zBloque] != null)
        		return _bloques[xBloque,yBloque,zBloque];
		}
		return null;
//		else
//		{
//			return _worldRef[_xTerreno+xBloque, _yTerreno+yBloque, _zTerreno+zBloque];
//		}
	}
	
	/// <summary>
	/// 	Modifica un determinado Bloque dentro del Chunk
	/// </summary>
	/// <param name='bloque'>
	/// 	Bloque nuevo.
	/// </param>
	/// <param name='xBloque'>
	/// 	Coordenada X del Bloque que se quiere cambiar.
	/// </param>
	/// <param name='yBloque'>
	///  	Coordenada Y del Bloque que se quiere cambiar.
	/// </param>
	/// <param name='zBloque'>
	///  	Coordenada Z del Bloque que se quiere cambiar.
	/// </param>
	public void setBloque(Bloque bloque, int xBloque, int yBloque, int zBloque)
	{
		if (xBloque < numBloquesEnX && yBloque < numBloquesEnY && zBloque < numBloquesEnZ)
		{
        	_bloques[xBloque,yBloque,zBloque] = bloque;
		}
	}
	
	/// <summary>
	/// 	Metodo para avisar desde el Chunk al MallaChunk que se ha modificado 
	/// 	y tiene que recalcularse la malla y el collider.
	/// </summary>
	public void seHaModificadoElChunk()
	{
		if(_malla != null)
			_malla.seHaModificadoElChunk();
	}

	public override string ToString()
	{
		return string.Format("Chunk ({1},{2},{3})", this._xTerreno, this._yTerreno, this._zTerreno);
	}
}
