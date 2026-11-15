using UnityEngine;

public class PaginasEnemy : MonoBehaviour
{
    public GameObject[] paginas;
    public int currentIndex = 0;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UpdatePaginas();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPagina();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousPagina();
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void NextPagina()
    {
        if (currentIndex < paginas.Length - 1)
        {
            currentIndex++;
            UpdatePaginas();
        }
    }
    public void PreviousPagina()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdatePaginas();
        }
    }
    void UpdatePaginas()
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            if (paginas[i] != null)
            {
                paginas[i].SetActive(i == currentIndex);
            }
        }
    }
}