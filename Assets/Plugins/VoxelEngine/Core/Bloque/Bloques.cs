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
		
		//comprobamos por cada eje de coordenada si existe algun bloque vecino que tenga el tipo indicado
		existe = existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_X, bloque, tipoBloqueVecino) 
				|| existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_Z, bloque, tipoBloqueVecino);
//				|| existeAlgunVecinoEnCoordenadaEje(EjeCoordenada.EJE_Y, bloque, tipoBloqueVecino);
		
		return existe;
	}
	
	private static bool existeAlgunVecinoEnCoordenadaEje(EjeCoordenada eje, Bloque bloque, TipoBloque tipoBloqueVecino){
		bool existe = false; //flag
		int limiteTerreno = 0;
		int coordBloqueEnElEje = bloque.getXTerreno(); //coordenada del bloque vecino segun el eje
		
		//seleccionamos el limite segun el eje de coordenada
		switch(eje){
			case EjeCoordenada.EJE_X: 
				limiteTerreno = Terreno.totalBloquesX;
				coordBloqueEnElEje = bloque.getXTerreno();
			break;
//			case EjeCoordenada.EJE_Y: 
//				limite = Terreno.totalBloquesY;
//				coordenada = bloque.getYTerreno();
//			break;
			case EjeCoordenada.EJE_Z: 
				limiteTerreno = Terreno.totalBloquesZ;
				coordBloqueEnElEje = bloque.getZTerreno();
			break;
		}
		
		//recorremos todos los bloques en el eje de coordenada indicado
		//i: coordenada del bloque vecino que recorremos en el eje indicado
		for (int coordVecinoEnElEje = coordBloqueEnElEje - 1; coordVecinoEnElEje <= coordBloqueEnElEje + 1 && !existe; coordVecinoEnElEje++) {
			//comprobamos los blosques que estan a los limites del terreno tambien
			if (coordVecinoEnElEje >= -1 && coordVecinoEnElEje < limiteTerreno + 1){
				Vector3i coordsTerrenoVecino = new Vector3i(); //coordenadas del terreno del bloque vecino
				
				//segun el eje, obtenemos las coordenadas del terreno donde buscar al bloque vecino y 
				//comprobar si su tipo es del tipo indicado
				switch(eje){
					case EjeCoordenada.EJE_X: coordsTerrenoVecino = new Vector3i(coordVecinoEnElEje, bloque.getYTerreno(), bloque.getZTerreno()); break;
//					case EjeCoordenada.EJE_Y: coordsTerrenoVecino = new Vector3i(bloque.getXTerreno(), coordVecinoEnElEje, bloque.getZTerreno()); break;
					case EjeCoordenada.EJE_Z: coordsTerrenoVecino = new Vector3i(bloque.getXTerreno(), bloque.getYTerreno(), coordVecinoEnElEje); break;
				}
				
				//caso especial: tipo del vecino sea agua y que la coordenada y del bloque no esta por encima del nivel del mar 
				if(tipoBloqueVecino == TipoBloque.AGUA && bloque.getYTerreno() < Terreno.nivelDelAgua){
					TipoBloque tipoVecinoRecorrido = Bloques.getBloqueEnCoordsTerreno(coordsTerrenoVecino).getTipo();
				 	existe = tipoVecinoRecorrido == TipoBloque.AGUA
							|| tipoVecinoRecorrido == TipoBloque.LIMITE_TERRENO; //>> tambien comprobamos que el bloque devuelto es de tipo limite (consideramos que hay agua al rededor del terreno)
				}
				else{ //caso general
					//la concicicion para que exista un vecino que verifica el tipo de tile pasado como parametro
					existe = Bloques.getBloqueEnCoordsTerreno(coordsTerrenoVecino).getTipo() == tipoBloqueVecino;
				}
			}	
		}
		
		return existe;
	}
	
	#region propagacion de agua por bloques vacios
	/// <summary>
	/// Deja que pase el agua por el bloque si es vacio
	/// </summary>
	/// <returns>
	/// True si se ha dejado pasar el agua
	/// </returns>
	/// <param name='bloque'>
	/// El bloque por el que se tiene que dejar pasar el agua si es vacio
	/// </param>
	public static bool dejarPasarElAgua(Bloque bloque){
		bool haPasadoElAgua = false;
		
		//si es de tipo vacio
		if(bloque.getTipo() == TipoBloque.VACIO){
			bloque.setTipo(TipoBloque.AGUA); //le cambiamos el tipo a agua
			dejarPasarElAguaPorVecinosHuecos(bloque); //dejar pasar el agua por los vecinos al bloque que sean huecos
			haPasadoElAgua = true;
		}
		
		return haPasadoElAgua;
	}
	
	/// <summary>
	/// Deja que pase el agua por los bloques vacios que son vecinos al bloque indicado
	/// </summary>
	/// <param name='bloque'>
	/// El bloque al que hay que buscarle los vecinos que sean vacios y dejar pasar el agua por ellos y 
	/// con los vecinos que sean vacios de estos y asi sucesivamente
	/// </param>
	private static void dejarPasarElAguaPorVecinosHuecos(Bloque bloque){
		int limiteIzq = 0; //limite izquierdo del terreno
		int limiteDcha = Terreno.totalBloquesX + 1; //limite derecho del terreno
		int limiteInf = 0; //limite superior del terreno
		int limiteSup = Terreno.totalBloquesZ + 1; //limite superior del terreno
		int limitePisoSup = Terreno.totalBloquesY; //numero de pisos del terreno
		int i = bloque.getXTerreno(); //coordenada x del terreno donde se localiza el bloque que comprobamos
		int j = bloque.getYTerreno(); //coordenada y del terreno donde se localiza el bloque que comprobamos
		int k = bloque.getZTerreno(); //coordenada z del terreno donde se localiza el bloque que comprobamos

		
		//comprobamos los limites del terreno y el camino de agua ya creado (para no repetir)
		if(i>=limiteIzq && i<limiteDcha && j>=0 && j< limitePisoSup && k>=limiteInf && k<limiteSup && Terreno.caminoAgua[i, j, k] == false){
			//caso base: el bloque es hueco, se rellena con agua
			if(bloque.getTipo() == TipoBloque.VACIO){ //el bloque es un hueco		
				bloque.setTipo(TipoBloque.AGUA); //le cambiamos el tipo a agua
				
				//TODO: cambiar todos los chunk cuando se sepan todos los bloques que ha afectado
				Chunks.getChunkQueContieneAlBloque(bloque).seHaModificadoElChunk();
				
				//actualizar camino de agua
				if(Terreno.caminoAgua[i, j, k] == false){
					Terreno.caminoAgua[i, j, k] = true;	
				}
			}
			
			//si el bloque es agua y existe algun vecino que es hueco, seguimos propagando el agua por los vecinos huecos de ese vecino
			if(bloque.getTipo() == TipoBloque.AGUA && existeAlgunBloqueVecinoConElTipo(bloque, TipoBloque.VACIO)){
				//vecino de abajo
				Bloque vecino = getBloqueEnCoordsTerreno(i-1, j, k); 
				dejarPasarElAguaPorVecinosHuecos(vecino);
				
				//vecino de arriba
				vecino = getBloqueEnCoordsTerreno(i+1, j, k); 
				dejarPasarElAguaPorVecinosHuecos(vecino); 
				
				//vecino de la izquierda
				vecino = getBloqueEnCoordsTerreno(i, j, k-1); 
				dejarPasarElAguaPorVecinosHuecos(vecino); 
				
				//vecino de la derecha
				vecino = getBloqueEnCoordsTerreno(i, j, k+1); 
				dejarPasarElAguaPorVecinosHuecos(vecino); 
				
				//vecino del piso inferior
				vecino = getBloqueEnCoordsTerreno(i, j-1, k);
				dejarPasarElAguaPorVecinosHuecos(vecino);
				
				//vecino del piso superior
				vecino = getBloqueEnCoordsTerreno(i, j+1, k);
				dejarPasarElAguaPorVecinosHuecos(vecino); 
			}
			
				
		}
	}
	#endregion
}


