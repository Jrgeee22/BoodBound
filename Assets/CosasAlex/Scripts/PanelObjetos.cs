using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelObjetos : MonoBehaviour
{
    public GameObject panelObj;
    public GameObject panelEnemy;
    void Update()
    {
        if (panelObj != null && panelObj.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanelObj();
        }
        if (panelEnemy != null && panelEnemy.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanelEnemy();
        }
    }
    public void OpenPanelObj()
    {
        if (panelObj != null)
        {
            panelObj.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void OpenPanelEnemy()
    {
        if (panelEnemy != null)
        {
            panelEnemy.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ClosePanelObj()
    {
        if (panelObj != null)
        {
            panelObj.SetActive(false);
        }
    }
    public void ClosePanelEnemy()
    {
        if (panelEnemy != null)
        {
            panelEnemy.SetActive(false);
        }
    }
    public void EscenaShooter()
    {
        SceneManager.LoadScene("HuginhoDedeadorDeVenecos");
    }
}
