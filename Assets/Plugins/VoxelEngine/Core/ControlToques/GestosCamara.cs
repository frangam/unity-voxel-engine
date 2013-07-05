using UnityEngine;
using System.Collections;

public class GestosCamara : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var pan = new TKPanRecognizer();		
		
		pan.gestureRecognizedEvent += ( r ) =>
		{
			float LimiteMinX = -82f;
			float LimiteMaxX = -23f;
			float LimiteMinY = 55f;
			float LimiteMaxY = 77f;
			
			float PuntoMedioX = (LimiteMaxX+LimiteMinX) / 2;
			float PuntoMedioY = (LimiteMaxY+LimiteMinY) / 2;
			
			float PuntoMedioMinX = (LimiteMinX + PuntoMedioX) / 2;
			float PuntoMedioMaxX = (LimiteMaxX + PuntoMedioX) / 2;
			float PuntoMedioMinY = (LimiteMinY + PuntoMedioY) / 2;
			float PuntoMedioMaxY = (LimiteMaxY + PuntoMedioY) / 2;
			
			float panX = pan.deltaTranslation.x;
			float panY = pan.deltaTranslation.y;
			Vector3 camPos = Camera.mainCamera.transform.position;

			
			Vector3 j = camPos - new Vector3(panX, panY) / 25;

			float jx = Mathf.Clamp(j.x, -82, -23);
			float jy = Mathf.Clamp(j.y, 55, 77);
			
			Vector3 res = new Vector3(jx,jy, j.z);
			
			if(jx > PuntoMedioMaxX && jy > PuntoMedioMaxY){
			}else if(jx > PuntoMedioMaxX && jy < PuntoMedioMinY){
			}else if(jx < PuntoMedioMinX && jy > PuntoMedioMaxY){
			}else if(jx < PuntoMedioMinX && jy < PuntoMedioMinY){
			}else{
				Camera.mainCamera.transform.position = res;
			}
		};
		
		// continuous gestures have a complete event so that we know when they are done recognizing
		pan.gestureCompleteEvent += r =>
		{
		};
		TouchKit.addGestureRecognizer( pan );
		
		
		
		
		var pinch = new TKPinchRecognizer();
		pinch.gestureRecognizedEvent += ( r ) =>
		{
			Camera.mainCamera.orthographicSize = Mathf.Clamp(Camera.mainCamera.orthographicSize + pinch.deltaScale*10, 9, 25);
		};
		TouchKit.addGestureRecognizer( pinch );

	}
}
