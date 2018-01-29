using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorNetwork : Doctor_AIScript {

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        ParticleSystemRenderer ren = GetComponentInChildren<ParticleSystemRenderer>();

        if (stream.isWriting)
        {
            Vector3 targ = (targetNPC == null ? gameObject.transform.position :targetNPC.transform.position);
            stream.Serialize(ref targ);
        }
        else
        {

            Vector3 targ = Vector3.zero;
            stream.Serialize(ref targ);

            if (path == null)
            {
                path = GetComponent<LineRenderer>();
            }
            path.SetPositions(new Vector3[] { gameObject.transform.position + Vector3.back, targ + Vector3.back });

        }
    }


    // Update is called once per frame
    override public void Update()
    {
        if (PhotonNetwork.isMasterClient)
        {
            if (targetNPC != null)
            {

                if (distance < 2f)
                {
                    targetNPC.Uninfect();
                    ChooseNewTarget();
                }

                UpdateTargetLocation();

                gameObject.transform.Translate(direction * Time.deltaTime * 7);

                UpdateMovement();
            }
            else
            {

                ChooseNewTarget();

            }

        }

    }
}
