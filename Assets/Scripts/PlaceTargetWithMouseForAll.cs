using UnityEngine;


namespace UnityStandardAssets.SceneUtils
{
    public class PlaceTargetWithMouseForAll : MonoBehaviour
    {
        public float surfaceOffset = 1.5f;
        public GameObject setTargetOn;
        public GameObject[] creeps;

        // Update is called once per frame
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            transform.position = hit.point + hit.normal * surfaceOffset;
            if (setTargetOn != null)
            {

                creeps = GameObject.FindGameObjectsWithTag(setTargetOn.tag);

                foreach (GameObject creep in creeps)
                {
                    creep.SendMessage("SetTarget", transform);
                }



            }
        }
    }
}
