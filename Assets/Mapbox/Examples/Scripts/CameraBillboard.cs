namespace Mapbox.Examples
{
	using UnityEngine;

	public class CameraBillboard : MonoBehaviour
	{
		[HideInInspector] public Camera _camera;
		[SerializeField] bool onlyY;

		public void Start()
		{
			_camera = Camera.main;
		}

		void Update()
		{
			transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
			if (onlyY)
			{
				transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
			}
		}
	}
}