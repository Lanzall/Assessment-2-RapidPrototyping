using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParticlesX
{
    /// <summary>
    /// Toggles a particles systems to play or stop
    /// </summary>
    static public void ToggleParticles(ParticleSystem[] _particles, bool _show)
    {
        for (int i = 0; i < _particles.Length; i++)
        {
            if (_show)
                _particles[i].Play();
            else
                _particles[i].Stop();
        }
    }

    /// <summary>
    /// Toggles a particles systems to play or stop
    /// </summary>
    static public void ToggleParticles(List<ParticleSystem> _particles, bool _show)
    {
        for (int i = 0; i < _particles.Count; i++)
        {
            if (_show)
                _particles[i].Play();
            else
                _particles[i].Stop();
        }
    }
    
    static public void PlayParticles(ParticleSystem _particles, Vector3 _position)
    {
        if (!_particles)
            return;
        
        _particles.transform.position = _position;
        _particles.Play();
    }
}
