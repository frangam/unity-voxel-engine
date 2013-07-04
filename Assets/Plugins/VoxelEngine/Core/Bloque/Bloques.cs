using UnityEngine;
using System.Collections;

public static class Bloques{
	#region getters de bloques segun coordenadas del terreno
	/// <summary>
	/// Obtiene el bloque localizado en las coordenadas del terreno especificadas
	/// </summary>
	/// <returns>
	/// The bloque en coords terreno.
	/// </returns>
	/// <param name='xTerreno'>
	/// X terreno.
	/// </param>
	/// <param name='yTerreno'>
	/// Y terreno.
	/// </param>
	/// <param name='zTerren'>
	/// Z terren.
	/// </param>
	public static Bloque getBloqueEnCoordsTerreno(int xTerreno, int yTerreno, int zTerreno){
		return getBloqueEnCoordsTerreno(new Vector3i(xTerreno, yTerreno, zTerreno));	
	}
	
	/// <summary>
	/// Obtiene el bloque localizado en las coordenadas del terreno especificadas
	/// </summary>
	/// <returns>
	/// el bloque.
	/// </returns>
	/// <param name='posicionEnCoordsTerreno'>
	/// Las coordenadas del terreno
	/// </param>
	public static Bloque getBloqueEnCoordsTerreno(Vector3i posicionEnCoordsTerreno){
		Bloque bloque = new Bloque(TipoBloque.DESCONOCIDO);
		
		//comprobamos si las coordenadas del terreno no se salgan de rango
		//por lo que hay que comprobar que las coordenadas no sean inferiores o mayores/iguales que el numero total de bloques por lado
		bool coordTerrenoFueraRango = (posicionEnCoordsTerreno.x < 0 || posicionEnCoordsTerreno.y < 0 || posicionEnCoordsTerreno.z < 0) 
									|| (posicionEnCoordsTerreno.x >= Terreno.totalBloquesX || posicionEnCoordsTerreno.y >= Terreno.totalBloquesY || posicionEnCoordsTerreno.z >= Terreno.totalBloquesZ);
		
		//si las coordenadas del terreno se salen de rango devolvemos un bloque de limite de terreno
		if(coordTerrenoFueraRango){ 
			bloque = new Bloque(TipoBloque.LIMITE_TERRENO);
		}
		else{
	        // primero calculamos las coordenadas del chunk segun las coordenadas del Terreno
			int xChunk = (posicionEnCoordsTerreno.x / Chunk.numBloquesEnX);
			int yChunk = (posicionEnCoordsTerreno.y / Chunk.numBloquesEnY);
			int zChunk = (posicionEnCoordsTerreno.z / Chunk.numBloquesEnZ);
			
	        // comprobamos que las coordenadas del chunk no se salgan del rango, si se salen devolvemos un bloque desconocido
	        bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= Terreno.totalChunksX
										|| yChunk >= Terreno.totalChunksY || zChunk >= Terreno.totalChunksZ);
	        //si las coordenadas del chunk no se salen de rango
			if(!coordChunkFueraRango){
				//ahora si obtenemos el chunk segun las coordenas de chunk respecto del terreno
				Chunk chunk = Terreno.chunks[xChunk, yChunk, zChunk];
				
//		        Chunk chunk = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.getChunks()[xChunk,yChunk,zChunk];
		
				//calcula las coordenadas de bloque relativas al origen del chunk
		        int xBloque = posicionEnCoordsTerreno.x % Chunk.numBloquesEnX;
		        int yBloque = posicionEnCoordsTerreno.y % Chunk.numBloquesEnY;
		        int zBloque = posicionEnCoordsTerreno.z % Chunk.numBloquesEnZ;
		
				//por ultimo, obtenemos el bloque del chunk al que pertenece
		        bloque = chunk.getBloque(xBloque, yBloque, zBloque);
			}
		}
		
