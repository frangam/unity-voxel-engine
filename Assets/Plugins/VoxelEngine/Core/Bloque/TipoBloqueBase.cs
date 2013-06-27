using UnityEngine;
using System.Collections;

public class TipoBloqueBase : IBloque {
	
	public virtual bool esDibujable(){
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
		if(!esDibujable()){
			return false;
		}if(bloque.getChunk() != null){
			bloque.getChunk().seHaModificadoElChunk();
		}
		bloque.setTipo(TipoBloque.VACIO);
		return true;
	}
	
	public virtual bool crearse(Bloque bloque, TipoBloque tipo)
	{
		if(!esDibujable()){
			return false;
		}if(bloque.getChunk() != null){
			bloque.getChunk().seHaModificadoElChunk();
		}
		bloque.setTipo(tipo); 
		return true;
	}
}
