using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [Header("Other script references")]
    [SerializeField]
    private UIManager uIManager;

    [SerializeField]
    private InteractBehaviour interactBehaviour;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Equipment equipmentSystem;

    [SerializeField]
    private float attackRange;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Vector3 attackOffset;

    private bool isAttacking;
    void Update()
    {
        // Debug.DrawRay(transform.position + attackOffset, transform.forward * attackRange, Color.red);

        if (Input.GetMouseButtonDown(0) && CanAttack())
        {
            isAttacking = true;
            SendAttack();
            animator.SetTrigger("Attack");
        }
    }

    void SendAttack()
    {
        Debug.Log("Attack sent");

        RaycastHit hit;

        if(Physics.Raycast(transform.position + attackOffset, transform.forward, out hit, attackRange,layerMask ))
        {
            if(hit.transform.CompareTag("AI"))
            {
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                enemy.TakeDamage(equipmentSystem.equipedWeaponItem.attackPoints);
            }
        }
    }

    bool CanAttack()
    {
        return equipmentSystem.equipedWeaponItem != null && !isAttacking && !uIManager.atLeastOnePanelOpen && !interactBehaviour.isBusy;
    }
    public void AttackFinished()
    {
        isAttacking = false;
    }
}
