using UnityEngine;
using System.Collections;

public static class Chunks{
	/// <summary>
	/// Obtiene el chunk que contiene al bloque indicado
	/// </summary>
	/// <returns>
	/// El chunk
	/// </returns>
	/// <param name='bloque'>
	/// El bloque que pertenece al chunk que se quiere obtener
	/// </param>
	public static Chunk getChunkQueContieneAlBloque(Bloque bloque){
		return getChunkEnCoordsTerreno(new Vector3i(bloque.getXTerreno(), bloque.getYTerreno(), bloque.getZTerreno()));
	}
	
	/// <summary>
	/// Obtiene el chunk localizado en las coordenadas del terreno
	/// </summary>
	/// <returns>
	/// El chunk
	/// </returns>
	/// <param name='xTerreno'>
	/// X terreno.
	/// </param>
	/// <param name='yTerreno'>
	/// Y terreno.
	/// </param>
	/// <param name='zTerreno'>
	/// Z terreno.
	/// </param>
	public static Chunk getChunkEnCoordsTerreno(int xTerreno, int yTerreno, int zTerreno){
		return getChunkEnCoordsTerreno(new Vector3i(xTerreno, yTerreno, zTerreno));
	}
	
	/// <summary>
	/// Obtiene el chunk localizado en coordenadas del terreno especificadas
	/// </summary>
	/// <returns>
	/// The chunk
	/// </returns>
	/// <param name='posicionEnCoordsTerreno'>
	/// Las coordenadas del terreno
	/// </param>
	public static Chunk getChunkEnCoordsTerreno(Vector3i posicionEnCoordsTerreno){
		Chunk chunk = null;
		
		//comprobamos si las coordenadas del terreno no se salgan de rango
		//por lo que hay que comprobar que las coordenadas no sean inferiores o mayores/iguales que el numero total de bloques por lado
		bool coordTerrenoFueraRango = (posicionEnCoordsTerreno.x < 0 || posicionEnCoordsTerreno.y < 0 || posicionEnCoordsTerreno.z < 0) 
									|| (posicionEnCoordsTerreno.x >= Terreno.totalBloquesX || posicionEnCoordsTerreno.y >= Terreno.totalBloquesY || posicionEnCoordsTerreno.z >= Terreno.totalBloquesZ);
		
		//si las coordenadas del terreno se salen de rango devolvemos un bloque de limite de terreno
		if(!coordTerrenoFueraRango){ 
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
				chunk = Terreno.chunks[xChunk, yChunk, zChunk];
//		        chunk = GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.getChunks()[xChunk,yChunk,zChunk];
				
			}
		}
		
		return chunk;
	}
}

