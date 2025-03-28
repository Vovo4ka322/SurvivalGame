using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace Game.Scripts.LeaderboardComponents
{
    public class Leaderboard : MonoBehaviour
    {
        private const string NameLeaderboard = "LeaderBoard";
        private const string Ru = "ru";
        private const string En = "en";
        private const string Tr = "tr";
        private const string AnonymousDataRu = "??? ??????";
        private const string AnonymousDataEn = "No data";
        private const string AnonymousDataTr = "Veri yok";
        private const string AnonymousNameRu = "??????";
        private const string AnonymousNameEn = "Anonymous";
        private const string AnonymousNameTr = "Anonim";

        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private VerticalLayoutGroup _currentPlayerLayoutGroup;
        [SerializeField] private LeaderboardPlayerData _leaderboardPlayerData;
        [SerializeField] private LeaderboardPlayerData _currentLeaderboardPlayerDataPrefab;
        [SerializeField] private int _maxPlayer = 7;

        private LeaderboardPlayerData _currentLeaderboardPlayerEntry;

        private readonly List<LeaderboardPlayerData> _playerDataEntries = new List<LeaderboardPlayerData>();

        private void OnEnable()
        {
            YandexGame.onGetLeaderboard += OnUpdate;
            RequestLeaderboardData();
        }

        private void OnDisable()
        {
            YandexGame.onGetLeaderboard -= OnUpdate;
        }

        private void RequestLeaderboardData()
        {
            string leaderboardName = NameLeaderboard;
            int maxQuantityPlayers = _maxPlayer;
            int quantityTop = _maxPlayer;
            int quantityAround = 0;

            YandexGame.GetLeaderboard(leaderboardName, maxQuantityPlayers, quantityTop, quantityAround, null);
        }

        private void OnUpdate(LBData lbData)
        {
            if (lbData.technoName != NameLeaderboard)
            {
                return;
            }

            foreach (LeaderboardPlayerData entry in _playerDataEntries)
            {
                Destroy(entry.gameObject);
            }

            _playerDataEntries.Clear();

            if (_currentLeaderboardPlayerEntry != null)
            {
                Destroy(_currentLeaderboardPlayerEntry.gameObject);

                _currentLeaderboardPlayerEntry = null;
            }

            if (lbData.entries == null || lbData.entries.Length == 0)
            {
                string noDataMessage = YandexGame.savesData.language switch
                {
                    Ru => AnonymousDataRu,
                    En => AnonymousDataEn,
                    Tr => AnonymousDataTr,
                    _ => "...",
                };

                LeaderboardPlayerData leaderboardPlayerData = Instantiate(_leaderboardPlayerData, _gridLayout.transform);
                leaderboardPlayerData.SetData("-", noDataMessage, "-", false);
                leaderboardPlayerData.UpdateEntries();
                _playerDataEntries.Add(leaderboardPlayerData);
            }
            else
            {
                LBPlayerData[] players = lbData.players;

                for (int i = 0; i < players.Length; i++)
                {
                    LBPlayerData player = players[i];
                    LeaderboardPlayerData leaderboardPlayerData = Instantiate(_leaderboardPlayerData, _gridLayout.transform);

                    string playerName = string.IsNullOrEmpty(player.name) ? GetAnonimName() : LBMethods.AnonimName(player.name);
                    bool isCurrentUser = player.uniqueID == YandexGame.playerId;

                    leaderboardPlayerData.SetData(player.rank.ToString(), playerName, player.score.ToString(), isCurrentUser);
                    leaderboardPlayerData.UpdateEntries();

                    _playerDataEntries.Add(leaderboardPlayerData);
                }

                if (lbData.thisPlayer != null)
                {
                    string currentPlayerName = string.IsNullOrEmpty(YandexGame.playerName) ? GetAnonimName() : YandexGame.playerName;
                    string currentPlayerScore = lbData.thisPlayer.score.ToString();
                    string currentPlayerRank = lbData.thisPlayer.rank.ToString();

                    _currentLeaderboardPlayerEntry = Instantiate(_currentLeaderboardPlayerDataPrefab, _currentPlayerLayoutGroup.transform);
                    _currentLeaderboardPlayerEntry.SetData(currentPlayerRank, currentPlayerName, currentPlayerScore, true);
                    _currentLeaderboardPlayerEntry.UpdateEntries();
                }
            }
        }

        private string GetAnonimName()
        {
            return YandexGame.savesData.language switch
            {
                Ru => AnonymousNameRu,
                En => AnonymousNameEn,
                Tr => AnonymousNameTr,
                _ => AnonymousNameEn,
            };
        }
    }
}