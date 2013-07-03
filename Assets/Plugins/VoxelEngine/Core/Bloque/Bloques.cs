using UnityEngine;
using System.Collections;

public static class Bloques{
	/// <summary>
	/// Obtiene un bloque en coordenadas del terreno
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
		        Chunk chunk = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.getChunks()[xChunk,yChunk,zChunk];
		
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
}


