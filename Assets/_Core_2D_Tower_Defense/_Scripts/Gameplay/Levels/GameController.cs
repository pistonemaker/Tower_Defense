using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameController : Singleton<GameController>
{
    public TowerPosition curTowerPosition;
    public Tower curTower;
    public BuildMenu curBuildMenu;
    public UpgradeMenu curUpgradeMenu;
    public bool canClickTower = true;
    public bool canClickTowerPosition = true;

    public AttackRangeIndicator rangeIndicator;
    public SpiritStoneIndicator spiritStoneIndicator;
    public Transform indicatorParent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUIElement())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.transform.CompareTag("TowerPos"))
                {
                    var towerPosition = hit.collider.GetComponent<TowerPosition>();
                    
                    if (towerPosition.tower)
                    {
                        if (towerPosition.tower != curTower)
                        {
                            HideCurrentUpgradeMenu(curTower);
                        }
                        
                        canClickTower = false;
                        HideCurrentBuildMenu(curTowerPosition);
                        HandleTowerClick(towerPosition.tower);
                        return;
                    }
                }
            }
            
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.transform.CompareTag("TowerPos"))
                {
                    var towerPosition = hit.collider.GetComponent<TowerPosition>();
                    
                    if (towerPosition)
                    {
                        if (towerPosition != curTowerPosition)
                        {
                            HideCurrentBuildMenu(curTowerPosition);
                        }
                        
                        canClickTowerPosition = false;
                        HideCurrentUpgradeMenu(curTower);
                        HandleTowerPositionClick(towerPosition);
                        return;
                    }
                }
            }
            
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    var towerPosition = hit.collider.GetComponent<TowerPosition>();
                    var toweCollider = hit.collider.GetComponent<TowerCollider>();

                    if (towerPosition)
                    {
                        continue;
                    }

                    if (toweCollider)
                    {
                        HideCurrentBuildMenu(curTowerPosition);
                        HideCurrentUpgradeMenu(curTower);
                        return;
                    }
                }
            }

            if (hits.Length == 0)
            {
                HideCurrentBuildMenu(curTowerPosition);
                HideCurrentUpgradeMenu(curTower);
            }
        }
    }

    private void HandleTowerClick(Tower tower)
    {
        curTower = tower;
        tower.towerPosition.ShowUpgradeMenu();

        // Hiển thị tầm đánh của tháp
        rangeIndicator.ShowIndicator(tower.transform.position, tower.shootingRange);
    }

    private void HandleTowerPositionClick(TowerPosition towerPosition)
    {
        curTowerPosition = towerPosition;
        towerPosition.ShowBuildMenu();

        // Ẩn tầm đánh khi click vào vị trí xây tháp
        rangeIndicator.HideIndicator();
    }

    private void HideCurrentUpgradeMenu(Tower tower)
    {
        if (tower != null)
        {
            tower.towerPosition.HideUpgradeMenu();
            curTower = null;
        }

        // Ẩn tầm đánh khi click vào vị trí xây tháp
        rangeIndicator.HideIndicator();
    }

    private void HideCurrentBuildMenu(TowerPosition towerPosition)
    {
        if (towerPosition != null)
        {
            towerPosition.HideBuildMenu();
            curTowerPosition = null;
        }
    }

    // Kiểm tra xem người chơi có click vào UI hay không 
    private bool IsMouseOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        // Kiểm tra results có chứa ít nhất một kết quả từ raycasting hay không. Nếu có, có nghĩa là chuột
        // đang nằm trên một phần tử UI, hàm trả về true. Nếu danh sách rỗng, hàm trả về false,
        // cho biết con trỏ chuột đang nằm trên các Game Object thay vì các phần tử UI.
        return results.Count > 0;
    }
}