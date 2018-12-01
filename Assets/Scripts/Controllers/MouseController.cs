using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    private Ray2D ray;
    private RaycastHit2D hit;
    private MinionInterface minionInterface;

    private void Start()
    {
        minionInterface = FindObjectOfType<MinionInterface>();
    }

    public void Update()
    {
        //If we click left mouse button and we aren't hovering over UI
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Check if we clicked on a minion
            if(hit.transform?.GetComponent<Minion>() != null)
            {
                var minion = hit.transform.GetComponent<Minion>();
                minionInterface.SelectMinion(minion);
            }
            //We didnt hit a minion
            else
            {
                minionInterface.DeselectMinion();
            }
        }
    }
}

