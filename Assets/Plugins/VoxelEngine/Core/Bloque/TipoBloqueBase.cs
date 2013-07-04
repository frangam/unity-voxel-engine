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
		bool res = false;
		
		//se puede construir en vacio o en agua
		if(bloque.getTipo() == TipoBloque.AGUA || bloque.getTipo() == TipoBloque.VACIO){
			bloque.setTipo(tipo); 
			res = true;
		}
		
		return res;
	}
	
	
	/// <summary>
	/// Comprueba si al menos existe un bloque vecino(en horizontal y vertical, se excluye en diagonal)
	/// al que se ha pulsado que se corresponda con el tipo de bloque que es pasado como parámetros
	/// </summary>
	/// <returns>
	/// True si existe almenos un vecino que verifica la condicion del tipo de bloque
	/// </returns>
	/// <param name='bloque'>
	/// El bloque
	/// </param>
	/// <param name='TipoBloque'>
	/// If set to <c>true</c> tipo bloque.
	/// </param>
	public virtual bool existeAlgunBloqueVecinoDelTipoEspecificado(Bloque bloque, TipoBloque TipoBloque){
		bool existe = false; //flag
		
		
//		
//		// Comprobamos si hay agua en el eje Z
//		for (int i = _k - 1; i <= _k + 1 && !existe; i++) {
//			//controlamos los bordes
//			if (i > -1 && i < Configuracion.NUM_TILES_HORIZONTAL + 1) {				
//				//la concicicion para que exista un vecino que verifica el tipo de tile pasado como parametro 
//				existe = MapGenerator.worldData[_i, _j, i].GetComponent<Tile>().type == tipo;
//			}
//		}
//		// Comprobamos si hay agua en el eje X
//		for (int j = _i - 1; j <= _i + 1 && !existe; j++) {
//			if (j > -1 && j < Configuracion.NUM_TILES_VERTICAL + 1){
//				//la concicicion para que exista un vecino que verifica el tipo de tile pasado como parametro
//				existe = MapGenerator.worldData[j, _j, _k].GetComponent<Tile>().type == tipo;
//			}	
//		}
		
		return existe;
	}
}
