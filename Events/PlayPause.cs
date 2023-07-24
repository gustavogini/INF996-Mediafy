using System;

namespace MediaFy.Events
{
    /// <summary>
    /// Classe de evento para alternar reprodução e pausa.
    /// </summary>
    public class PlayPause : EventArgs
    {
        private bool isPlaying;

        /// <summary>
        /// Construtor da classe PlayPause.
        /// </summary>
        /// <param name="isPlaying">Valor booleano que indica se a mídia está em reprodução (true) ou pausada (false).</param>
        public PlayPause(bool isPlaying)
        {
            this.isPlaying = isPlaying;
        }

        /// <summary>
        /// Propriedade que indica se a mídia está em reprodução (true) ou pausada (false).
        /// </summary>
        public bool IsPlaying
        {
            get { return isPlaying; }
        }
    }
}
