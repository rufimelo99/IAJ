using UnityEngine;

namespace Assets.Scripts.IAJ.Unity.Movement.DynamicMovement
{
    public class DynamicAvoidCharacter : DynamicMovement
    {
        public override string Name
        {
            get { return "Avoid Character"; }
        }

        public float MaxTimeLookAhead { get; set; }
        public float AvoidMargin { get; set; }


        //Ex2
        public float collisionRadius { get; set; }


        public DynamicAvoidCharacter(KinematicData target)
        {
            this.Target = target;
            this.Output = new MovementOutput();
        }


        /*
         Pseudo code

        Class CharacterAvoidance
        character, target, maxAcceleration, collisionRadius, maxTimeLookAhead
        def getMovement ()
            deltaPos = target.position – character.position
            deltaVel = target.velocity – character.velocity
            deltaSqrSpeed = deltaVel.sqrMagnitude()
            if(deltaSqrSpeed==0) return empty movement output
            timeToClosest = -Vector3.Dot(deltaPos,deltaVel)/deltaSqrSpeed
            if(timeToClosest > MaxTimeLookAhead) return empty movement output
            //for efficiency reasons I use the deltas instead of character and target
            futureDeltaPos = deltaPos + deltaVel * timeToClosest
            futureDistance = futureDeltaPos.magnitude()
            if(futureDistance > 2*collisionRadius) return empty movement output
            if(futureDistance <= 0 or deltaPos.magnitude() < 2 * collisionRadius)
            //deals with exact or immediate collisions
            movementOutput.linear = character.position – target.position
            else
            movementOutput.linear = futureDeltaPos*-1
            movementOutput.linear = movementOutput.linear.normalized()* maxAcceleration
            return movementOutput
         */
        public override MovementOutput GetMovement()
        {
            this.Output.Clear();

            Vector3 deltaPos = this.Target.Position - this.Character.Position;
            Vector3 deltaVel = this.Target.velocity - this.Character.velocity;
            Debug.Log("DeltaPos: " + deltaPos);
            Debug.Log("DeltaVel: " + deltaVel);

            float deltaSqrSpeed = deltaVel.sqrMagnitude;

            if (deltaSqrSpeed == 0)
            {
                return this.Output;
            }

            float timeToClosest = -Vector3.Dot(deltaPos, deltaVel) / deltaSqrSpeed;

            // Time to closest longer than look ahead
            if (timeToClosest > this.MaxTimeLookAhead)
            {
                return this.Output;
            }

            Vector3 futureDeltaPos = deltaPos + deltaVel * timeToClosest;
            float futureDistance = futureDeltaPos.magnitude;

            // No collision
            if (futureDistance > 2 * this.AvoidMargin)
            {
                return this.Output;
            }

            // Immediate Collisions
            if (futureDistance <= 0 || deltaPos.magnitude < 2 * this.AvoidMargin)
            {
                this.Output.linear = this.Character.Position - this.Target.Position;
            }
            else
            {
                this.Output.linear = futureDeltaPos * -1;
                this.Output.linear.y = 0;
            }

            this.Output.linear = this.Output.linear.normalized * this.MaxAcceleration;

            this.Output.linear.y = 0;
            return this.Output;

        }
    }
}