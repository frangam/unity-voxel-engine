using UnityEngine;
using System.Collections;

public class GestosCamara : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var pan = new TKPanRecognizer();		
		// when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
//		if( Application.platform == RuntimePlatform.IPhonePlayer )
//			pan.minimumNumberOfTouches = 2;
		
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
//			float newCamPosX = Mathf.Clamp(camPos.x - panX, -100, -7);
//			float newCamPosY = Mathf.Clamp(camPos.y - panY, 48, 87);
			
			Vector3 j = camPos - new Vector3(panX, panY) / 25;

			float jx = Mathf.Clamp(j.x, -82, -23);
			float jy = Mathf.Clamp(j.y, 55, 77);
			
			Vector3 res = new Vector3(jx,jy, j.z);
			Debug.Log (jx+" "+jy+" "+PuntoMedioMaxX+" "+PuntoMedioMaxY);
			
			if(jx > PuntoMedioMaxX && jy > PuntoMedioMaxY){
				//Camera.mainCamera.transform.position = new Vector3(Mathf.Clamp(j.x, PuntoMedioMaxX, PuntoMedioX), Mathf.Clamp(j.y, PuntoMedioMaxY, PuntoMedioY));
				//Camera.mainCamera.transform.position = new Vector3(PuntoMedioMaxX,PuntoMedioMaxY, j.z);
			}else if(jx > PuntoMedioMaxX && jy < PuntoMedioMinY){
//				Camera.mainCamera.transform.position = new Vector3(PuntoMedioMaxX, PuntoMedioMinY);
				
			}else if(jx < PuntoMedioMinX && jy > PuntoMedioMaxY){
//				Camera.mainCamera.transform.position = new Vector3(PuntoMedioMinX, PuntoMedioMaxY);
				
			}else if(jx < PuntoMedioMinX && jy < PuntoMedioMinY){
//				Camera.mainCamera.transform.position = new Vector3(PuntoMedioMinX, PuntoMedioMinY);
			}else{
				Camera.mainCamera.transform.position = res;
			}
			//Camera.mainCamera.transform.position = new Vector3(newCamPosX, newCamPosY, camPos.z);
			//Debug.Log( "pan recognizer fired: " + r );
		};
		
		// continuous gestures have a complete event so that we know when they are done recognizing
		pan.gestureCompleteEvent += r =>
		{
			//Debug.Log( "pan gesture complete" );
		};
		TouchKit.addGestureRecognizer( pan );
		
		
		
		
		var pinch = new TKPinchRecognizer();
		pinch.gestureRecognizedEvent += ( r ) =>
		{
			Camera.mainCamera.orthographicSize = Mathf.Clamp(Camera.mainCamera.orthographicSize + pinch.deltaScale*10, 9, 25);
//			Camera.mainCamera.fieldOfView = Mathf.Clamp(Camera.mainCamera.fieldOfView + pinch.deltaScale * 50, 20, 75);
//			Debug.Log (pinch.deltaScale);
			Debug.Log( "pinch recognizer fired: " + r );
		};
		TouchKit.addGestureRecognizer( pinch );

	}
}
