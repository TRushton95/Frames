import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

clr.AddReference("Monogame.Framework")
from Microsoft.Xna.Framework import Vector2

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, this.Show)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, this.Hide)

this.AddEventHandler('textbox', EventTypeConstants.Frames.ElementClick, this.Move)