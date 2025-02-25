using UnityEngine;

namespace EnemyComponents.Animations
{
    public static class AnimationDataParamsEnemy
    {
        public static class Params
        {
            private const string WalkParameter = "isWalk";
            private const string SpawnParameter = "onSpawn";
            private const string DeathParameter = "onDead";
            private const string HitParameter = "onHit";
            private const string AttackAroundVariant1Parameter = "attackAroundVar1";
            private const string AttackAroundVariant2Parameter = "attackAroundVar2";
            private const string AttackProjectileParameter = "attackProjectile";
            private const string AttackVar1Parameter = "attackVar1";
            private const string AttackVar2Parameter = "attackVar2";
            private const string AttackFrontVariant1Parameter = "attackFrontVar1";
            private const string AttackFrontVariant2Parameter = "attackFrontVar2";
            private const string AttackGroundParameter = "attackGround";
            private const string AttackJumpParameter = "attackJump";
            
            public static readonly int Walking = Animator.StringToHash(WalkParameter);
            public static readonly int Spawning = Animator.StringToHash(SpawnParameter);
            public static readonly int Dead = Animator.StringToHash(DeathParameter);
            public static readonly int TakeDamage = Animator.StringToHash(HitParameter);
            public static readonly int AttackAroundVariant1 = Animator.StringToHash(AttackAroundVariant1Parameter);
            public static readonly int AttackAroundVariant2 = Animator.StringToHash(AttackAroundVariant2Parameter);
            public static readonly int AttackProjectile = Animator.StringToHash(AttackProjectileParameter);
            public static readonly int AttackVar1 = Animator.StringToHash(AttackVar1Parameter);
            public static readonly int AttackVar2 = Animator.StringToHash(AttackVar2Parameter);
            public static readonly int AttackFrontVariant1 = Animator.StringToHash(AttackFrontVariant1Parameter);
            public static readonly int AttackFrontVariant2 = Animator.StringToHash(AttackFrontVariant2Parameter);
            public static readonly int AttackGround = Animator.StringToHash(AttackGroundParameter);
            public static readonly int AttackJump = Animator.StringToHash(AttackJumpParameter);
        }
    }
}