using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonCowSacrifice : MonoBehaviour {
    public GameObject SacrificeTarget;
    public bool sacrificingPlayer = false;

    private float sacrificeDelay = 3;
    private bool leaving = false;
    private Vector3 sacrificeTargetInitialPosition;

    void Start() {

    }

    void Update() {
        if (SacrificeTarget == null) {
            return;
        }
        if (sacrificeTargetInitialPosition == Vector3.zero) {
            sacrificeTargetInitialPosition = SacrificeTarget.transform.position;
            if (!sacrificingPlayer) {
                var rigidBody = SacrificeTarget.GetComponent<Rigidbody>();
                rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        sacrificeDelay -= Time.deltaTime;
        var animator = GetComponent<Animator>();
        var sacrificeAnimator = SacrificeTarget.GetComponent<Animator>();
        var animations = animator.runtimeAnimatorController.animationClips;

        if (leaving) {
            if (!sacrificingPlayer) {
                SacrificeTarget.transform.LookAt(transform);
                SacrificeTarget.transform.position = Vector3.MoveTowards(
                    SacrificeTarget.transform.position,
                    transform.position, 0.05f);
                if (isPlaying(animator) || isPlaying(sacrificeAnimator)) {
                    return;
                }
                Destroy(SacrificeTarget);
                Destroy(gameObject);
            }
        } else if (sacrificeDelay <= 0) {
            if (!sacrificingPlayer) {
                animator.Play("Despawn");
            }
            sacrificeAnimator.Play("Sacrifice");
            leaving = true;
        } else if (!sacrificingPlayer) {
            SacrificeTarget.transform.LookAt(transform);
            SacrificeTarget.transform.position = sacrificeTargetInitialPosition;
        }
    }

    bool isPlaying(Animator animator) {
        return animator.GetCurrentAnimatorStateInfo(0).length >
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
