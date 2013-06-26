using UnityEngine;
using System.Collections;

public static class TiposBloques {
	
	public static TipoBloqueBase[] tipos = new TipoBloqueBase[] {
		
	};
	public static TipoBloqueBase getBloque(TipoBloque tipoBloque)
	{
		return tipos[(int)tipoBloque];
	}
}
