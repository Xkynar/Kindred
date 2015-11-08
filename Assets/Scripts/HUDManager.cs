using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

    public static HUDManager Instance;

    [SerializeField] GameObject readyButton;
    [SerializeField] GameObject attacksGroup;
    [SerializeField] Text manaValue;
    [SerializeField] Slider manaSlider;
    [SerializeField] Text[] attackSlots; //parent is button
    [SerializeField] GameObject miniCamera;
    [SerializeField] TextMesh arenaHint;

    private int selectedAttackIndex;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitMana(float initialMana, float maxMana)
    {
        manaSlider.maxValue = maxMana;
        manaSlider.value = initialMana;
        manaValue.text = initialMana.ToString();
    }

    public void UpdateMana(float value)
    {
        manaSlider.value = value;
        manaValue.text = value.ToString();
    }


    /*
    * Displays a "READY" button on the main HUD. Called by the local player.
    */
    public void DisplayReadyButton()
    {
        this.readyButton.SetActive(true);
    }

    /*
     * Hide "READY" button.
     */
    public void HideReadyButton()
    {
        this.readyButton.SetActive(false);
    }

    /*
     * Callback for the "READY" button clicked event. Sets ownership of all visible monsters and alerts the server.
     */
    public void OnReadyButtonClick()
    {
        ClientManager.Instance.ReadyUp();
    }

    /*
     * Opens attack UI with the correct attack names.
     */
    public void OpenAttackUI(BaseAttack[] attacks)
    {
        attacksGroup.SetActive(true);
        
        // hide all attacks
        foreach (Text attackSlot in attackSlots)
        {
            attackSlot.transform.parent.gameObject.SetActive(false);
        }

        // display the attacks
        for (int i=0; i<attacks.Length; i++)
        {
            Text attackSlot = attackSlots[i];
            attackSlot.text = attacks[i].GetName() + " (" + attacks[i].GetManaCost() + " Mana)";
            attackSlot.transform.parent.gameObject.SetActive(true);

            if (ClientManager.Instance.GetCurrentMana() < attacks[i].GetManaCost())
            {
                attackSlot.transform.parent.GetComponent<Button>().interactable = false;
            }
            else
            {
                attackSlot.transform.parent.GetComponent<Button>().interactable = true;
            }
        }
    }

    /*
     * Handles the click of each attack in the HUD.
     */
    public void AttackButtonHandler(int index)
    {
        selectedAttackIndex = index;
        ClientManager.Instance.SetGameState(GameState.TARGET_MONSTER);
    }

    /*
     * Hides the attack UI.
     */
    public void CloseAttackUI()
    {
        attacksGroup.SetActive(false);
    }

    /*
     * Returns the selected attack.
     */
    public int GetSelectedAttackIndex()
    {
        return selectedAttackIndex;
    }

    /*
     * Sets the HUD mini-camera as a child of a transform. Used to attach the camera to a monster.
     */
    public void SetMiniCamera(Transform anchor)
    {
        miniCamera.transform.parent = anchor;
        miniCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
        miniCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        miniCamera.SetActive(true);
    }

    /*
     * Hides the HUD mini-camera;
     */
    public void ResetMiniCamera()
    {
        miniCamera.SetActive(false);
    }

    /*
     * Display a hint in the arena.
     */
    public void DisplayArenaHint(string hint)
    {
        arenaHint.text = hint;
    }
}
