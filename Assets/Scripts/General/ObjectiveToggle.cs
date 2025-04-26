using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectiveToggle : MonoBehaviour
{
    public UIDocument uiDocument;

    private VisualElement root;
    private VisualElement objectivesMenu;

    private bool isVisible = true;
    private Vector2 visiblePosition;
    private Vector2 hiddenPosition;

    private readonly float marginRight = 10f;
    private readonly float marginTop = 10f;
    private float animationDuration = 0.3f;

    void Awake()
    {
        root = uiDocument.rootVisualElement;
        objectivesMenu = root.Q<VisualElement>("ObjectiveBoard");

        objectivesMenu.style.position = Position.Absolute;

        root.RegisterCallback<GeometryChangedEvent>(OnLayoutChanged);
    }

    private void OnLayoutChanged(GeometryChangedEvent evt)
    {
        float parentWidth = evt.newRect.width;
        float menuWidth = objectivesMenu.layout.width;

        visiblePosition = new Vector2(parentWidth - menuWidth - marginRight, marginTop);
        hiddenPosition = new Vector2(parentWidth + marginRight, marginTop);

        var startPos = isVisible ? visiblePosition : hiddenPosition;
        objectivesMenu.style.left = startPos.x;
        objectivesMenu.style.top = startPos.y;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        StopAllCoroutines();

        Vector2 from = isVisible ? visiblePosition : hiddenPosition;
        Vector2 to = isVisible ? hiddenPosition : visiblePosition;
        StartCoroutine(AnimatePosition(from, to));

        isVisible = !isVisible;
    }

    private IEnumerator AnimatePosition(Vector2 start, Vector2 end)
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / animationDuration);

            objectivesMenu.style.left = Mathf.Lerp(start.x, end.x, t);
            objectivesMenu.style.top = Mathf.Lerp(start.y, end.y, t);

            yield return null;
        }

        objectivesMenu.style.left = end.x;
        objectivesMenu.style.top = end.y;
    }
}
