//using System;
//using System.Runtime.InteropServices;
//using System.Text;
//using FMOD.Studio;
//using UnityEngine;
//
//namespace RitualRhythm.Actor.Enemy {
//    public class EnemyModel : ActorModel {
//        private readonly BeatExecutor _beatExecutor;
//        private bool _isAttackQueued;
//
//        public EnemyModel(Vector2 position, BeatState beatState, BeatExecutor beatExecutor) : base(position) {
//            _beatExecutor = beatExecutor;
//        }
//
//        public override void Attack() {
//            if (!_isAttackQueued) {
//                _beatExecutor.queueCallback(AttackOnBeat);
//                _isAttackQueued = true;
//            }
//        }
//
//        private bool AttackOnBeat(EVENT_CALLBACK_TYPE type, IntPtr instance, IntPtr parameters) {
//            FMOD.Studio.TIMELINE_MARKER_PROPERTIES marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES) Marshal.PtrToStructure(parameters, typeof (FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
//            IntPtr namePtr = marker.name;
//            int nameLen = 0;
//            while (Marshal.ReadByte(namePtr, nameLen) != 0) {
//                ++nameLen;
//            }
//            byte[] buffer = new byte[nameLen];
//            Marshal.Copy(namePtr, buffer, 0, buffer.Length);
//            string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
//            Debug.Log(name);
//            if (name == "BeatIn") {
//                base.Attack();
//                _isAttackQueued = false;
//                return true;
//            }
//            return false;
//        }
//    }
//}
