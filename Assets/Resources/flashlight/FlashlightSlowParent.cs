// ****************************************************************
// Unity Game Engine C#-Script - Slow Parent FlashLight
// Everything in this file by MrLarodos: http://www.youtube.com/user/MrLarodos
//
// Released under the Creative Commons Attribution 3.0 Unported License:
// http://creativecommons.org/licenses/by/3.0/de/
// http://creativecommons.org/licenses/by/3.0/
//
// If you use this file or parts of it, you have to include this information header.
// ****************************************************************

using UnityEngine;

namespace Resources.flashlight
{
	[RequireComponent(typeof(AudioSource))]
	public class FlashlightSlowParent : MonoBehaviour{

		public GameObject owner;
		public float turnSpeed = 5f;
		public bool point2Target;
		public bool swingVertical;
		public float swingVerticalDegrees = 3f;
		public float swingVerticalSpeed = 1f;
		public bool swingHorizontal;
		public float swingHorizontalDegrees = 3f;
		public float swingHorizontalSpeed = 1f;
		public bool holdLeftHand;
		public float camHorizontalOffset;
		public float camVerticalOffset;
		public KeyCode flashlightSwitchKey = KeyCode.F;
		public bool soundsActive;
		public AudioClip soundFlashlightON;
		public AudioClip soundFlashlightOff;

		private RaycastHit _lookRay;
		private bool _flashlightActive;
		private AudioSource _audioSource;
		private Light _flashlightLight;
		private Vector3 _handpos;
		private Quaternion _targetRotation;

		private void Awake()
		{
			if (!owner)
			{
				try
				{
					owner = transform.parent.gameObject;
				}
				catch
				{
					print("Kein Owner in " + name + " gesetzt! " + name + " wird nun gelöscht.");
					Destroy(gameObject);
				}
			}

			SwitchHandPos();
			
			_audioSource = GetComponent<AudioSource>();
			_flashlightLight = transform.GetComponent<Light>();

			if (_flashlightLight) return;
			print("Keine Lichtquelle in " + name + " gesetzt! " + name + " wird nun gelöscht.");
			Destroy(gameObject);
		}

		private void Start()
		{
			_flashlightActive = false; //Taschenlampe beim Start deaktivieren
			_flashlightLight.enabled = false; //Taschenlampe beim Start deaktivieren

			if (transform.parent) transform.parent = null;
			// cam_vertical_offset = this.transform.position.y - owner.transform.position.y;
		}

		private void Update()
		{
			TurnOnOff();
			if (!_flashlightLight.enabled) return;
			SwitchHandPos();
			SmoothRotate();
		}

		private void TurnOnOff()
		{
			var switchKey = Input.GetKeyDown(flashlightSwitchKey);

			switch (switchKey)
			{
				case true when _flashlightActive:
				{
					//Taschenlampe aussschalten
					_flashlightActive = false;
					if (soundsActive) PlaySound(soundFlashlightOff, 1.0F, false);
					_flashlightLight.enabled = false;
					break;
				}
				case true when !_flashlightActive:
				{
					//Taschenlampe einschalten
					_flashlightActive = true;
					if (soundsActive) PlaySound(soundFlashlightON, 1.0F, false);
					transform.rotation = owner.transform.rotation;
					_flashlightLight.enabled = true;
					break;
				}
			}
		}
		private void SwitchHandPos()
		{
			if (holdLeftHand)
			{
				//Check, ob die Taschenlampe in der linken Hand gehalten wird
				_handpos = owner.transform.right * -1; //Taschenlampe links
			}
			else
			{
				_handpos = owner.transform.right; //Taschenlampe rechts
			}

			_handpos *= camHorizontalOffset; //Wie weit ist die Hand mit der Lampe vom Körper entfernt
		}
		private void SmoothRotate()
		{
			var newPosition = owner.transform.position + _handpos;

			newPosition = new Vector3(newPosition.x, newPosition.y + camVerticalOffset, newPosition.z);
			transform.position = newPosition;
			
			if (point2Target && Physics.Raycast(owner.transform.position, owner.transform.forward, out _lookRay, 500F))
			{
				Vector3 lookPoint = new Vector3(_lookRay.point.x, _lookRay.point.y, _lookRay.point.z);
				_targetRotation = Quaternion.LookRotation(lookPoint - transform.position);
			}
			else
			{
				_targetRotation = owner.transform.rotation;
			}
			
			
			var distanceSwingVertical = 0f;
			if (swingVertical)
			{
				var swingVInputVal = Time.timeSinceLevelLoad * swingVerticalSpeed;
				distanceSwingVertical = swingVerticalDegrees * Mathf.Sin(swingVInputVal);
			}

			var distanceSwingHorizontal = 0f;
			if (swingHorizontal)
			{
				var swingVInputVal = Time.timeSinceLevelLoad * swingHorizontalSpeed;
				distanceSwingHorizontal = swingHorizontalDegrees * Mathf.Sin(swingVInputVal);
			}

			_targetRotation = Quaternion.Euler(_targetRotation.eulerAngles.x + distanceSwingVertical,
				_targetRotation.eulerAngles.y + distanceSwingHorizontal, _targetRotation.eulerAngles.z);

			transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, turnSpeed * Time.deltaTime);
		}
		private void PlaySound(AudioClip soundFile, float vol, bool randomPitch)
		{
			_audioSource.clip = soundFile;

			_audioSource.pitch = randomPitch ? Random.Range(0.7F, 1.0F) : 1.0F;

			_audioSource.volume = vol;
			_audioSource.loop = false;
			_audioSource.PlayOneShot(soundFile, vol);

		}

	}
}
