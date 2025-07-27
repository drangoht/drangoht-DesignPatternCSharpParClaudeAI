using System;

namespace Patterns.Behavioral.State
{
    /// <summary>
    /// Context - Classe qui maintient une référence à un objet State concret
    /// et délègue toutes les requêtes spécifiques à l'état à cet objet
    /// </summary>
    public class AudioPlayer
    {
        // État actuel du lecteur
        private IState _state;
        
        // Propriétés du contexte qui peuvent être utilisées par les états
        public bool IsPlaying { get; set; }
        public bool IsPaused { get; set; }
        public int CurrentTrack { get; set; }
        public int Volume { get; private set; }
        
        // Référence vers tous les états possibles
        public readonly IState StoppedState;
        public readonly IState PlayingState;
        public readonly IState PausedState;
        public readonly IState LockedState;
        
        public AudioPlayer()
        {
            // Initialisation des états
            StoppedState = new StoppedState(this);
            PlayingState = new PlayingState(this);
            PausedState = new PausedState(this);
            LockedState = new LockedState(this);
            
            // Initialisation des propriétés
            CurrentTrack = 1;
            Volume = 50; // 0-100
            
            // État initial
            _state = StoppedState;
            IsPlaying = false;
            IsPaused = false;
        }
        
        // Méthode pour changer l'état actuel
        public void ChangeState(IState state)
        {
            _state = state;
        }
        
        // Méthodes qui délèguent les opérations à l'état actuel
        public void Play()
        {
            _state.Play();
        }
        
        public void Stop()
        {
            _state.Stop();
        }
        
        public void Pause()
        {
            _state.Pause();
        }
        
        public void Lock()
        {
            _state.Lock();
        }
        
        public void Unlock()
        {
            _state.Unlock();
        }
        
        public void NextTrack()
        {
            _state.NextTrack();
        }
        
        public void PreviousTrack()
        {
            _state.PreviousTrack();
        }
        
        public void VolumeUp()
        {
            if (Volume < 100)
            {
                Volume += 10;
                if (Volume > 100) Volume = 100;
                Console.WriteLine($"Volume augmenté à {Volume}%");
            }
            else
            {
                Console.WriteLine("Volume déjà au maximum");
            }
        }
        
        public void VolumeDown()
        {
            if (Volume > 0)
            {
                Volume -= 10;
                if (Volume < 0) Volume = 0;
                Console.WriteLine($"Volume baissé à {Volume}%");
            }
            else
            {
                Console.WriteLine("Volume déjà au minimum");
            }
        }
        
        // Permet d'obtenir l'état actuel sous forme de chaîne pour l'affichage
        public string GetStatus()
        {
            string status = "État inconnu";
            
            if (_state == StoppedState) status = "Arrêté";
            else if (_state == PlayingState) status = "Lecture";
            else if (_state == PausedState) status = "Pause";
            else if (_state == LockedState) status = "Verrouillé";
            
            return $"Lecteur audio | Piste: {CurrentTrack} | Volume: {Volume}% | État: {status}";
        }
    }
    
    /// <summary>
    /// State - Interface définissant les méthodes qui doivent être implémentées
    /// par tous les états concrets
    /// </summary>
    public interface IState
    {
        void Play();
        void Stop();
        void Pause();
        void Lock();
        void Unlock();
        void NextTrack();
        void PreviousTrack();
    }
    
    /// <summary>
    /// ConcreteState - Implémente le comportement associé à l'état "Arrêté"
    /// </summary>
    public class StoppedState : IState
    {
        private readonly AudioPlayer _player;
        
        public StoppedState(AudioPlayer player)
        {
            _player = player;
        }
        
        public void Play()
        {
            Console.WriteLine("▶️ Démarrage de la lecture");
            _player.IsPlaying = true;
            _player.IsPaused = false;
            _player.ChangeState(_player.PlayingState);
        }
        
        public void Stop()
        {
            Console.WriteLine("⏹️ Déjà à l'arrêt");
        }
        
        public void Pause()
        {
            Console.WriteLine("⏸️ Impossible de mettre en pause: déjà à l'arrêt");
        }
        
        public void Lock()
        {
            Console.WriteLine("🔒 Verrouillage du lecteur");
            _player.ChangeState(_player.LockedState);
        }
        
        public void Unlock()
        {
            Console.WriteLine("🔓 Le lecteur n'est pas verrouillé");
        }
        
        public void NextTrack()
        {
            _player.CurrentTrack++;
            Console.WriteLine($"⏭️ Piste suivante sélectionnée (piste {_player.CurrentTrack})");
        }
        
        public void PreviousTrack()
        {
            if (_player.CurrentTrack > 1)
            {
                _player.CurrentTrack--;
                Console.WriteLine($"⏮️ Piste précédente sélectionnée (piste {_player.CurrentTrack})");
            }
            else
            {
                Console.WriteLine("⏮️ Impossible de revenir en arrière: première piste");
            }
        }
    }
    
    /// <summary>
    /// ConcreteState - Implémente le comportement associé à l'état "En lecture"
    /// </summary>
    public class PlayingState : IState
    {
        private readonly AudioPlayer _player;
        
