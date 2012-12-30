﻿using Microsoft.Kinect;

namespace Com.Enterprisecoding.RobosapienKinect.GestureCommands {
    internal sealed class RightArmCenterCommand : GestureCommandBase {
        public override bool ShouldHandle(JointCollection joints) {
            if (RoboManagerInstance.RightArmStatus == ArmStatus.ArmMiddle) { return false; }

            var shoulderCenter = joints[JointType.ShoulderCenter].AsVector3D();

            var rightArmAngle = GetAngle(joints[JointType.ElbowRight].AsVector3D() - shoulderCenter,
                                        joints[JointType.Spine].AsVector3D() - shoulderCenter,
                                        shoulderCenter);

            return rightArmAngle > Angles.ARM_IN && rightArmAngle < Angles.ARM_OUT;
        }

        public override void Execute() {
            if (RoboManagerInstance.RightArmStatus == ArmStatus.ArmUp) {
                RoboManagerInstance.RightArmIn();
            }
            else {
                RoboManagerInstance.RightArmOut();
            }
        }
    }
}