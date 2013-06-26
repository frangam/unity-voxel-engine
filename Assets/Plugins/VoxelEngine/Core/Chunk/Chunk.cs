using UnityEngine;
using System.Collections;

public class Chunk {
	
	public const int numBloquesEnX = 10;
	public const int numBloquesEnY = 4;
	public const int numBloquesEnZ = 10;
	
	private int xTerreno = 0;
	private int yTerreno = 0;
	private int zTerreno = 0;
	
	private Bloque[,,] _bloques;
	private MallaChunk _malla;
	
	public Chunk(int xTerreno, int yTerreno, int zTerreno)
	{
		this.xTerreno = xTerreno;
		this.yTerreno = yTerreno;
		this.zTerreno = zTerreno;
		_bloques = new Bloque(xTerreno, yTerreno, zTerreno);
	}
	
	public int getNumBloquesEnX(){ return numBloquesEnX; }
	public int getNumBloquesEnY(){ return numBloquesEnY; }
	public int getNumBloquesEnZ(){ return numBloquesEnZ; }
	
	public MallaChunk Malla { get { return _malla; } set { _malla = value; } }
	
	public void actualizarMalla()
	{
		_malla.Malla.mesh.Clear();
		//_malla.Malla.mesh = ChunkRender.Render(this);
		_malla.collider.sharedMesh = _malla.Malla.mesh;
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