        public PlayingState(AudioPlayer player)
        {
            _player = player;
        }
        
        public void Play()
        {
            Console.WriteLine("▶️ Déjà en lecture");
        }
        
        public void Stop()
        {
            Console.WriteLine("⏹️ Arrêt de la lecture");
            _player.IsPlaying = false;
            _player.IsPaused = false;
            _player.ChangeState(_player.StoppedState);
        }
        
        public void Pause()
        {
            Console.WriteLine("⏸️ Mise en pause de la lecture");
            _player.IsPlaying = false;
            _player.IsPaused = true;
            _player.ChangeState(_player.PausedState);
        }
        
        public void Lock()
        {
            Console.WriteLine("🔒 Verrouillage du lecteur");
            _player.ChangeState(_player.LockedState);
        }
        
        public void Unlock()
        {
            Console.WriteLine("🔓 Le lecteur n'est pas verrouillé");
        }
        
        public void NextTrack()
        {
            _player.CurrentTrack++;
            Console.WriteLine($"⏭️ Lecture de la piste suivante (piste {_player.CurrentTrack})");
        }
        
        public void PreviousTrack()
        {
            if (_player.CurrentTrack > 1)
            {
                _player.CurrentTrack--;
                Console.WriteLine($"⏮️ Lecture de la piste précédente (piste {_player.CurrentTrack})");
            }
            else
            {
                Console.WriteLine("⏮️ Impossible de revenir en arrière: première piste");
            }
        }
    }
    
    /// <summary>
    /// ConcreteState - Implémente le comportement associé à l'état "En pause"
    /// </summary>
    public class PausedState : IState
    {
        private readonly AudioPlayer _player;
        
        public PausedState(AudioPlayer player)
        {
            _player = player;
        }
        
        public void Play()
        {
            Console.WriteLine("▶️ Reprise de la lecture");
            _player.IsPlaying = true;
            _player.IsPaused = false;
            _player.ChangeState(_player.PlayingState);
        }
        
        public void Stop()
        {
            Console.WriteLine("⏹️ Arrêt de la lecture");
            _player.IsPlaying = false;
            _player.IsPaused = false;
            _player.ChangeState(_player.StoppedState);
        }
        
        public void Pause()
        {
            Console.WriteLine("⏸️ Déjà en pause");
        }
        
        public void Lock()
        {
            Console.WriteLine("🔒 Verrouillage du lecteur");
            _player.ChangeState(_player.LockedState);
        }
        
        public void Unlock()
        {
            Console.WriteLine("🔓 Le lecteur n'est pas verrouillé");
        }
        
        public void NextTrack()
        {
            _player.CurrentTrack++;
            Console.WriteLine($"⏭️ Piste suivante sélectionnée (piste {_player.CurrentTrack})");
        }
        
        public void PreviousTrack()
        {
            if (_player.CurrentTrack > 1)
            {
                _player.CurrentTrack--;
                Console.WriteLine($"⏮️ Piste précédente sélectionnée (piste {_player.CurrentTrack})");
            }
            else
            {
                Console.WriteLine("⏮️ Impossible de revenir en arrière: première piste");
            }
        }
    }
    
    /// <summary>
    /// ConcreteState - Implémente le comportement associé à l'état "Verrouillé"
    /// </summary>
    public class LockedState : IState
    {
        private readonly AudioPlayer _player;
        private readonly bool _wasPlayingBeforeLock;
        
        public LockedState(AudioPlayer player)
        {
            _player = player;
            _wasPlayingBeforeLock = player.IsPlaying;
        }
        
        public void Play()
        {
            Console.WriteLine("🔒 Impossible de démarrer la lecture: lecteur verrouillé");
        }
        
        public void Stop()
        {
            Console.WriteLine("🔒 Impossible d'arrêter: lecteur verrouillé");
        }
        
        public void Pause()
        {
            Console.WriteLine("🔒 Impossible de mettre en pause: lecteur verrouillé");
        }
        
        public void Lock()
        {
            Console.WriteLine("🔒 Le lecteur est déjà verrouillé");
        }
        
        public void Unlock()
        {
            Console.WriteLine("🔓 Déverrouillage du lecteur");
            
            // Retour à l'état précédent
            if (_wasPlayingBeforeLock)
            {
                _player.IsPlaying = true;
                _player.IsPaused = false;
                _player.ChangeState(_player.PlayingState);
            }
            else if (_player.IsPaused)
            {
                _player.IsPlaying = false;
                _player.IsPaused = true;
                _player.ChangeState(_player.PausedState);
            }
            else
            {
                _player.IsPlaying = false;
                _player.IsPaused = false;
                _player.ChangeState(_player.StoppedState);
            }
        }
        
        public void NextTrack()
        {
            Console.WriteLine("🔒 Impossible de changer de piste: lecteur verrouillé");
        }
        
        public void PreviousTrack()
        {
            Console.WriteLine("🔒 Impossible de changer de piste: lecteur verrouillé");
        }
    }
}


