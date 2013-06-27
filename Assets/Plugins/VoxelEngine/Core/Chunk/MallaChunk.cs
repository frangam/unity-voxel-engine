using UnityEngine;
using System.Collections;

//	<summary>
// 	Representa el GameObject para Unity, que sera solo una MeshFilter y un MeshCollider
// 	con la informacion de cada Chunk para saber que cubos debe mostrar y cuales no.
// 	</summary>

public class MallaChunk : MonoBehaviour {
	
	/// <summary>
	/// 	MeshFilter del GameObject.
	/// </summary>
	public MeshFilter _malla;
	
	/// <summary>
	/// 	MeshCollider del GameObject.
	/// </summary>
	public MeshCollider _collider;
	
	private Chunk _chunk;
	private bool _seHaModificadoElChunk = false;
	
	/// <summary>
	/// 	Devuelve la MeshFilter del GameObject MallaChunk.
	/// </summary>
	/// <returns>
	/// 	MeshFilter.
	/// </returns>
	public MeshFilter getMalla() {
		return _malla; 
	}
	
	/// <summary>
	/// 	Cambia la MeshFilter del GameObject MallaChunk.
	/// </summary>
	/// <param name='malla'>
	/// 	MeshFilter nuevo.
	/// </param>
	public void setMalla(MeshFilter malla){ 
		_malla = malla; 
	}
	
	/// <summary>
	/// 	Devuelve el MeshCollider del GameObject MallaChunk.
	/// </summary>
	/// <returns>
	/// 	MeshCollider.
	/// </returns>
	public MeshCollider getCollider(){
		return _collider;
	}
	
	/// <summary>
	/// 	Cambia la MeshCollider del GameObject MallaChunk.
	/// </summary>
	/// <param name='malla'>
	/// 	MeshCollider nuevo.
	/// </param>
	public void setCollider(MeshCollider collider){
		_collider = collider;
	}
	
	/// <summary>
	/// 	Cambia el Chunk del GameObject MallaChunk.
	/// </summary>
	/// <param name='chunk'>
	/// 	Chunk.
	/// </param>
	public void setChunk(Chunk chunk)
	{
		this._chunk = chunk;
	}
	
	/// <summary>
	/// 	Comprobamos si se ha modificado cada el chunk para actuarlizarlo
	/// </summary>
	public void LateUpdate()
	{
		if (_seHaModificadoElChunk == true)
		{
			_seHaModificadoElChunk = false;
			_chunk.actualizarMalla();
		}
	}
	/// <summary>
	/// Cambiamos el valor del bool seHaModificadoElChunk a true.
	/// </summary>
	public void seHaModificadoElChunk()
	{
		_seHaModificadoElChunk = true;
	}
}
