using UnityEngine;
using Assets.Scripts.IAJ.Unity.Util;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicWander : DynamicSeek
    {
        public DynamicWander()
        {
            this.Target = new KinematicData();
        }
        public override string Name
        {
            get { return "Wander"; }
        }
        public float TurnAngle { get; set; }
        public float WanderOffset { get; set; }
        public float WanderRadius { get; set; }
        public float WanderRate { get; set; }
        protected float WanderOrientation { get; set; }
        public Vector3 CircleCenter { get; private set; }
        public GameObject DebugTarget { get; set; }


        public override MovementOutput GetMovement()
        {   	
            /*PSEUDOCODE
            wanderAngle += randomBinomial() * wanderRate
            targetOrientation = wanderAngle + character.orientation

            //O2V is short for OrientationToVector, converts angle into vector
            circleCenter = character.position + wanderOffset * O2V(character.orientation)
            //set the base target position (seek)
            base.target.position = circleCenter + wanderRadius * O2V(targetOrientation)
            return base.getMovement()
            */
            
            this.TurnAngle += RandomHelper.RandomBinomial() * this.WanderRate;
            this.WanderOrientation = this.TurnAngle + this.Character.orientation;
            //public static Vector3 ConvertOrientationToVector(float orientation)
            this.CircleCenter = this.Character.position + this.WanderOffset * MathHelper.ConvertOrientationToVector(this.Character.orientation);
            base.Target.position = this.CircleCenter + this.WanderRadius * MathHelper.ConvertOrientationToVector(this.WanderOrientation);

            if (this.DebugTarget != null)
            {
                this.DebugTarget.transform.position = this.Target.position;
            }
            
            return base.GetMovement();
        }
    }
}
