using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay
{
    public class DayCycle : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _timeOfDay;
        [SerializeField] private float _dayDuration = 30f;

        [SerializeField] private AnimationCurve _sunCurve;
        [SerializeField] private AnimationCurve _moonCurve;
        [SerializeField] private AnimationCurve _skyboxCurve;

        [SerializeField] private Material _daySkybox;

        [SerializeField] private Material _starsMaterial;
        [SerializeField] private ParticleSystem _rain;

        [SerializeField] private Light _sun;
        [SerializeField] private Light _moon;

        [Space]

        [SerializeField] private bool _stop;
        [SerializeField] private bool _playRain;

        private float _sunIntensity;
        private float _moonIntensity;

        private void Start()
        {
            _sunIntensity = _sun.intensity;
            _moonIntensity = _moon.intensity;
        }

        private void Update()
        {
            if (!_stop) 
                _timeOfDay += Time.deltaTime / _dayDuration;

            if (_timeOfDay >= 1)
                _timeOfDay -= 1;

            var skyBoxExposure = _skyboxCurve.Evaluate(_timeOfDay);
            var sunIntensity = _sunIntensity * _sunCurve.Evaluate(_timeOfDay);
            var moonIntensity = _moonIntensity * _moonCurve.Evaluate(_timeOfDay);

            _rain.gameObject.SetActive(_playRain);
            if (_playRain)
            {
                skyBoxExposure *= 0.25f;
                sunIntensity *= 0.25f;
                moonIntensity *= 0.25f;
            }

            RenderSettings.skybox.SetFloat("_Exposure", skyBoxExposure);
            RenderSettings.sun = _timeOfDay > 0.5f ? _moon : _sun;
            DynamicGI.UpdateEnvironment();

            _starsMaterial.color = new Color(1f, 1f, 1f, 1f - _skyboxCurve.Evaluate(_timeOfDay));

            _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f, 180f, 0f);
            _moon.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f + 180f, 180, 0f);

            _sun.intensity = sunIntensity;
            _moon.intensity = moonIntensity;
        }
    }
}
