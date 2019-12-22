import clr
clr.AddReference("Frames")
from Frames.EventSystem import *

this.AddEventHandler('menu-button', EventTypeConstants.Frames.ElementClick, this.Show)
this.AddEventHandler('menu-close-icon', EventTypeConstants.Frames.ElementClick, this.Hide)