using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ZombieCloning : MonoBehaviour
{
    [SerializeField] Mesh zombieMesh;
    [SerializeField] Mesh humanMesh;
    [SerializeField] Material zombieMat;
    [SerializeField] Material humanMat;
    [SerializeField] GameObject clone;
    [SerializeField] Transform clonePos;
    [SerializeField] GameObject tube;
    [SerializeField] Vector3 endPos;
    [SerializeField] float speed;

    [SerializeField] private GameObject currentClone;
    private bool isNetworked;
    private Coroutine _animationCor;
    private bool isOpen = false;
    private bool isReady = false;
    private PhotonView v;
    Vector3 initialPos;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
        {
            v = GetComponent<PhotonView>();
            isNetworked = true;
        }
        initialPos = tube.transform.position;
    }

    public void Clone()
    {
        if(currentClone == null)
        {
            if(isNetworked)
            {
                v.RPC("CloneRPC", RpcTarget.All);
            }
            else
            {
                currentClone = Instantiate(clone, clonePos);
            }
        }
    }

    public void ChangeMats()
    {
        if (currentClone != null)
        {
            if (isNetworked)
            {
                v.RPC("ChangeMatsRPC", RpcTarget.All);
            }
            else
            {
                ChangeMatsRPC();
            }
            isReady = true;
        }
    }

    public void Activate()
    {
        if (isReady)
        {
            print("acitvate");
            if (isNetworked)
                v.RPC("ActivateRPC", RpcTarget.All);
            else
                ActivateRPC();
            isReady = false;
        }
    }
    public void ResetTube()
    {
        if (isNetworked)
            v.RPC("ResetRPC", RpcTarget.All);
        else
            ResetRPC();
    }
    [PunRPC]
    private void ResetRPC()
    {
        Close();
    }

    [PunRPC]
    private void ActivateRPC()
    {
        Open();
    }

    [PunRPC]
    private void ChangeMatsRPC()
    {
        SkinnedMeshRenderer[] renderers = currentClone.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer rend in renderers)
        {
            if (rend.enabled)
            {
                rend.material = humanMat;
                rend.sharedMesh = humanMesh;
            }
        }
    }

    [PunRPC]
    private void CloneRPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject cl = PhotonNetwork.Instantiate("MeetingroomAI", clonePos.position, clonePos.rotation);
            v.RPC("SetCurrentClone", RpcTarget.All, cl.GetComponent<PhotonView>().ViewID);
        }
    }
    [PunRPC]
    private void SetCurrentClone(int id)
    {
        currentClone = PhotonView.Find(id).gameObject;
    }
    private void Open()
    {
        if (!isOpen)
        {
            print("activate open");
            
            if (_animationCor != null)
            {
                StopCoroutine(_animationCor);
            }
            _animationCor = StartCoroutine(OpenSlide());
            isOpen = true;
        }  
    }

    private IEnumerator OpenSlide()
    {
        print("activate slide");
        float time = 0;
        while (time < 1)
        {
            Vector3 startPos = tube.transform.position;
            float y = Mathf.Lerp(startPos.y, endPos.y, time);
             tube.transform.position = new Vector3(tube.transform.position.x, y, tube.transform.position.z);
             yield return null;
             time += Time.deltaTime * speed;
        }
        currentClone.GetComponent<AIController>().enabled = true;
    }
    private void Close()
    {
        if (isOpen)
        {
            print("acitvate close");
            if (_animationCor != null)
            {
                StopCoroutine(_animationCor);
            }
            _animationCor = StartCoroutine(CloseSlide());
            isOpen = false;
        }
        currentClone = null;
    }
    private IEnumerator CloseSlide()
    {
        float time = 0;
        while (time < 1)
        {
            Vector3 startPos = tube.transform.position;
            float y = Mathf.Lerp(startPos.y, initialPos.y, time);
            tube.transform.position = new Vector3(tube.transform.position.x, y, tube.transform.position.z);
            yield return null;
            time += Time.deltaTime * speed;
        }
        
    }
}
