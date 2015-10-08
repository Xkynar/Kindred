using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private enum GameState {
        WAIT_TURN,
        PICK_MONSTER,
        SELECT_ACTION,
        TARGET_MONSTER,
        WAIT_ACTION
    }

    private GameState currentState;

	// Use this for initialization
	void Start ()
    {
        currentState = GameState.WAIT_TURN;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void monsterClicked(MonsterController monster, bool mine)
    {
        switch(currentState)
        {
            case GameState.WAIT_TURN:
                return;

            case GameState.PICK_MONSTER:
                //highlight and show attack hud
                break;
            
            //case GameState.SELECT_ACTION:

        }
    }
}
