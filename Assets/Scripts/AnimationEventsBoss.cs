using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventsBoss : MonoBehaviour
{
    [SerializeField] private Boss_01 _boss_01;

    // Start is called before the first frame update
    void Update()
    {
       
    }

    public void AddAttack()
    {
        Debug.Log("Activando ataque");
        _boss_01.isAttacking = true;
        int randomAttack = Random.Range(0, 10);
        if(randomAttack < 7)
        {
            _boss_01.fireAttack();
        }
        else
        {
            _boss_01.fireWallAttack();
        }
    }
    public void DisableAnimation()
    {
        Debug.Log("Desactivando ataque");
        gameObject.GetComponent<Animator>().ResetTrigger("Attack");
        gameObject.GetComponent<Animator>().SetTrigger("NoAttack");

        //gameObject.GetComponent<Animator>().SetBool("IsAttack", false);
    }
}
