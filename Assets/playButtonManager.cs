using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class playButtonManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private TitleManager tm;
    
    #region Public Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        tm.PlayHoverAudio();
    }

    #endregion
}