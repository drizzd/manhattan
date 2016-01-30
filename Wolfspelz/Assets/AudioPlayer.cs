using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {

	public Transform wolf;
	public float distval;

	private AudioSource song;
	public AudioClip songClip;
	private AudioSource song1;
	public AudioClip songClip1;
	private AudioSource song2;
	public AudioClip songClip2;
	private AudioSource song3;
	public AudioClip songClip3;
	private AudioSource song4;
	public AudioClip songClip4;
	private AudioSource song5;
	public AudioClip songClip5;

	public float pitch;
	public float height;
	// Use this for initialization
	void Start () {
		song = gameObject.AddComponent<AudioSource>();
		song.clip = songClip;
		song.Play();
		song1 = gameObject.AddComponent<AudioSource>();
		song1.clip = songClip1;
		song1.Play();
		song2 = gameObject.AddComponent<AudioSource>();
		song2.clip = songClip2;
		song2.Play();
		song3 = gameObject.AddComponent<AudioSource>();
		song3.clip = songClip3;
		song3.Play();
		song4 = gameObject.AddComponent<AudioSource>();
		song4.clip = songClip4;
		song4.Play();
		song5 = gameObject.AddComponent<AudioSource>();
		song5.clip = songClip5;
		song5.Play();

		song.loop = true;
	}
	
	// Update is called once per frame
	void Update () {

		pitch = Vector3.Distance(transform.position,wolf.position)* distval;

		if(pitch<1)
			pitch=1;

		song.pitch = pitch;
		song1.pitch = pitch;
		song2.pitch = pitch;
		song3.pitch = pitch;
		song4.pitch = pitch;
		song5.pitch = pitch;

		if(height<0.2){
			song.volume = (float)(1-height*5);
			song1.volume = (float)(height*5);
			song2.volume = 0;
			song3.volume = 0;
			song4.volume = 0;
			song5.volume = 0;
		}else if(height<0.4){
			song.volume = 0;
			song1.volume = (float)(1-(height-0.2)*5);
			song2.volume = (float)(height-0.2)*5;
			song3.volume = 0;
			song4.volume = 0;
			song5.volume = 0;
		}else if(height<0.6){
			song.volume = 0;
			song1.volume = 0;
			song2.volume = (float)(1-(height-0.4)*5);
			song3.volume = (float)(height-0.4)*5;
			song4.volume = 0;
			song5.volume = 0;
		}else if(height<0.8){
			song.volume = 0;
			song1.volume = 0;
			song2.volume = 0;
			song3.volume = (float)(1-(height-0.6)*5);
			song4.volume = (float)(height-0.6)*5;
			song5.volume = 0;
		}else{
			song.volume = 0;
			song1.volume = 0;
			song2.volume = 0;
			song3.volume = 0;
			song4.volume = (float)(1-(height-0.6)*5);
			song5.volume = (float)(height-0.6)*5;
		}


	}
}
