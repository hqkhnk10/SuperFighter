using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUniversal : MonoBehaviour
{
    public LayerMask collisionLayer;
    public float radius = 1f;
    public float damage = 2f;
    public GameObject hit_FX_Prefab, block_FX_Prefab;
    private Animator anim;
    void Update()
    {
        DetectCollision();
    }
    void DetectCollision() {

    Collider[] hit = Physics.OverlapSphere(transform.position, radius, collisionLayer);
        if (hit.Length > 0) {
                anim = hit[0].GetComponentInChildren<Animator>();
                Vector3 hitFX_Pos = transform.position;
            if (Equals(anim.ToString(),"Batman"))
            {
                hitFX_Pos.x += radius;
            }
            else
            {
                hitFX_Pos.x -= radius;
            }
                bool block = anim.GetBool(AnimationTags.BLOCK);
                if (block)
                {
                    Instantiate(block_FX_Prefab, transform.position, Quaternion.identity);
                    hit[0].GetComponent<HealthScript>().ApplyDamage(1, false, block);
                }
                else
                {
                    Instantiate(hit_FX_Prefab, transform.position, Quaternion.identity);
                    if (gameObject.CompareTag(Tags.RIGHT_ARM_TAG) || gameObject.CompareTag(Tags.RIGHT_LEG_TAG))
                    {
                        hit[0].GetComponent<HealthScript>().ApplyDamage(damage, true, block);
                    }
                    else
                    {
                        hit[0].GetComponent<HealthScript>().ApplyDamage(damage, false, block);
                    }
                }

            gameObject.SetActive(false);
        }
    }
}
