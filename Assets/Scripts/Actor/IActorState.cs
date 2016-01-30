using UnityEngine;

namespace RitualRhythm.Actor {
    public interface IActorState {
        IActorState Jump(ActorModel actorModel);
        IActorState LookTowards(ActorModel actorModel, Vector2 direction);
        IActorState Move(ActorModel actorModel, Vector2 position);
        IActorState Attack(ActorModel actorModel);
        IActorState GetHurt(ActorModel actorModel);
        IActorState GetHurtBadly(ActorModel actorModel);
        IActorState Update(ActorModel actorModel, float deltaTime);
    }
}