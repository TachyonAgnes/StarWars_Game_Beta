﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	[Tooltip("The particle effect of hitting something.")]
	public GameObject HitEffect;
	public ParticleSystem Trail;
	public Transform ship;
    public int bulletDmg = 1;

    Transform player;

	//float Damage;

	//float lifetime;

	void Start(){
		StartCoroutine (Lifetime());
	}
	void Awake()
	{
		if(SpaceshipController.instance!=null){
			player = SpaceshipController.instance.transform;
			ship = SpaceshipController.instance.transform.root;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.transform.root != ship){
			if(col.transform.parent==player){
				if(SpaceshipController.instance!=null){
					SpaceshipController.instance.Shake();
				}
			}
            //if (col.GetComponent<DestructionScript> () != null) {
            //	if(SpaceshipController.instance!=null){
            //		col.GetComponent<DestructionScript> ().HP -= SpaceshipController.instance.m_shooting.bulletSettings.BulletDamage;
            //	}
            //}
            //if(col.transform.parent != null){
            //	if (col.transform.parent.GetComponent<SpaceshipController> () != null) {
            //		//col.transform.parent.GetComponent<SpaceshipController> (). HP -= (int)SpaceshipController.instance.m_shooting.bulletSettings.BulletDamage/3;
            //		SpaceshipController.instance.m_spaceship.HP-=(int)SpaceshipController.instance.m_shooting.bulletSettings.BulletDamage/3;
            //	}

            //}
            //if (col.GetComponent<BasicAI> () != null) {
            //	col.GetComponent<BasicAI> ().threat ();
            //}


            bulletDmg = (int)ship.GetChild(0).GetChild(0).gameObject.GetComponent<SpaceshipController>().m_shooting.bulletSettings.BulletDamage;
            if (col.CompareTag("Missile")) {
                return;
            }
            if (col.gameObject.layer == LayerMask.NameToLayer("Agent")) {
                if (col.tag != "Player") {
                    if (col.tag != ship.GetChild(0).GetChild(0).gameObject.GetComponent<player>().teamBelongsTo) {
                        if (col.name == "MotherShip") {
                            col.GetComponent<mothershiphealth>().takedamage(bulletDmg);
                        }
                        else {
                            col.GetComponent<AgentHealth>().takedamage(bulletDmg);
                        }
                    }
                }
                else {
                    if(col.GetComponent<player>().teamBelongsTo != ship.tag) {
                        col.GetComponent<player>().takedamage(bulletDmg);
                    }
                }
            }

            StartCoroutine (DestroySequence (col.gameObject));
		}
	}

	IEnumerator DestroySequence(GameObject other){
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		GetComponent<Renderer> ().enabled = false;
		if(Trail!=null){
			Trail.gameObject.SetActive(false);
		}
        if (HitEffect != null)
        {
            //Debug.Log(transform.GetChild(1).gameObject);
            Vector3 position = transform.GetChild(1).gameObject.transform.position;

            GameObject firework = Instantiate(HitEffect, position, Quaternion.identity);
            firework.transform.parent = gameObject.transform;
            ParticleSystem fireworkPS = GetComponent<ParticleSystem>();
            if (fireworkPS != null) fireworkPS.Play();
            //firework.GetComponent<ParticleSystem>().Play();

            //ParticleSystem ps=gameObject.AddComponent<ParticleSystem>();
            //ps = HitEffect;
            //Debug.Log(ps);
            // GameObject[] childrens =  GetComponents<GameObject>();
            // foreach (var c in childrens) Debug.Log(c.name);
            //  HitEffect.Play();
            yield return new WaitForSeconds(1f);
            Destroy(firework);
        }

        /*
        if (HitEffect != null && other.GetComponent<DestructionScript>() !=  null) {
			if(other.GetComponent<DestructionScript>().Asteroid){			
				HitEffect.Play ();
			}
		}
        */

        
		Destroy (gameObject);
       
	}

	IEnumerator Lifetime(){
		if(SpaceshipController.instance!=null){
			yield return new WaitForSeconds (SpaceshipController.instance.m_shooting.bulletSettings.BulletLifetime);
		}
		Destroy (gameObject);
	}

}
