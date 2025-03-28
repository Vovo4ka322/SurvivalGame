using UnityEngine;
using TMPro;

namespace Game.Scripts.EnemyComponents
{
    public class WaveTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _waveLabel;
        [SerializeField] private TextMeshProUGUI _bossText;

        private WaveCycle _waveCycle;

        private float _time;
        private bool _isRunning;
        private bool _isBossWave;

        private void Update()
        {
            if (!_isRunning)
            {
                return;
            }

            if (_isBossWave)
            {
                _time += Time.deltaTime;
            }
            else
            {
                _time -= Time.deltaTime;

                if (_time <= 0)
                {
                    _time = 0;
                    _isRunning = false;
                }
            }

            _timerText.text = FormatTime(_time);
        }

        public void SetWaveCycle(WaveCycle waveCycle)
        {
            if (_waveCycle != null)
            {
                _waveCycle.OnWaveStart -= StartTimer;
            }

            _waveCycle = waveCycle;

            if (_waveCycle != null)
            {
                _waveCycle.OnWaveStart += StartTimer;
            }
        }

        private void StartTimer(float waveDuration, int waveNumber)
        {
            if (waveNumber == 0)
            {
                _isBossWave = true;
                _waveLabel.gameObject.SetActive(false);
                _bossText.gameObject.SetActive(true);
                _time = 0f;
            }
            else
            {
                _isBossWave = false;
                _waveLabel.text = $"{waveNumber}";
                _time = waveDuration;
            }

            _isRunning = true;
        }

        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            return minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}