using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float runningSpeed;
    public float turningSpeed;
    public SkinnedMeshRenderer meshRenderer;
    public BaseAttack[] attacks;

    private string monsterName;
    private Animator animator;
    private MonsterHealth health;
    private bool isMine = false;
    private bool isAlive = true;
    private Color initialColor;
    private Color selectionColor = Color.cyan;
    private Color targetColor = Color.red;
    private Transform miniCameraPos;

    void Start()
    {
        monsterName = this.gameObject.name;
        animator = this.GetComponent<Animator>();
        health = this.GetComponent<MonsterHealth>();
        miniCameraPos = transform.Find("MiniCameraPos");

        foreach(BaseAttack attack in attacks)
        {
            attack.Init();
        }

        if (meshRenderer != null)
        {
            initialColor = meshRenderer.material.GetColor("_OutlineColor");
        }
    }

    /*
     * Sets monster ownership relative to the local player.
     */
    public void SetMine(bool isMine)
    {
        this.isMine = isMine;
    }

    /*
     * Returns true if monster belongs to local player.
     */
    public bool IsMine()
    {
        return isMine;
    }

    /*
     * 
     */
    public bool IsAlive()
    {
        return isAlive;
    }

    /*
     * 
     */
    public BaseAttack[] GetAttacks()
    {
        return attacks;
    }

    /*
     * 
     */
    public BaseAttack GetAttack(int index)
    {
        return attacks[index];
    }

    /*
     * Returns monster's unique name.
     */
    public string GetMonsterName()
    {
        return monsterName;
    }

    /*
     * Changes the outline of a monster to indicate it is now selected.
     */
    public void Select()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_OutlineColor", selectionColor);
        }

        if (miniCameraPos != null)
        {
            HUDManager.Instance.SetMiniCamera(miniCameraPos);
        }
    }

    /*
     * Returns a monster's outline back to its initial state.
     */
    public void Deselect()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_OutlineColor", initialColor);
        }

        HUDManager.Instance.ResetMiniCamera();
    }

    /*
     * Changes the outline of a monster to indicate it is now targeted.
     */
    public void Target()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_OutlineColor", targetColor);
        }
    }

    /*
     * Event triggered on mouse click (depends on the presence of a collider).
     */
    void OnMouseDown()
    {
        ClientManager.Instance.OnMonsterClick(this);
    }

    /*
     * Initiates an attack coroutine.
     */
    public void Attack(MonsterController targetMonster, int attackIndex)
    {
        StartCoroutine(attacks[attackIndex].Routine(this, targetMonster));
    }

    /*
     * Handled separately in the event that something else needs to occur.
     */
    public void GetHit(float damage)
    {
        health.TakeDamage(damage);
        
        if(health.GetHealth() <= 0)
        {
            animator.SetTrigger("Death");
            isAlive = false;
        }

        animator.SetTrigger("Hit");
    }
}