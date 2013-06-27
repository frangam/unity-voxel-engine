using UnityEngine;
using System.Collections;

// <summary>
// 	Clase que representa los bloques que tendra el mapa.
// </summary>
 
public class Bloque : IBloque {

	private int _xTerreno = 0;
	private int _yTerreno = 0;
	private int _zTerreno = 0;
	private TipoBloque _tipo;
	private Chunk _chunk;
	
	/// <summary>
	/// 	Inicializamos una nueva instacia de la clase <see cref="Bloque"/>.
	/// </summary>
	/// <param name='tipo'>
	/// 	De que <see cref="TipoBloque"/> sera.
	/// </param>
	/// <param name='xTerreno'>
	/// 	Coordenada en el eje X del bloque.
	/// </param>
	/// <param name='yTerreno'>
	/// 	Coordenada en el eje X del bloque.
	/// </param>
	/// <param name='zTerreno'>
	/// 	Coordenada en el eje X del bloque.
	/// </param>
	public Bloque(TipoBloque tipo, int xTerreno, int yTerreno, int zTerreno)
	{
		this._xTerreno = xTerreno;
		this._yTerreno = yTerreno;
		this._zTerreno = zTerreno;
		this._tipo = tipo;
	}
	
	/// <summary>
	/// 	Variable para saber si un bloque sera visible o no.
	/// </summary>
	/// <returns>
	/// 	bool.
	/// </returns>
	public bool esDibujable()
	{
		return false;
	}
	
	/// <summary>
	/// 	Posicion UV de la cara inferior en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public Vector2 posicionUVTexturaCaraInferior()
	{
		return TiposBloques.getBloque(_tipo).posicionUVTexturaCaraInferior();
	}
	
	/// <summary>
	/// 	Posicion UV de la cara superior en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public Vector2 posicionUVTexturaCaraSuperior()
	{
		return TiposBloques.getBloque(_tipo).posicionUVTexturaCaraSuperior();
	}
	
	/// <summary>
	/// 	Posicion UV de las caras laterales en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public Vector2 posicionUVTexturaCaraLaterales()
	{
		return TiposBloques.getBloque(_tipo).posicionUVTexturaCaraLaterales();
	}
	
	/// <summary>
	/// 	Posicion UV de la cara cara en general en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public Vector2 posicionUVTexturaCara()
	{
		return TiposBloques.getBloque(_tipo).posicionUVTexturaCara();
	}
	/// <summary>
	/// 	Destruimos el bloque actual.
	/// </summary>
	/// <returns>
	/// 	bool.
	/// </returns>
	public bool destruirse()
	{
		return TiposBloques.getBloque(_tipo).destruirse(this);
	}
	
	/// <summary>
	/// 	Creamos un Bloque con un TipoBloqueCrearse the specified bloque and tipoBloque.
	/// </summary>
	/// <param name='tipoBloque'>
	/// 	De que TipoBloque se quiere crear el Bloque.
	/// </param>
	/// <returns>
	///  	bool.
	/// </returns>
	public bool crearse(TipoBloque tipoBloque)
	{
		return TiposBloques.getBloque(_tipo).crearse(this,tipoBloque);
	}
	
	/// <summary>
	/// 	Devuelve el TipoBloque del Bloque.
	/// </summary>
	/// <returns>
	///  	TipoBloque.
	/// </returns>
	public TipoBloque getTipo() {
		return _tipo; 
	} 
	/// <summary>
	/// 	Cambia el TipoBloque del Bloque.
	/// </summary>
	/// <param name='tipo'>
	/// 	TipoBloque nuevo.
	/// </param>
	public void setTipo(TipoBloque tipo) { 
		this._tipo = tipo; 
	}
	
	/// <summary>
	/// 	Devuelve el Chunk.
	/// </summary>
	/// <returns>
	/// 	Chunk.
	/// </returns>
	public Chunk getChunk() { 
		return _chunk; 
	} 
	/// <summary>
	/// 	Cambia el Chunk al que pertenece el Bloque.
	/// </summary>
	/// <param name='chunk'>
	/// 	Chunk nuevo.
	/// </param>
	public void setChunk(Chunk chunk) { 
		this._chunk = chunk; 
	} 
}
