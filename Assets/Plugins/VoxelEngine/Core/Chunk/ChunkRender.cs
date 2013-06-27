using UnityEngine;
using System.Collections.Generic;

public class ChunkRender {
	
	private static List<Vector3> verticiesMallaChunk;
	private static List<int> triangulosMallaChunk;
	private static List<Vector2> uvCoorTexturaMallaChunk;
	private static Dictionary<TipoBloque, SubMallaChunk> _subMallasChunk;
	
	
	public static Mesh renderizar(Chunk chunk)
	{
		verticiesMallaChunk.Clear();
		triangulosMallaChunk.Clear();
		uvCoorTexturaMallaChunk.Clear();
		_subMallasChunk.Clear();
		Bloque bloque = null;
		
		for(int x=0; x < chunk.getNumBloquesEnX(); x++)
		{
			for(int y=0; y < chunk.getNumBloquesEnY(); y++)
			{
				for(int z=0; z < chunk.getNumBloquesEnZ(); z++)
				{
					Vector3 posicionBloque = new Vector3(x,y,z);
					//miramos los vecinos.
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
						//hace falta dibujarlo!! miramos si ya existe su submalla en el diccionario.
						if(_subMallasChunk.ContainsKey(bloque.getTipo()))
						{
							//existe asi que no hace falta crearlo.
						}else{
							//no existe asi que lo añadimos.
							_subMallasChunk.Add(bloque.getTipo(), new SubMallaChunk(bloque.getTipo()));
						}
						
						if(!arriba.esDibujable())
						{
							crearCaraMalla(bloque, posicionBloque, "arriba");
						}
						if(!abajo.esDibujable())
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
		mesh.subMeshCount = _subMallasChunk.Count;
		int i = 1;
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
		
		uvCoorTexturaMallaChunk.Add(new Vector2(0, 0));
		uvCoorTexturaMallaChunk.Add(new Vector2(1, 0));
		uvCoorTexturaMallaChunk.Add(new Vector2(0, 1));
		uvCoorTexturaMallaChunk.Add(new Vector2(1, 1));
			
			
		List<int> tri = _subMallasChunk[bloque.getTipo()].getTriangulos();
		tri.Add(indeceVertices);
		tri.Add(indeceVertices+1);
		tri.Add(indeceVertices+2);
		
		tri.Add(indeceVertices+2);
		tri.Add(indeceVertices+3);
		tri.Add(indeceVertices);
	}
	
}
