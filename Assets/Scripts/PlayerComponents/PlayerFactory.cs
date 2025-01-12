using PlayerComponents;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFactory", menuName = "PlayerFactory")]
public class PlayerFactory : ScriptableObject
{
    [SerializeField] private Player _firstMeleePlayer;
    [SerializeField] private Player _secondMeleePlayer;
    [SerializeField] private Player _thirdMeleePlayer;
    [SerializeField] private Player _fourthMeleePlayer;
    [SerializeField] private Player _firstRangePlayer;
    [SerializeField] private Player _secondRangePlayer;
    [SerializeField] private Player _thirdRangePlayer;
    [SerializeField] private Player _fourthRangePlayer;

    public Player Get(PlayerSkins playerSkin, Transform spawnPoint)
    {
        Player player = Instantiate(GetPrefab(playerSkin), spawnPoint.position, Quaternion.identity);

        return player;
    }

    private Player GetPrefab(PlayerSkins playerSkin)
    {
        switch (playerSkin)
        {
            case PlayerSkins.FirsMeleePlayer:
                return _firstMeleePlayer;

            case PlayerSkins.SecondMeleePlayer:
                return _secondMeleePlayer;

            case PlayerSkins.ThirdMeleePlayer:
                return _thirdMeleePlayer;

            case PlayerSkins.FourthMeleePlayer:
                return _fourthMeleePlayer;

            case PlayerSkins.FirstRangePlayer:
                return _firstRangePlayer;

            case PlayerSkins.SecondRangePlayer:
                return _secondRangePlayer;

            case PlayerSkins.ThirdRangePlayer:
                return _thirdRangePlayer;

            case PlayerSkins.FourthRangePlayer:
                return _fourthRangePlayer;

            default: return null;
        }
    }
}