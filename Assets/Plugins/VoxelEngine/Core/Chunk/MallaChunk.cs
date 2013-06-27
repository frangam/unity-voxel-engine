using UnityEngine;
using System.Collections;

public class MallaChunk : MonoBehaviour {

	public MeshFilter _malla;
	public MeshCollider _collider;
	private Chunk _chunk;
	private bool _seHaModificadoElChunk = false;
	
	public MeshFilter getMalla() {
		return _malla; 
	}
	public void setMalla(MeshFilter malla){ 
		_malla = malla; 
	}
	
	public MeshCollider getCollider(){
		return _collider;
	}
	public void setCollider(MeshCollider collider){
		_collider = collider;
	}
	public void setChunk(Chunk chunk)
	{
		this._chunk = chunk;
	}
	public void LateUpdate()
	{
		if (_seHaModificadoElChunk == true)
		{
			_seHaModificadoElChunk = false;
			_chunk.actualizarMalla();
		}
	}
	public void seHaModificadoElChunk()
	{
		_seHaModificadoElChunk = true;
	}
}
