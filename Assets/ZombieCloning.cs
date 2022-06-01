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

    private bool isNetworked;
    private PhotonView v;
    private void Awake()
    {
        if (FindObjectOfType<NetworkManager>() == null)
            isNetworked = false;
        else
        {
            v = GetComponent<PhotonView>();
            isNetworked = true;
        }
    }

    public void Clone()
    {
        if(clone == null)
        {
            if(isNetworked)
            {
                v.RPC("CloneRPC", RpcTarget.All);
            }
            else
            {
                Instantiate(clone, clonePos);
            }
        }
    }

    [PunRPC]
    private void CloneRPC()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.Instantiate("MeetingroomAI", clonePos.position, clonePos.rotation);
    }


}
