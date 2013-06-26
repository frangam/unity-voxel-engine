using UnityEngine;
using System.Collections;

public class Chunk {
	
	public const int numBloquesEnX = 10;
	public const int numBloquesEnY = 4;
	public const int numBloquesEnZ = 10;
	
	private int xTerreno = 0;
	private int yTerreno = 0;
	private int zTerreno = 0;
	
	private Bloque[,,] bloques;
	
	private MallaChunk malla;
	
	public Chunk(int xTerreno, int yTerreno, int zTerreno)
	{
		this.xTerreno = xTerreno;
		this.yTerreno = yTerreno;
		this.zTerreno = zTerreno;
		bloques = new Bloque(xTerreno, yTerreno, zTerreno);
	}
	
	public MallaChunk Malla { get { return malla; } set { malla = value; } }
	
	public void actualizarMalla()
	{
		
	}
	
	public Bloque getBloque(int xBloque, int yBloque, int zBloque)
	{
		return bloques[xBloque, yBloque, zBloque];
	} 
	public void setBloque()
	{
		
	}
}
