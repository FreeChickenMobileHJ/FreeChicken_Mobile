using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombine : MonoBehaviour
{

    void Start()
    {
        CombineInstance[] combine = new CombineInstance[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            // 각 자식 오브젝트의 MeshFilter 정보를 가져오기
            MeshFilter childMeshFilter = transform.GetChild(i).GetComponent<MeshFilter>();

            // 복제된 Mesh를 만들어서 CombineInstance에 할당
            combine[i].mesh = Instantiate(childMeshFilter.sharedMesh);

            // 각 자식 오브젝트의 로컬 트랜스폼을 설정하여 원래의 위치에 유지
            combine[i].transform = childMeshFilter.transform.localToWorldMatrix;

            // 현재 자식 오브젝트 비활성화 (필요에 따라 활성화 상태를 유지할 수 있음)
            transform.GetChild(i).gameObject.SetActive(false);
        }

        // 현재 오브젝트에 MeshFilter 및 MeshRenderer 추가
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        // 수동으로 Mesh를 병합
        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine, true, true);

        meshFilter.mesh = combinedMesh;

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // 원래 오브젝트에 있는 재질 설정
        meshRenderer.sharedMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;

        // MeshCollider Combine를 통해 병합된 메시에 맞게 자동으로 설정
        MeshCollider meshColliderOnCombinedObject = gameObject.AddComponent<MeshCollider>();
        meshColliderOnCombinedObject.sharedMesh = combinedMesh;
    }
}





