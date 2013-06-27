using UnityEngine;
using System.Collections;

public class Bloque : IBloque {
	
	private int xTerreno = 0;
	private int yTerreno = 0;
	private int zTerreno = 0;
	private TipoBloque tipo;
	private Chunk chunk;
	
	public Bloque(TipoBloque tipo, int xTerreno, int yTerreno, int zTerreno)
	{
		this.xTerreno = xTerreno;
		this.xTerreno = xTerreno;
		this.xTerreno = xTerreno;
		this.tipo = tipo;
	}
	
	public bool esDibujable()
	{
		return false;
	}
	public Vector2 posicionUVTexturaCaraInferior()
	{
		return TiposBloques.getBloque(tipo).posicionUVTexturaCaraInferior();
	}
	public Vector2 posicionUVTexturaCaraSuperior()
	{
		return TiposBloques.getBloque(tipo).posicionUVTexturaCaraSuperior();
	}
	public Vector2 posicionUVTexturaCaraLaterales()
	{
		return TiposBloques.getBloque(tipo).posicionUVTexturaCaraLaterales();
	}
	public Vector2 posicionUVTexturaCara()
	{
		return TiposBloques.getBloque(tipo).posicionUVTexturaCara();
	}
	public bool destruirse(Bloque bloque)
	{
		return TiposBloques.getBloque(tipo).destruirse(this);
	}
	public bool crearse(Bloque bloque, TipoBloque tipoBloque)
	{
		return TiposBloques.getBloque(tipo).crearse(this,tipoBloque);
	}
	public TipoBloque getTipo() {
		return tipo; 
	} 
	public void setTipo(TipoBloque tipo) { 
		this.tipo = tipo; 
	}
	public Chunk getChunk() { 
		return chunk; 
	} 
	public void setChunk(Chunk chunk) { 
		this.chunk = chunk; 
	} 
}
