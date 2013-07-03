using UnityEngine;
using System.Collections;

public class Acciones : MonoBehaviour {
	
	private Bloque _bloqueSeleccionado = new Bloque(TipoBloque.DESCONOCIDO);
	private TipoBloque _tipoBloqueSeleccionado;
//	private Terreno terreno;
	private Vector3 _bloqueSeleccionadoPos;
	private Vector3 _colisionPoint;
	
//	public void Init(Terreno terreno)
//	{
////		this.terreno = terreno;
//		_bloqueSeleccionado 
//	}
	
	public void setTipoBloqueSeleccionado(TipoBloque tipo)
	{
		_tipoBloqueSeleccionado = tipo;	
	}

	public void CreateBlock(Vector2 posicionToque = new Vector2())
	{	
		
		Bloque bloqueSeleccionado = GetBloque(true, posicionToque);
		
		//construir bloque si es de tipo vacio (hueco)
		if ( bloqueSeleccionado.getTipo() == TipoBloque.VACIO)
		{
//				_soundController.PlayCreateBlock();
			
			_bloqueSeleccionado = bloqueSeleccionado;
			_bloqueSeleccionado.crearse(_tipoBloqueSeleccionado);
	
			//multitarea: se refresca la renderizacion de la malla del chunk
			StartCoroutine(GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.ActualizarChunk(bloqueSeleccionado, false  ) );
		}		
	}
	
	public void DestroyBlock(Vector2 posicionToque = new Vector2()){		
//		Bloque bloqueSeleccionado = GetBloque(false, posicionToque);
//		
//		if ( bloqueSeleccionado.esDibujable() && bloqueSeleccionado.getTipo() != TipoBloque.SUELO)
//		{
//			_selectedBlock = bloqueSeleccionado;
////			
////			_soundController.PlayDestroyBlock();
//			
//			#region DestroyFx
//			ParticleSystem destroyParticle = null;
//			
//			switch (_selectedBlock.Type)
//			{
//				case BlockType.Grass:
//					destroyParticle = _grassDestroy;
//					break;
//				
//				case BlockType.Dirt:
//					destroyParticle = _dirtyDestroy;
//					break;
//				
//				case BlockType.Stone:
//					destroyParticle = _stoneDestroy;
//					break;
//			}
//			
//			if (destroyParticle != null)
//			{
//				ParticleSystem particle = GameObject.Instantiate(destroyParticle) as ParticleSystem;
//				
//				// Apply the same light of the block that will be destroyed into the particles
//				int x = Mathf.RoundToInt(_selectedBlockPosition.x);
//				int y = Mathf.RoundToInt(_selectedBlockPosition.y);
//				int z = Mathf.RoundToInt(_selectedBlockPosition.z);
//				Block topBlock = _world[x, y+1, z];
//				float particleColor = ((float)topBlock.Light)/255.0f;
//				particle.renderer.material.color = new Color(particleColor, particleColor, particleColor);
//				
//				Vector3 particlePos = _selectedBlockPosition;
//				particlePos.y += 0.75f;
//				particle.transform.position = particlePos;
//			}
//			#endregion
//			
//			_selectedBlock.Destroy();
//			StartCoroutine( _world.RefreshChunkMesh( new Vector3i(_selectedBlockPosition), false  ) );
			
			//multitarea: se refresca la renderizacion de la malla del chunk
//			StartCoroutine(GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.ActualizarChunk( new Vector3(_bloqueSeleccionadoPos.x, _bloqueSeleccionadoPos.y,0), false  ) );
//		}			
	}
	
	Bloque GetBloque(bool getNearestNeighbor = false, Vector2 posicionToque = new Vector2())
	{
		Ray ray = Camera.main.ScreenPointToRay( posicionToque ); //rayo desde la camara principal hasta la posicion de toque en pantalla
		
		RaycastHit hit = new RaycastHit();
		
		Debug.DrawRay (ray.origin, ray.direction, Color.black, 100f); //solo para debug, para ver el rayo lanzado
		Bloque bloque = new Bloque();
    	if (Physics.Raycast(ray, out hit, 1000.0f) == true)
		{	
			Debug.DrawRay (ray.origin, ray.direction, Color.black, 100f); //solo para debug, para ver el rayo lanzado
			_colisionPoint = hit.point;
            Vector3 hp = _colisionPoint + 0.0001f * ray.direction;

            int x = Mathf.CeilToInt(hp.x) - 1;
            int y = Mathf.CeilToInt(hp.y) - 1;
            int z = Mathf.CeilToInt(hp.z) - 1;

			_bloqueSeleccionadoPos = new Vector3(x,y,z);
			
			
			if (getNearestNeighbor == true)
			{
				#region GetNearestNeighbor
				Vector3 nearestBlock = _colisionPoint - _bloqueSeleccionadoPos;

				if (nearestBlock.x == 1.0f)
				{
					x++;
				}
				else if (nearestBlock.x == 0.0f)
				{
					x--;
				}
	
				if (nearestBlock.y == 1.0f)
				{
					y++;
				}
				else if (nearestBlock.y == 0.0f)
				{
					y--;
				}
	
				if (nearestBlock.z == 1.0f)
				{
					z++;
				}
				else if (nearestBlock.z == 0.0f)
				{
					z--;
				}
								
				_bloqueSeleccionadoPos.x = x;
				_bloqueSeleccionadoPos.y = y;
				_bloqueSeleccionadoPos.z = z;
				
				bloque = Terreno.getBloque(x,y,z);
				#endregion
			}
			else
			{
				bloque = Terreno.getBloque(x,y,z);	
			}
		}		
		
		return bloque;
	}
	
	void OnDrawGizmos() 
	{
//		if (Application.isPlaying == true)
//		{
//			Gizmos.DrawRay(Camera.main.ScreenPointToRay( new Vector3(Screen.width/2f, Screen.height/2f, 0.0f) ) );
//			
//			if ( _selectedBlock != null && _selectedBlock.Type != BlockType.Unknown )
//			{
//				Vector3 cubePos = _selectedBlockPosition + Vector3.one/2;
//				Gizmos.color = Color.red;
//				Gizmos.DrawWireCube( cubePos, Vector3.one*1.05f );
//			}
//		}
	}
}
