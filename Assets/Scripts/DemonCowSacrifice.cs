using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCowSacrifice : MonoBehaviour {
    public GameObject SacrificeTarget;

    private float sacrificeDelay = 3;
    private bool leaving = false;
    private Vector3 sacrificeTargetInitialPosition;

    void Start () {
		
	}
	
	void Update () {
        if (SacrificeTarget == null) {
            return;
        }
        if (sacrificeTargetInitialPosition == Vector3.zero) {
            sacrificeTargetInitialPosition = SacrificeTarget.transform.position;
        }

        sacrificeDelay -= Time.deltaTime;
        var animator = GetComponent<Animator>();
        var sacrificeAnimator = SacrificeTarget.GetComponent<Animator>();
        var animations = animator.runtimeAnimatorController.animationClips;

        if (leaving) {
            SacrificeTarget.transform.LookAt(transform);
            SacrificeTarget.transform.position = Vector3.MoveTowards(
                SacrificeTarget.transform.position,
                transform.position, 0.05f);
            if (isPlaying(animator) || isPlaying(sacrificeAnimator)) {
                return;
            }
            Destroy(SacrificeTarget);
            Destroy(gameObject);
        } else if (sacrificeDelay <= 0) {
            animator.Play("Despawn");
            sacrificeAnimator.Play("Sacrifice");
            leaving = true;
        } else {
            SacrificeTarget.transform.LookAt(transform);
            SacrificeTarget.transform.position = sacrificeTargetInitialPosition;
        }
    }

    bool isPlaying(Animator animator) {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
