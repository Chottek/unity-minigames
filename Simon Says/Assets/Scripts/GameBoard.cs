using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour {

    private const string GRID_NAME = "ButtonGrid";
    private const int GRID_SIZE = 9;

    [SerializeField]
    public int startSequenceLength = 1;

    [SerializeField]
    public float sequenceDelay = 1f;

    private int[] currentSequence;
    private int currentSequenceLength;

    //[SerializeField]
    //private List<GameButton> buttons;

    [SerializeField]
    private GameButton[] buttons;

    private GameButton nextButton;

    private PlayerHandler playerHandler;

    void Awake(){
        playerHandler = GetComponent<PlayerHandler>();

        playerHandler.SetCanClick(true); //change to false later! <- state before pressing start 
        playerHandler.SetCanType(false); //change to true later!

        buttons = GetComponentsInChildren<GameButton>();
      
        Debug.Log("Buttonz size:" + buttons.Length);
    }

    private void Start() {
        StartGame();
    }

    private void GenerateSequence(){
        currentSequence = new int[currentSequenceLength];
        for (int i = 0; i < currentSequenceLength; i++){
           currentSequence[i] = Random.Range(0, buttons.Length);
           Debug.Log("Generated: " + i + " : " + currentSequence[i]);
        }
    }

    private void StartGame(){
        currentSequenceLength = startSequenceLength;
        StartCoroutine(PlaySequenceRoutine());
    }

    private IEnumerator PlaySequenceRoutine(){
        yield return new WaitForSeconds(sequenceDelay);
        playerHandler.SetCanClick(false);
        GenerateSequence();

        for (int i = 0; i < currentSequenceLength; i++){
            nextButton = buttons[currentSequence[i]];
            yield return StartCoroutine(nextButton.PlayBlinkRoutine());
        }
    }

}
