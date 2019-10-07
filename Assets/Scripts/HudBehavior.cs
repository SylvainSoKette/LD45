using UnityEngine;
using UnityEngine.UI;


public class HudBehavior : MonoBehaviour
{
    public GameObject tooltip;

    public GameObject board;
    private BoardBehavior boardBehavior;

    public GameObject ressources;
    private bool alignLeft;

    public GameObject resTime;
    private Text resTimeText;
    public GameObject resDirt;
    private Text resDirtText;
    public GameObject resClay;
    private Text resClayText;
    public GameObject resStick;
    private Text resStickText;
    public GameObject resBrick;
    private Text resBrickText;
    public GameObject resStone;
    private Text resStoneText;
    public GameObject resLeaf;
    private Text resLeafText;
    public GameObject resFruit;
    private Text resFruitText;
    public GameObject resFood;
    private Text resFoodText;

    public Color normalColor;
    public Color fullColor;

    void Start()
    {
        alignLeft = true;

        boardBehavior = board.GetComponent<BoardBehavior>();

        resTimeText = resTime.GetComponent<Text>();
        resDirtText = resDirt.GetComponent<Text>();
        resClayText = resClay.GetComponent<Text>();
        resStickText = resStick.GetComponent<Text>();
        resBrickText = resBrick.GetComponent<Text>();
        resStoneText = resStone.GetComponent<Text>();
        resLeafText = resLeaf.GetComponent<Text>();
        resFruitText = resFruit.GetComponent<Text>();
        resFoodText = resFood.GetComponent<Text>();
    }

    void Update()
    {
        // Tooltip location update
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            tooltip.transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out localPoint
        );
        Vector2 offset = new Vector2(-80, 80);
        localPoint += offset;
        tooltip.transform.localPosition = localPoint;

        // ressource panel left or right
        if (Input.mousePosition.x > (2*Screen.width/3) && !alignLeft) {
            alignLeft = true;
            ressources.GetComponent<RectTransform>().anchoredPosition = new Vector3(200, 0, 0);
        } else if (Input.mousePosition.x <= (Screen.width/3) && alignLeft) {
            alignLeft = false;
            ressources.GetComponent<RectTransform>().anchoredPosition = new Vector3(960, 0, 0);
        }

        // ressource panel updates
        resTimeText.text = boardBehavior.GetDays() + " days";
        UpdateRessourceText(ressource.DIRT, resDirtText, " soil");
        UpdateRessourceText(ressource.CLAY, resClayText, " clay");
        UpdateRessourceText(ressource.STICK, resStickText, " stick");
        UpdateRessourceText(ressource.BRICK, resBrickText, " brick");
        UpdateRessourceText(ressource.STONE, resStoneText, " stone");
        UpdateRessourceText(ressource.LEAF, resLeafText, " leaf");
        UpdateRessourceText(ressource.FRUIT, resFruitText, " fruit");
        UpdateRessourceText(ressource.FOOD, resFoodText, " food");
    }

    private void UpdateRessourceText(ressource res, Text txt, string msg) {
        txt.text = boardBehavior.GetRessource(res) + msg;
        if (boardBehavior.GetRessource(res) < boardBehavior.GetMaxRessources()) {
            txt.color = normalColor;
        } else {
            txt.color = fullColor;
        }
    }
}
