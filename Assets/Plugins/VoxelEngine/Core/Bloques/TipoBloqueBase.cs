using UnityEngine;
using System.Collections;

public class TipoBloqueBase : IBloque {
	
	public virtual bool esSolido(){
		return false;
	}
	
	public virtual Vector2 posicionUVTexturaCaraInferior()
	{
		return Vector2.zero;
	}
	public virtual Vector2 posicionUVTexturaCaraSuperior()
	{
		return Vector2.zero;
	}
	public virtual Vector2 posicionUVTexturaCaraLaterales()
	{
		return Vector2.zero;
	}
	public virtual Vector2 posicionUVTexturaCara()
	{
		return Vector2.zero;
	}
	public virtual bool destruirse(Bloque bloque)
	{
		if(!esSolido()){
			return false;
		}if(bloque.Chunk != null){
			//activar seHaModificadoElChunk();
		}
		bloque.Tipo = TipoBloque.VACIO;
		return true;
	}
	public virtual bool crearse(Bloque bloque, TipoBloque tipoNuevo)
	{
		if(!esSolido()){
			return false;
		}if(bloque.Chunk != null){
			//activar seHaModificadoElChunk();
		}
		bloque.Tipo = tipoNuevo; 
		return true;
	}
}
