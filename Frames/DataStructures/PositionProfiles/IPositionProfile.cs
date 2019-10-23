namespace Frames.DataStructures.PositionProfiles
{
    #region Usings

    using Microsoft.Xna.Framework;

    #endregion

    /// <summary>
    /// Defines a contract for a class that represents how a component is positioned.
    /// </summary>
    public interface IPositionProfile
    {
        #region Methods
        
        /// <summary>
        /// Calculates the coordinates of the component.
        /// </summary>
        Vector2 CalculatePosition(Rectangle parentBounds, Size componentSize);
        
        #endregion
    }
}
