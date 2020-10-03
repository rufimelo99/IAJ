using Assets.Scripts.IAJ.Unity.Util;
using UnityEngine;

    //PSEUDOCODE TGETMOVEMENT
    /*

    direction = destinationTarget.position – character.position
    distance = direction.magnitude()
    if (distance < stopRadius)
    desiredSpeed = 0
    else if (distance > slowRadius)
    desiredSpeed = maxSpeed
    else
    desiredSpeed = maxSpeed *(distance/slowRadius)
    //set the target velocity of the base clase (Velocity Matching)
    base.target.velocity = direction.normalize()*desiredSpeed
    //execute the getMovement of the base class
    return base.getMovement()
    */
namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicArrive : DynamicVelocityMatch
    {
        public override string Name
        {
            get { return "Arrive"; }
        }

        public float stopRadius { get; set; }
        public float slowRadius { get; set; }
        
        public DynamicArrive()
        {
            base.MovingTarget = new KinematicData();
        }
        public override MovementOutput GetMovement()
        {
            Vector3 direction = this.Target.position - this.Character.position;
            float distance = direction.magnitude;

            float desiredSpeed = 0;

            if(distance < this.stopRadius){
                desiredSpeed = 0;
            }else if(distance>this.slowRadius){
                desiredSpeed = this.MaxAcceleration * (distance/this.slowRadius);
            }

            base.MovingTarget.velocity = direction.normalized * desiredSpeed;

            
            return base.GetMovement();
        }
    }
    
}
