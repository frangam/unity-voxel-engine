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
}