using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    class Semicolon : Instruction
    {

        private Instruction a;
        private Instruction b;

        private bool aIsDone = false, bIsDone = false;

        public Semicolon(Instruction A, Instruction B)
        {
            this.a = A;
            this.b = B;
        }

        public override InstructionResult Execute(float deltaTime)
        {
            if (!aIsDone)
            {
                var aRes = a.Execute(deltaTime);
                switch (aRes)
                {
                    case InstructionResult.Running:
                        return InstructionResult.Running;
                    case InstructionResult.Done:
                        aIsDone = true;
                        return InstructionResult.Running;
                    case InstructionResult.RunningAndCreateAstroid:
                        return InstructionResult.RunningAndCreateAstroid;
                    case InstructionResult.DoneAndCreateAstroid:
                        aIsDone = true;
                        return InstructionResult.RunningAndCreateAstroid;
                    default:
                        return aRes;
                        break;
                }
            }
            else
            {
                if(!bIsDone) {
                    var bRes = b.Execute(deltaTime);
                    switch (bRes)
                    {
                        case InstructionResult.Done:
                            bIsDone = true;
                            break;
                        case InstructionResult.DoneAndCreateAstroid:
                            bIsDone = true;
                            break;
                    }
                    return bRes;
                }
                else {
                    return InstructionResult.Done;
                }
            }
        }

        public override Instruction Reset()
        {
            return new Semicolon(a.Reset(), b.Reset());
        }
    }
}
