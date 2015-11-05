using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour
{
    public float runningSpeed;
    public float turningSpeed;
    public SkinnedMeshRenderer meshRenderer;

    private string monsterName;
    private Animator animator;
    private MonsterHealth health;
    private bool isMine = false;

    public BaseAttack[] attacks;

    private Color initialOutline;

    void Start()
    {
        monsterName = this.gameObject.name;
        animator = this.GetComponent<Animator>();
        health = this.GetComponent<MonsterHealth>();

        if (meshRenderer != null)
        {
            initialOutline = meshRenderer.material.GetColor("_OutlineColor");
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
            meshRenderer.material.SetColor("_OutlineColor", Color.cyan);
        }
    }

    /*
     * Returns a monster's outline back to its initial state.
     */
    public void Deselect()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_OutlineColor", initialOutline);
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
    public void Attack(string attackName, MonsterController targetMonster)
    {
        StartCoroutine(attacks[0].routine(this, targetMonster));
    }

    /*
     * Handled separately in the event that something else needs to occur.
     */
    public void GetHit()
    {
        animator.SetTrigger("Hit");
    }
}