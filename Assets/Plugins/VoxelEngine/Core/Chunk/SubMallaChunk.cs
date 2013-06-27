using UnityEngine;
using System.Collections.Generic;

public class SubMallaChunk {

	private List<int> triangulos;
	private TipoBloque tipoBloqueARenderizar;
	
	public SubMallaChunk(TipoBloque tipoBloque)
	{
		triangulos = new List<int>();
		tipoBloqueARenderizar = tipoBloque;
	}
	
	public List<int> getTriangulos()
	{
		return triangulos;
	}
	
	public TipoBloque getTipoBloqueARenderizar()
	{
		return tipoBloqueARenderizar;
	}
//	public int getIndiceSubMalla(MallaChunk mallaChunk)
//	{
//		return MaterialesDelChunk.getIndiceMaterialSegunTipo(mallaChunk, tipoBloqueARenderizar);
//	}
}
	/// Este metodo sirve obtener el valor adecuado de una submalla de una Mesh cuando se utiliza el metodo Mesh.SetTriangles(int[] arrayTriangulosSubmalla, int indiceSubmalla).
	/// Dicho indice sirve para indicar cual sera el material del MeshRenderer que se debe utilizar para renderizar esa submalla.
	/// Hay que comprender cómo asigna Unity los materiales a las submallas de MeshRenderer:
	/// >> La asignacion de materiales a una submalla de MeshRender se realiza teniendo en cuenta el indice de submalla que se le indica
	/// >> y segun este, se accede al array de materiales de MeshRenderer orden inverso, es decir, desde el ultimo elemento hasta el primero.
	/// >> Por ejemplo: indice de submalla 2 (indica que es la submalla numero 2). Suponiendo que el array de materiales tiene 4 elementos que están en las posiciones 0, 1, 2 y 3.
	/// >> Pues bien, el material que se le asigna a la submalla es el material que ocupa la posicion 2 del array, porque la subllama 1 tendria asignado el ultimo elemento del array
	/// >> de materiales el numero 3. La submalla 3 tendria asignado el material de la posicion del array 1, y la ultima cuarta submalla el elemento 0 del array.
	/// 
	/// 