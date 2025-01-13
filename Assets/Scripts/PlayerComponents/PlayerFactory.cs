using PlayerComponents;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField] private Player _firstMeleePlayer;
    [SerializeField] private Player _secondMeleePlayer;
    [SerializeField] private Player _thirdMeleePlayer;
    [SerializeField] private Player _firstRangePlayer;
    [SerializeField] private Player _secondRangePlayer;
    [SerializeField] private Player _thirdRangePlayer;

    public Player Get(PlayerSkins playerSkin, Transform spawnPoint)
    {
        Player player = Instantiate(GetPrefab(playerSkin), spawnPoint.position, Quaternion.identity);

        return player;
    }

    private Player GetPrefab(PlayerSkins playerSkin)
    {
        switch (playerSkin)
        {
            case PlayerSkins.FirstMeleePlayer:
                return _firstMeleePlayer;

            case PlayerSkins.SecondMeleePlayer:
                return _secondMeleePlayer;

            case PlayerSkins.ThirdMeleePlayer:
                return _thirdMeleePlayer;

            case PlayerSkins.FirstRangePlayer:
                return _firstRangePlayer;

            case PlayerSkins.SecondRangePlayer:
                return _secondRangePlayer;

            case PlayerSkins.ThirdRangePlayer:
                return _thirdRangePlayer;

            default: return null;
        }
    }
}