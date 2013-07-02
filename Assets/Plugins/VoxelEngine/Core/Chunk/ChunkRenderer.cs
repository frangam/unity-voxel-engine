using UnityEngine;
using System.Collections.Generic;

public class ChunkRenderer {
	
	private static List<Vector3> verticiesMallaChunk = new List<Vector3>();
	private static List<Vector2> uvCoorTexturaMallaChunk = new List<Vector2>();
	private static Dictionary<TipoBloque, SubMallaChunk> _subMallasChunk = new Dictionary<TipoBloque, SubMallaChunk>();
	
	
	public static Mesh renderizar(Chunk chunk)
	{
		verticiesMallaChunk.Clear();
		uvCoorTexturaMallaChunk.Clear();
		_subMallasChunk.Clear();
		Bloque bloque = null;
		
		/*
		 * Recorremos todos los bloques del chunk por niveles, para que se genere bien todas las listas de materiales que necesita.
		 * */
		for(int y=0; y < chunk.getNumBloquesEnY(); y++)
		{
			for(int x=0; x < chunk.getNumBloquesEnX(); x++)
			{
				for(int z=0; z < chunk.getNumBloquesEnZ(); z++)
				{
					Vector3 posicionBloque = new Vector3(x,y,z);
					//buscamos los vecinos.
					bloque = chunk.getBloque(x, y, z);                    
                    Bloque arriba = chunk.getBloque(x, y + 1, z);
					Bloque abajo = chunk.getBloque(x, y - 1, z);
					Bloque frente = chunk.getBloque(x, y, z +1);
					Bloque atras = chunk.getBloque(x, y, z - 1);
					Bloque izquierda = chunk.getBloque(x + 1, y, z);
					Bloque derecha = chunk.getBloque(x - 1, y, z);
					
					//miramos si hace falta dibujar ese bloque.
					if(bloque != null && bloque.esDibujable())
					{	
						if(!arriba.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "arriba");
						}
						//si es la cara de abajo del todo no la pintamos ya que nunca se vera.
						if(!abajo.esDibujable() && TipoBloque.LIMITE_TERRENO != abajo.getTipo())
						{
							crearCaraMalla(bloque, posicionBloque, "abajo");
						}
						if(!frente.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "frente");
						}
						if(!atras.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "atras");
						}
						if(!izquierda.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "izquierda");
						}
						if(!derecha.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "derecha");
						}
					}
				}
			}
		}
		List<TipoBloque> listaTipos = new List<TipoBloque>(_subMallasChunk.Keys);
		MaterialesDelChunk.inicializarMateriales(chunk.ToString(), listaTipos);
		Mesh mesh = new Mesh();
		mesh.vertices = verticiesMallaChunk.ToArray();
		mesh.uv = uvCoorTexturaMallaChunk.ToArray();
		mesh.subMeshCount = _subMallasChunk.Count+1; //+1 porque su valor por defecto sin ninguna submalla es 1
		int i = 0; //indice inicial de submaya, como no hay un array de triangulos principal empezamos en 0
		foreach(KeyValuePair<TipoBloque,SubMallaChunk> smc in _subMallasChunk)
		{
			mesh.SetTriangles(smc.Value.getTriangulos().ToArray(), i);
			i++;
		}
		return mesh;
		
	}
	
	private static void crearCaraMalla(Bloque bloque, Vector3 posicionBloque, string sitio)
	{
		int indeceVertices = verticiesMallaChunk.Count;
		TipoBloque tipoBloque = bloque.getTipo();
		
		//si no existe el tipo de bloque en el diccionario lo creamos.
		if(!_subMallasChunk.ContainsKey(tipoBloque))
		{
			_subMallasChunk.Add(tipoBloque, new SubMallaChunk(tipoBloque));
		}
		
		switch(sitio){
		case "arriba":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,0));
			break;
			
		case "abajo":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,1));
			break;
			
		case "frente":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,1));
			break;
			
		case "atras":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,0));
			break;
			
		case "izquierda":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,1,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(1,0,1));
			break;
			
		case "derecha":
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,1));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,1,0));
			verticiesMallaChunk.Add(posicionBloque + new Vector3(0,0,0));
			break;
			
		}
		//añadimos las coordenadas uv para que sepa donde pintar la textura.
		//debe ser en ese orden, de lo contrario no se veran bien las texturas.
		//sentido anti-horario.
		uvCoorTexturaMallaChunk.Add(new Vector2(0, 0));
		uvCoorTexturaMallaChunk.Add(new Vector2(1, 0));
		uvCoorTexturaMallaChunk.Add(new Vector2(1, 1));
		uvCoorTexturaMallaChunk.Add(new Vector2(0, 1));
				
		//añadimos los triangulos a su submalla correspondiente.
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices);
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices+1);
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices+2);
		
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices+2);
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices+3);
		_subMallasChunk[tipoBloque].getTriangulos().Add(indeceVertices);
	}
}
