using UnityEngine;
using System.Collections;

//<summary>
//Ball movement controlls and simple third-person-style camera
//</summary>
public class RollerBall : MonoBehaviour {
	public float movementSpeed = 15.0f;
	public float counterClockwise = -2.0f;
	public GameObject ViewCamera = null;
	public AudioClip JumpSound = null;
	public AudioClip HitSound = null;
	public AudioClip CoinSound = null;

	private Rigidbody mRigidBody = null;
	private AudioSource mAudioSource = null;
	private bool mFloorTouched = false;

	float x,y,z;


	void Start () {
		mRigidBody = GetComponent<Rigidbody> ();
		mAudioSource = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		x = ViewCamera.transform.eulerAngles.x;
		y = ViewCamera.transform.eulerAngles.y;
		z = ViewCamera.transform.eulerAngles.z;
		//temp.transform.localRotation = ViewCamera.transform.localRotation;
		if (mRigidBody != null) {
			/*if (Input.GetButton ("Horizontal")) {
				//mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
				ViewCamera.transform.Rotate (0, Time.deltaTime - counterClockwise, 0);
			}
			if (Input.GetButton ("Vertical")) {
				//mRigidBody.AddTorque(Vector3.right * Input.GetAxis("Vertical")*10);
				ViewCamera.transform.Rotate (0, Time.deltaTime + counterClockwise, 0);


			}*/
			if (Input.GetKey(KeyCode.UpArrow))
			{
			//	mRigidBody.AddTorque(Vector3.back * Input.GetAxis("Horizontal")*10);
				ViewCamera.transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
				mRigidBody.transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				ViewCamera.transform.Translate(-transform.forward * Time.deltaTime * movementSpeed , Space.World);
				mRigidBody.transform.Translate(-transform.forward * Time.deltaTime * movementSpeed, Space.World);
			}
			 if (Input.GetKey(KeyCode.RightArrow))
			{
				mRigidBody.transform.Rotate (0, Time.deltaTime - counterClockwise, 0);
				ViewCamera.transform.Rotate (0, Time.deltaTime - counterClockwise, 0);
				//ViewCamera.transform.Rotate(0, Time.deltaTime - counterClockwise, 0);
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				ViewCamera.transform.Rotate(0, Time.deltaTime + counterClockwise, 0);
				mRigidBody.transform.Rotate(0, Time.deltaTime + counterClockwise, 0);
			}
			if(Input.GetKey(KeyCode.Space)){
				mRigidBody.velocity = Vector3.zero;
				ViewCamera.transform.eulerAngles=new Vector3(0,0,0);
			}
			/*if (Input.GetButtonDown("Jump")) {
				if(mAudioSource != null && JumpSound != null){
					mAudioSource.PlayOneShot(JumpSound);
	 			}
				mRigidBody.AddForce(Vector3.up*200);
			}*/
		}
		if (ViewCamera != null) {
		// Vector3 direction = (Vector3.up*2+Vector3.back)*2;
			Vector3 direction=new Vector3();
			RaycastHit hit;
			Debug.DrawLine(transform.position,transform.position+direction,Color.red);
			if(Physics.Linecast(transform.position,transform.position+direction,out hit)){
				ViewCamera.transform.position = hit.point;
			}else{
				ViewCamera.transform.position = transform.position+direction;
			}
			ViewCamera.transform.LookAt(transform.position);
		}
	}

	void OnCollisionEnter(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = true;
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.y > .5f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		} else {
			if (mAudioSource != null && HitSound != null && coll.relativeVelocity.magnitude > 2f) {
				mAudioSource.PlayOneShot (HitSound, coll.relativeVelocity.magnitude);
			}
		}
		mRigidBody.velocity = Vector3.zero;
		//mRigidBody.angularDrag = 1.0f;
		//mRigidBody.freezeRotation=true;
	}

	void OnCollisionExit(Collision coll){
		if (coll.gameObject.tag.Equals ("Floor")) {
			mFloorTouched = false;
		}
	//	ViewCamera.transform.localRotation = temp.transform.localRotation;
	//	ViewCamera.transform.eulerAngles=new Vector3(0,0,0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name.Equals ("CoinPrefab")) {
			if(mAudioSource != null && CoinSound != null){
				mAudioSource.PlayOneShot(CoinSound);
			}
			Destroy(other.gameObject);
		}
		if (other.gameObject.name == "WallPrefab") {
			mRigidBody.velocity = Vector3.zero;
		}
	}
}
