using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool isAttacking = false; 
    //private float timeToAttack = 1f;
    //private float timer = 0f;
    private Animator amt;
    public float attackCooldown = 1f; 
    public int damage =20;
    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        amt = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if ( attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }else if(attackCooldown==0 ){

        }

       // if (isAttacking)
        //{
        ///    timer += Time.deltaTime;
          //  if (timer >= timeToAttack)
          //  {
           //     timer = 0;
           //     isAttacking = false;
           //     attackArea.SetActive(isAttacking);
           //     amt.SetBool("isAtk", false); // Set the boolean parameter to false
           // }
        }
    

   public void Attack(GameObject target)
{
    
    amt.SetTrigger("isAttack"); // Set the boolean parameter to true
    // Tìm và lấy component Health của đối tượng bị tấn công
    Health targetHealth = target.GetComponent<Health>();
    if (targetHealth != null)
    {
        // Gọi hàm Damage() trên component Health của đối tượng bị tấn công
        targetHealth.TakeDamage(damage);
    }
}
}