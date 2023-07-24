using System;

namespace MediaFy.Events
{
    /// <summary>
    /// Classe de evento para quando uma playlist é movida pelo mouse (hover).
    /// </summary>
    public class Hover : EventArgs
    {
        private bool isHovering;

        /// <summary>
        /// Construtor da classe Hover.
        /// </summary>
        /// <param name="isHovering">Valor booleano que indica se a playlist está sendo movida pelo mouse (hover).</param>
        public Hover(bool isHovering)
        {
            this.isHovering = isHovering;
        }

        /// <summary>
        /// Propriedade que indica se a playlist está sendo movida pelo mouse (hover).
        /// </summary>
        public bool IsHovering
        {
            get { return isHovering; }
        }
    }
}
