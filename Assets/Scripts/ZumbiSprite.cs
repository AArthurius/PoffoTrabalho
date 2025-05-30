using UnityEngine;

public class ZumbiSprite : MonoBehaviour
{

    public GameObject attackHitbox;

    [SerializeField] private Zumbi zumbi; 
    [SerializeField] private Animator anim;

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
