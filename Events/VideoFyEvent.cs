using System;

namespace MediaFy.Events
{
    /// <summary>
    /// Classe de evento para abrir/fechar a janela de reprodução de vídeo.
    /// </summary>
    public class VideoFyEvent : EventArgs
    {
        // Variável privada para indicar se a janela de reprodução de vídeo está aberta (true) ou fechada (false).
        private bool isVideo;

        /// <summary>
        /// Construtor da classe VideoFyEvent.
        /// </summary>
        /// <param name="isVideo">Valor booleano que indica se a janela de reprodução de vídeo está aberta (true) ou fechada (false).</param>
        public VideoFyEvent(bool isVideo)
        {
            this.isVideo = isVideo;
        }

        /// <summary>
        /// Propriedade que indica se a janela de reprodução de vídeo está aberta (true) ou fechada (false).
        /// </summary>
        public bool IsVideo
        {
            get { return isVideo; }
        }
    }
}
