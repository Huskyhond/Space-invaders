using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    class Wait : Instruction
    {
        float waitTime, initialTime;
        public Wait(float waitTime)
        {
            this.waitTime = waitTime;
            this.initialTime = waitTime;
        }

        public override InstructionResult Execute(float deltaTime)
        {
            waitTime -= deltaTime;
            if (waitTime <= 0.0f)
                return InstructionResult.Done;
            else
                return InstructionResult.Running;
        }

        public override void Reset()
        {
            waitTime = initialTime;   
        }
    }
}
