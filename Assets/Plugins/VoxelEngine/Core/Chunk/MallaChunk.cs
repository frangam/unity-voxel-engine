using UnityEngine;
using System.Collections;

public class MallaChunk : MonoBehaviour {

	public MeshFilter _malla;
	public MeshCollider _collider;
	private Chunk _chunk;
	private bool _seHaModificadoElChunk = false;
	
	public MeshFilter Malla { get{ return _malla; } set{ _malla = value; }}
	
	public MeshFilter getCollider(){
		return _collider;
	}
	public void setCollider(MeshCollider collider){
		_collider = collider;
	}
	public void setChunk(Chunk chunk)
	{
		this.chunk = chunk;
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
