using UnityEngine;

public class ZumbiSprite : MonoBehaviour
{

    public GameObject attackHitbox;

    [SerializeField] Zumbi zumbi; 
    [SerializeField] Animator anim;

    public void enableAttackHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void disableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }

    public void resetar()
    {

        zumbi.stop = 1;
        anim.Play("move");
    }
}
