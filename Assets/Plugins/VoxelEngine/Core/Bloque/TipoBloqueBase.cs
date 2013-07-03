using UnityEngine;
using System.Collections;

// <summary>
// 	TipoBloqueBase es una clase que representa a un tipo de bloque generico,
// 	de esta clase heredaran los bloques especificos comco AGUA, TIERRA, ETC.
// </summary>

public class TipoBloqueBase : IBloque {
	
	/// <summary>
	/// 	Variable para saber si un bloque sera visible o no.
	/// </summary>
	/// <returns>
	/// 	bool.
	/// </returns>
	public virtual bool esDibujable(){
		return false;
	}
	
	/// <summary>
	/// 	Posicion UV de la cara inferior en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public virtual Vector2 posicionUVTexturaCaraInferior()
	{
		return Vector2.zero;
	}
	
	/// <summary>
	/// 	Posicion UV de la cara superior en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public virtual Vector2 posicionUVTexturaCaraSuperior()
	{
		return Vector2.zero;
	}
	
	/// <summary>
	/// 	Posicion UV de las caras laterales en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public virtual Vector2 posicionUVTexturaCaraLaterales()
	{
		return Vector2.zero;
	}
	
	/// <summary>
	/// 	Posicion UV de la cara cara en general en la textura.
	/// </summary>
	/// <returns>
	/// 	Vector2.
	/// </returns>
	public virtual Vector2 posicionUVTexturaCara()
	{
		return Vector2.zero;
	}
	
	/// <summary>
	/// 	Destruimos el bloque actual.
	/// </summary>
	/// <returns>
	/// 	bool.
	/// </returns>
	public virtual bool destruirse(Bloque bloque)
	{
		if(bloque.getTipo() == TipoBloque.SUELO){
			return false;
		}
//		if(bloque.getChunk() != null){
//			bloque.getChunk().seHaModificadoElChunk();
//		}
		bloque.setTipo(TipoBloque.VACIO);
		return true;
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
	public virtual bool crearse(Bloque bloque, TipoBloque tipo)
	{
		if(bloque.getTipo() != TipoBloque.VACIO){ 
			return false;
		}
//		if(bloque.getChunk() != null){
//			bloque.getChunk().seHaModificadoElChunk();
//		}
		bloque.setTipo(tipo); 
		return true;
	}
}