		return bloque;
	}
	#endregion
	
	#region setters de bloques segun coordenadas del terreno
	/// <summary>
	/// Modifica el bloque localizado en las coordenadas del terreno especificadas con el bloque nuevo indicado
	/// </summary>
	/// <param name='bloque'>
	/// Bloque.
	/// </param>
	/// <param name='xTerreno'>
	/// X terreno.
	/// </param>
	/// <param name='yTerreno'>
	/// Y terreno.
	/// </param>
	/// <param name='zTerreno'>
	/// Z terreno.
	/// </param>
	public static void setBloqueEnCoordsTerreno(Bloque bloque, int xTerreno, int yTerreno, int zTerreno){
		setBloqueEnCoordsTerreno(bloque, new Vector3i(xTerreno, yTerreno, zTerreno));
	}
	
	/// <summary>
	/// Modifica el bloque localizado en las coordenadas del terreno especificadas con el bloque nuevo indicado
	/// </summary>
	/// <param name='bloque'>
	/// Bloque.
	/// </param>
	/// <param name='posicionEnCoordsTerreno'>
	/// Posicion en coords terreno.
	/// </param>
	public static void setBloqueEnCoordsTerreno(Bloque bloque, Vector3i posicionEnCoordsTerreno){
		// primero calculamos las coordenadas del chunk segun las coordenadas del Terreno
		int xChunk = (posicionEnCoordsTerreno.x / Chunk.numBloquesEnX);
		int yChunk = (posicionEnCoordsTerreno.y / Chunk.numBloquesEnY);
		int zChunk = (posicionEnCoordsTerreno.z / Chunk.numBloquesEnZ);
		
        // comprobamos que las coordenadas del chunk no se salgan del rango
        bool coordChunkFueraRango =  (xChunk < 0 || yChunk < 0 || zChunk < 0) || (xChunk >= Terreno.totalChunksX
									|| yChunk >= Terreno.totalChunksY || zChunk >= Terreno.totalChunksZ);
        //si las coordenadas del chunk no se salen de rango
		if(!coordChunkFueraRango){
			//calcula las coordenadas de bloque relativas al origen del chunk
	        int xBloque = posicionEnCoordsTerreno.x % Chunk.numBloquesEnX;
	        int yBloque = posicionEnCoordsTerreno.y % Chunk.numBloquesEnY;
	        int zBloque = posicionEnCoordsTerreno.z % Chunk.numBloquesEnZ;
			
			//obtenemos el chunk del terreno
			Chunk chunk = Terreno.chunks[xChunk, yChunk, zChunk];
			
//			Se tarda mas en generar el terreno si el chunk se obtiene asi:
//			Chunk chunk = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.getChunks()[xChunk, yChunk, zChunk];
			
			//modificamos el bloque con el nuevo bloque
			chunk.setBloque(bloque, xBloque, yBloque, zBloque);
		}
	}
	#endregion
	
	/// <summary>
	/// Comprueba si existe algun bloque vecino al indicado con el tipo indicado
	/// </summary>
	/// <returns>
	/// El bloque
	/// </returns>
	/// <param name='bloque'>
	/// <c>true</c> si existe algun bloque vecino que tenga el tipo indicado
	/// </param>
	/// <param name='tipoBloqueVecino'>
	/// El tipo de bloque que debe ser alguno de los vecinos
	/// </param>
	public static bool existeAlgunBloqueVecinoConElTipo(Bloque bloque, TipoBloque tipoBloqueVecino){
		bool existe = false; //flag
		
		//comprobamos por cada eje de coordenada si existe algun bloque vecido que tenga el tipo indicado
		existe = existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_X, bloque, tipoBloqueVecino) 
				|| existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_Z, bloque, tipoBloqueVecino);
//				|| existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_Y, bloque, tipoBloqueVecino);
		
		return existe;
	}
	
	private static bool existeAlgunVecinoEnCoordenadaEje(EjeCoordenada eje, Bloque bloque, TipoBloque tipoBloqueVecino){
		bool existe = false; //flag
		
		//seleccionamos el limite segun el eje de coordenada
		switch(eje){
			case EjeCoordenada.EJE_X: limite = Chunk.numBloquesEnX; break;
//			case EjeCoordenada.EJE_Y: limite = Chunk.numBloquesEnY; break;
			case EjeCoordenada.EJE_Z: limite = Chunk.numBloquesEnZ; break;
		}
		
		//recorremos todos los bloques en el eje de coordenada indicado
		//i: coordenada en el eje indicado del bloque vecino que recorremos en dicho eje
		for (int i = coordenada - 1; i <= coordenada + 1 && !existe; i++) {
			if (i > -1 && i < limite + 1){
				Vector3i coordsTerrenoVecino = new Vector3i(); //coordenadas del terreno del bloque vecino
				
				//segun el eje, obtenemos las coordenadas del terreno donde buscar al bloque vecino y 
				//comprobar si su tipo es del tipo indicado
				switch(eje){
					case EjeCoordenada.EJE_X: coordsTerrenoVecino = new Vector3i(i, bloque.getYTerreno(), bloque.getZTerreno()); break;
//					case EjeCoordenada.EJE_Y: coordsTerrenoVecino = new Vector3i(bloque.getXTerreno(), i, bloque.getZTerreno()); break;
					case EjeCoordenada.EJE_Z: coordsTerrenoVecino = new Vector3i(bloque.getXTerreno(), bloque.getYTerreno(), i); break;
				}
				
				//la concicicion para que exista un vecino que verifica el tipo de tile pasado como parametro
				existe = Bloques.getBloqueEnCoordsTerreno(coordsTerrenoVecino).getTipo() == tipoBloqueVecino;
			}	
		}
		
		return existe;
	}
}


