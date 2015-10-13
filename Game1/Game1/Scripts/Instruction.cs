using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    enum InstructionResult {
        Running,
        Done,
        RunningAndCreateAstroid,
        DoneAndCreateAstroid
    }
    abstract class Instruction
    {
        public abstract InstructionResult Execute(float deltaTime);
        public abstract void Reset();
    }
}
