using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class playButtonManager : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Transform audioPos;
    
    #region Public Methods

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX(AudioNames.hover, audioPos.position);
    }

    #endregion
}