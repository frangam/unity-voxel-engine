using UnityEngine;
using System.Collections;

public class AccionesTerreno : MonoBehaviour {
	
	private Bloque _bloqueSeleccionado = new Bloque(TipoBloque.DESCONOCIDO);
	private TipoBloque _tipoBloqueSeleccionado;
	private Vector3 _bloqueSeleccionadoPos;
	private Vector3 _colisionPoint;
	
	#region atributos de configuracion desde el inspector
	public SonidosTerreno sonidosTerreno;
	
	public ParticleSystem particulasDestruccionHierba;
	public ParticleSystem particulasDestruccioTierra;
	public ParticleSystem particulasDestruccionPiedra;
	#endregion
	
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
		if ( bloqueSeleccionado.getTipo() == TipoBloque.VACIO){
			//bloque que esta debajo
			Bloque bloqueDebajo = Bloques.getBloqueEnCoordsTerreno(bloqueSeleccionado.getXTerreno(), bloqueSeleccionado.getYTerreno() - 1, bloqueSeleccionado.getZTerreno());
			
			sonidosTerreno.playCrearBloque(); //reproducimos sonido de creacion de bloque
			
			//si el bloque se que va a construir tiene debajo agua
			if(bloqueDebajo.getTipo() == TipoBloque.AGUA){
				//tenemos que crear todos los bloques que tengan agua hasta encontrar el suelo del terreno
				while(bloqueDebajo.getTipo() == TipoBloque.AGUA && bloqueDebajo.getTipo() != TipoBloque.SUELO){
					Bloque bloqueConstruir = bloqueDebajo;
					bloqueConstruir.crearse(_tipoBloqueSeleccionado);
					
					//multitarea: se refresca la renderizacion de la malla del chunk
					StartCoroutine(GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.ActualizarChunk(bloqueConstruir, false  ) );
					
					bloqueDebajo = Bloques.getBloqueEnCoordsTerreno(bloqueDebajo.getXTerreno(), bloqueDebajo.getYTerreno() - 1, bloqueDebajo.getZTerreno());
				}
			}
			else{
				//creamos el bloque
				_bloqueSeleccionado = bloqueSeleccionado;
				_bloqueSeleccionado.crearse(_tipoBloqueSeleccionado);
				//multitarea: se refresca la renderizacion de la malla del chunk
				StartCoroutine(GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.ActualizarChunk(bloqueSeleccionado, false  ) );
			}			
		}		
	}
	
	public void DestroyBlock(Vector2 posicionToque = new Vector2()){		
		Bloque bloqueSeleccionado = GetBloque(false, posicionToque);
		
		if ( bloqueSeleccionado.esDibujable() && bloqueSeleccionado.getTipo() != TipoBloque.AGUA)
		{
			_bloqueSeleccionado = bloqueSeleccionado;
			
			sonidosTerreno.playDestruirBloque(); //reproducimos sonido de destruccion de bloque

			
			#region efecto de destruccion del bloque
			ParticleSystem particulasDestruccion = null;
			
			//seleccionamos las particulas para la destruccion del bloque segun el tipo del bloque a destruir
			switch (bloqueSeleccionado.getTipo())
			{
			case TipoBloque.HIERBA:
					particulasDestruccion = particulasDestruccionHierba;
					break;
				
				case TipoBloque.TIERRA:
					particulasDestruccion = particulasDestruccioTierra;
					break;
				
				case TipoBloque.PIEDRA:
					particulasDestruccion = particulasDestruccionPiedra;
					break;
			}
			
			//si se ha seleccionado unas particulas para crear el efecto de destruccion del bloque
			if (particulasDestruccion != null)
			{
				ParticleSystem particulas = GameObject.Instantiate(particulasDestruccion) as ParticleSystem;
				
				// Apply the same light of the block that will be destroyed into the particles
				int x = Mathf.RoundToInt(bloqueSeleccionado.getXTerreno());
				int y = Mathf.RoundToInt(bloqueSeleccionado.getYTerreno());
				int z = Mathf.RoundToInt(bloqueSeleccionado.getZTerreno());
				Bloque bloqueSuperior = Bloques.getBloqueEnCoordsTerreno(x, y+1, z);
//				float colorParticulas = ((float)topBlock.Light)/255.0f;
//				particulas.renderer.material.color = new Color(colorParticulas, colorParticulas, colorParticulas);
				
				Vector3 posParticulas = new Vector3(bloqueSeleccionado.getXTerreno(), bloqueSeleccionado.getYTerreno(), bloqueSeleccionado.getZTerreno());
				posParticulas.y += 0.75f;
				particulas.transform.position = posParticulas;
			}
			#endregion
	
			//destruimos el bloque
			_bloqueSeleccionado.destruirse();
			
			//si existe algun vecino al bloque a destruir que tenga agua, propagamos el agua
			if(Bloques.existeAlgunBloqueVecinoConElTipo(bloqueSeleccionado, TipoBloque.AGUA)){
				Bloques.dejarPasarElAgua(bloqueSeleccionado); //dejamos que pase el agua por este bloque vacio y por todos sus vecino y vecinos de este
			}
			
			//multitarea: se refresca la renderizacion de la malla del chunk
			StartCoroutine(GameObject.Find("GeneradorTerreno").GetComponent<GeneradorTerreno>().terreno.ActualizarChunk(bloqueSeleccionado, false  ) );
		}			
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
