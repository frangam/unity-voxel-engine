using UnityEngine;
using System.Collections;


// <summary>
// 		Clase estatica para accceder a todos los tipos de Bloques que hay en el juego.
// </summary>
public static class TiposBloques {
	
	/// <summary>
	/// 	Array con los objectos de TipoBloque que habra en el juego.
	/// 
	/// 	En orden con el enum de TipoBloque
	/// </summary>
	public static TipoBloqueBase[] tipos = new TipoBloqueBase[] {
		
		
		new BloqueDesconocido(),
		new BloqueLimiteTerreno(),
		new BloqueSuelo(),
		new BloqueProximoASuelo(),
		new BloqueAgua(),
		new BloqueHierba(),
		new BloquePiedra(),
		new BloqueTierra(),
		new BloqueVacio(),
		
	};
	/// <summary>
	/// 	Devuelve un BloqueTipoBosa apartir del TipoBloque que se le pase.
	/// </summary>
	/// <returns>
	/// 	TipoBloqueBase.
	/// </returns>
	/// <param name='tipoBloque'>
	/// 	TipoBloque que se quiere conseguir.
	/// </param>
	public static TipoBloqueBase getBloque(TipoBloque tipoBloque)
	{
		return tipos[(int)tipoBloque];
	}
}
